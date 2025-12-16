using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SiberiaApp.Classes
{
    class ServiceGoogleDrive
    {
        private readonly string[] _scopes = { DriveService.Scope.DriveReadonly }; // это набор прав
        private readonly ILogger<ServiceGoogleDrive> _logger; // логер можем прописывать ход процесса выполнения кода
        private DriveService _driveService; // это объек для работы с google drive

        public ServiceGoogleDrive(ILogger<ServiceGoogleDrive> logger = null)
        {
            _logger = logger;
        }

        // Метод для инициализации сервиса
        public async Task InitializeDriveServiceAsync(
            string credentialsPath = "service.json",
            CancellationToken cancellationToken = default)
        {
            if (_driveService == null)
            {
                var credential = await GoogleCredential
                    .FromFileAsync(credentialsPath, cancellationToken)
                    .ConfigureAwait(false);

                credential = credential.CreateScoped(_scopes);

                _driveService = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "SiberiaApp"
                });

                _logger?.LogInformation("DriveService инициализирован");
            }
        }

        // Поиск файла по имени
        private async Task<Google.Apis.Drive.v3.Data.File> FindFileByNameAsync(
            string fileName,
            CancellationToken cancellationToken)
        {
            if (_driveService == null)
            {
                throw new InvalidOperationException(
                    "DriveService не инициализирован. Вызовите InitializeDriveServiceAsync() сначала.");
            }

            var request = _driveService.Files.List();
            request.Q = $"name = '{fileName}' and trashed = false";
            request.Fields = "files(id, name, mimeType)";
            request.PageSize = 10;

            var result = await request.ExecuteAsync(cancellationToken)
                .ConfigureAwait(false);

            return result.Files.FirstOrDefault();
        }

        // Чтение содержимого файла
        public async Task<string> ReadFileContentAsync(
            string fileName = "API.txt",
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger?.LogInformation("Поиск файла: {FileName}", fileName);

                // Ищем файл
                var file = await FindFileByNameAsync(fileName, cancellationToken);

                if (file == null)
                {
                    _logger?.LogWarning("Файл '{FileName}' не найден", fileName);
                    throw new FileNotFoundException($"Файл '{fileName}' не найден в Google Drive");
                }

                _logger?.LogInformation("Файл найден: {FileName} (ID: {FileId})", file.Name, file.Id);

                // Скачиваем файл
                var downloadRequest = _driveService.Files.Get(file.Id);
                using var memoryStream = new MemoryStream();

                await downloadRequest.DownloadAsync(memoryStream, cancellationToken)
                    .ConfigureAwait(false);

                memoryStream.Position = 0;

                // Читаем содержимое
                using var reader = new StreamReader(memoryStream, Encoding.UTF8);
                var content = await reader.ReadToEndAsync();

                _logger?.LogInformation("Файл прочитан успешно, размер: {Size} символов", content.Length);

                return content;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Ошибка при чтении файла {FileName}", fileName);
                throw;
            }
        }

    }
}