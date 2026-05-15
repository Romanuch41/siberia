using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiberiaApp.Interface
{
    public interface IMessagePublisher : IAsyncDisposable
    {
        public Task PublishHttpTaskAsync(string url, string method, string body);
    }
}
