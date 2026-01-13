using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiberiaApp.Classes
{
    public class GoogleSheetsService
    {
        private readonly SheetsService _sheets;

        public GoogleSheetsService(string serviceAccountJsonPath, string applicationName = "SiberiaApp")
        {
            using var stream = new FileStream(serviceAccountJsonPath, FileMode.Open, FileAccess.Read);
            var credential = GoogleCredential
                .FromStream(stream)
                .CreateScoped(SheetsService.Scope.Spreadsheets);

            _sheets = new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });
        }

        public async Task<IList<IList<object>>> ReadTable(string nametable, string idtable, CancellationToken ct = default)
        {
            var request = _sheets.Spreadsheets.Values.Get(idtable, nametable); // весь лист [web:359][web:406]
            var response = await request.ExecuteAsync(ct);
            return response.Values ?? Array.Empty<IList<object>>();
        }
    }
}
