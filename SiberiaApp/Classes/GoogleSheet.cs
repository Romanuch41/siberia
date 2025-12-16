using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiberiaApp.Classes
{
    class GoogleSheet
    {
        private readonly string[] _scopes = { DriveService.Scope.DriveReadonly };
        private readonly ILogger<GoogleSheet> _logger;

        public GoogleSheet(ILogger<GoogleSheet> logger = null)
        {
            _logger = logger;
        }

        public async Task<DriveService> CreateServiceAccountDriveAsync(string credintalspath = "service.json")
        {
            try
            {
                await using var stream = new FileStream(
                    credintalspath,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read,
                    bufferSize: 4064,
                    useAsync: true);

                var credential = await GoogleCredential
                .FromStreamAsync(stream)         // Используем асинхронную версию
                .ConfigureAwait(false);
            }
        }
    }
}
