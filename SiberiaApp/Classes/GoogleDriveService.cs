using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SiberiaApp.Classes
{
    public class GoogleDriveService
    {
        private readonly DriveService _drive;
        private readonly string FileName = "API.txt";
        private readonly string FoldeId = "1xFn1nZhwBhbuSmQso7inattk9E-LcOQM";

        public GoogleDriveService(Stream ServiceAccountJsonPath, string ApplicationName = "SiberiaApp")
        {
            var credential = GoogleCredential
                .FromStream(ServiceAccountJsonPath)
                .CreateScoped(DriveService.ScopeConstants.Drive);
            _drive = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });
        }

        public async Task<Google.Apis.Drive.v3.Data.File?> FindFileByNameAsync(CancellationToken cancellationToken = default)
        {
            // задаем параметры поиска. это будет список параметров
            var conditions = new List<string>
            {
               $"name = '{FileName}'", // имя файла
               "trashed = false", // не искать в удаленных
               $"'{FoldeId}' in parents" // искать в папке которая имеет определенный ID
            };

            //Выполняем запрос на поиск
            var listRequest = _drive.Files.List(); // это будет список найденных файлов
            listRequest.Q = string.Join(" and ", conditions); // формируем запрос
            listRequest.Spaces = "drive"; // искать в "мой диск"66
            listRequest.Fields = "files(id, name, mimeType, parents)"; // просим свойства найденных файлов
            listRequest.PageSize = 1; // ограничиваем до первого найденного файла

            var response = await listRequest.ExecuteAsync(cancellationToken); // отправляем API запрос

            return response.Files?.FirstOrDefault(); // возвращаем первый найденный файл или null
        }

        public async Task<string> GetApiKeyAsync(CancellationToken cancellationToken = default)
        {
            // 1. Ищем файл
            var file = await FindFileByNameAsync(cancellationToken); // вызываем функцию поиска файла
            if (file == null) // создаем ошибку если файл не найден
                throw new FileNotFoundException($"Файл не найден в папке");

            var request = _drive.Files.Get(file.Id);

            using var ms = new MemoryStream();

            // 1. Скачиваем содержимое
            await request.DownloadAsync(ms, cancellationToken).ConfigureAwait(false); // [web:207][web:210]

            ms.Position = 0;

            // 2. Читаем как текст
            using var reader = new StreamReader(ms, Encoding.UTF8);
            var content = await reader.ReadToEndAsync().ConfigureAwait(false);

            return content;
        }
    }
}
