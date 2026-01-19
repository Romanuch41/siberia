using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SiberiaApp.Classes
{
    public class TableManager
    {
        private readonly GoogleSheetsService _sheetsService;
        private string TableData = Path.Combine(AppContext.BaseDirectory, "json", "tablecontent.json");
        private SheetsConfigRoot Sheets;

        public TableManager(GoogleSheetsService sheetsService)
        {
            _sheetsService = sheetsService;

            var json = File.ReadAllText(TableData);
            Sheets = JsonSerializer.Deserialize<SheetsConfigRoot>(json) ?? new SheetsConfigRoot();

            Debug.WriteLine($"Load tables {Sheets.sheets.Count}");
        }

        public async Task<IList<IList<object>>> ReadTableAsync(string nameTable, CancellationToken ct = default)
        {
            var cfg = Sheets.sheets
            .FirstOrDefault(s => s.name.Equals(nameTable, StringComparison.OrdinalIgnoreCase));

            if (cfg is null)
                throw new KeyNotFoundException(
                    $"Table reports not found in '{TableData}'");

            Debug.WriteLine("загружаю таблицу");
            return await _sheetsService.ReadTable(cfg.sheetname, cfg.id, ct);
        }

        public async Task ExportReportsToTxtAsync(
    string outputPath, CancellationToken ct = default)
        {
            // читаем таблицу через текущий метод менеджера
            var rows = await ReadTableAsync("Report", ct); // твой ReadTableAsync для "reports"

            var lines = new List<string>();

            foreach (var row in rows)
            {
                // конвертация ячеек в строки; разделитель выбери сам: ;, |, таб и т.п.
                var cells = row.Select(c => c?.ToString() ?? string.Empty);
                var line = string.Join(";", cells);
                lines.Add(line);
            }

            // записываем в текстовый файл
            File.WriteAllLines(outputPath, lines, Encoding.UTF8); // создаст или перезапишет файл [web:471][web:476]
        }
    }

    public class SheetConfig
    {
        public string id { get; set; } = default!;
        public string name { get; set; } = default!;
        public string sheetname { get; set; } = default!;
    }

    public class SheetsConfigRoot
    {
        public List<SheetConfig> sheets { get; set; } = new();
    }
}
