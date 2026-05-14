using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SiberiaApp.Classes
{
    public class Rabbit : IAsyncDisposable
    {
        private readonly ConnectionFactory _factory;
        private IConnection? _connection;
        private IChannel? _channel;
        private readonly string _queueName = "http_requests";
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public Rabbit()
        {
            _factory = new ConnectionFactory() { HostName = "localhost" };
        }

        public async ValueTask DisposeAsync()
        {
            if (_channel != null)
            {
                await _channel.DisposeAsync();
                _channel = null;
            }

            if (_connection != null)
            {
                await _connection.DisposeAsync();
                _connection = null;
            }

            _semaphore.Dispose();
        }

        private async Task InitializeAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                if (_connection is { IsOpen: true } && _channel is { IsOpen: true })
                    return;

                await DisposeAsync();

                _connection = await _factory.CreateConnectionAsync();
                _channel = await _connection.CreateChannelAsync();

                await _channel.QueueDeclareAsync(
                    queue: _queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
            }
            finally
            {
                _semaphore.Release(); 
            }
        }

        public async Task PublishHttpTaskAsync(string url, string method, string body)
        {
            await InitializeAsync();

            var httpTask = new { url = url, method = method, body = body };
            var json = JsonSerializer.Serialize(httpTask);

            var bytes = Encoding.UTF8.GetBytes(json);

            var props = new BasicProperties
            {
                DeliveryMode = DeliveryModes.Persistent
            };

            await _channel!.BasicPublishAsync(
                exchange: "",
                routingKey: _queueName,
                mandatory: false,
                basicProperties: props,
                body: bytes);
        }
    }
}
