using Google.Apis.Sheets.v4;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SiberiaApp.Classes
{
    class GoogleSheetReader
    {
        private readonly SheetsService _sheetsService;
        private readonly ILogger<GoogleSheetReader> _logger;

        public GoogleSheetReader(SheetsService sheetsService, ILogger<GoogleSheetReader> logger = null)
        {
            this._sheetsService = sheetsService;
            this._logger = logger;
        }


        public async Task<IList<IList<object>>> ReadRangeAsync(string spreadsheetId, string range)
        {
            try
            {
                var request = _sheetsService.Spreadsheets.Values.Get(spreadsheetId, range);
                var response = await request.ExecuteAsync();

                _logger?.LogInformation($"Прочитано {response.Values?.Count ?? 0} строк из диапазона {range}");

                return response.Values ?? new List<IList<object>>();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Ошибка чтения Google Sheets {spreadsheetId}, диапазон: {range}");
                throw;
            }
        }

        private DateTime TryParseDate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return default;  // или DateTime.MinValue

            return DateTime.TryParseExact(value.Trim(), "dd.MM.yyyy",
                                          CultureInfo.InvariantCulture,
                                          DateTimeStyles.None, out var date)
                   ? date : default;
        }

        public async Task<IList<IList<object>>> ReadLast30DaysAsync(string spreadsheetId, string sheetName, int dateColumnIndex = 0)
        {
            try
            {
                // Читаем весь диапазон листа
                var fullRange = $"{sheetName}!A:Z";
                var fullData = await ReadRangeAsync(spreadsheetId, fullRange);

                if (!fullData.Any())
                {
                    _logger?.LogWarning($"Таблица {spreadsheetId} ({sheetName}) пуста");
                    return new List<IList<object>>();
                }

                // Фильтруем данные за последние 30 дней
                var dateThreshold = DateTime.UtcNow.AddDays(-30);
                var filteredRows = new List<IList<object>>();

                foreach (var row in fullData)
                {
                    if (row.Count > dateColumnIndex && row[dateColumnIndex] != null)
                    {
                        if (TryParseDate(row[dateColumnIndex].ToString()) >= dateThreshold)
                        {
                            filteredRows.Add(row);
                        }
                    }
                }

                _logger?.LogInformation($"Найдено {filteredRows.Count} строк за 30 дней из {fullData.Count} всего в {sheetName}");
                return filteredRows;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Ошибка чтения данных за 30 дней из {spreadsheetId} ({sheetName})");
                throw;
            }
        }

    }
}
