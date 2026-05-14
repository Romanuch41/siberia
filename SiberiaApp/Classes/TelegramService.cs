using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;

// TelegramService подготовлен для авторизации пользователей в будеющем будет ещё использоваться для оповещений
// нужно обязательно убрать токен из кода, исопльзовать REST API и реализовать отправку сообщений через API с google apps script

namespace SiberiaApp.Classes
{
    public class TelegramService
    {

        public TelegramService()
        {
            _httpClient = new HttpClient();
            factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            using var connection = factory.CreateConnectionAsync();
            var channel = connection.Cre
            
        }

    }
    
    public class SenMassege()
    {
        public string Text { get; set; }
    }
}

// тут будет типа комит