using Google.Apis.Drive.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiberiaApp.Classes
{
    internal class GoogleSheetsService
    {
        private readonly string[] _scopes = { DriveService.Scope.DriveReadonly }; // набор прав
        private readonly ILogger<ServiceGoogleDrive> _logger; // логер
        private SheetsService _sheetsService; // сервис для GoogleSheetsService

        public GoogleSheetsService(ILogger<ServiceGoogleDrive> logger)
        {
            _logger = logger;
        }

        public async Task InitializeSheetsServiceAsync(
            string credentialsPath = "service.json",
            CancellationToken cancellationToken = default)
        {
            if (_sheetsService == null)
            {
                _logger?.LogInformation("Инициализация Google Sheets сервиса...");

                var credential = await GoogleCredential
                    .FromFileAsync(credentialsPath, cancellationToken)
                    .ConfigureAwait(false);

                credential = credential.CreateScoped(_scopes);

                _sheetsService = new SheetsService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "SiberiaApp"
                });

                _logger?.LogInformation("Google Sheets сервис инициализирован");
            }
        }

        // Получить готовый сервис
        public SheetsService GetSheetsService()
        {
            if (_sheetsService == null)
            {
                throw new InvalidOperationException(
                    "SheetsService не инициализирован. Вызовите InitializeSheetsServiceAsync() сначала.");
            }
            return _sheetsService;
        }


    }
}
