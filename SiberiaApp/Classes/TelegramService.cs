using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SiberiaApp.Interface;

// TelegramService подготовлен для авторизации пользователей в будеющем будет ещё использоваться для оповещений
// нужно обязательно убрать токен из кода, исопльзовать REST API и реализовать отправку сообщений через API с google apps script

namespace SiberiaApp.Classes
{
    public class TelegramService
    {
        private string veryficationCode = string.Empty;
        private IMessagePublisher _contract;

        public TelegramService(IMessagePublisher contract)
        {
            _contract = contract;
        }

        public async Task SendVerificationCode(string chatid, string veryficationCode)
        {
            if (string.IsNullOrWhiteSpace(chatid) || string.IsNullOrWhiteSpace(veryficationCode))
                throw new ArgumentNullException(nameof(chatid));

            
        }

    }
    
    public class SenMassege()
    {
        public string Text { get; set; }
    }
}

// тут будет типа комит