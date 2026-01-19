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
        private SheetsConfigRoot Sheets;
        Stream File;

        public TableManager(GoogleSheetsService sheetsService, Stream file)
        {
            _sheetsService = sheetsService;
            File = file;
            using var reader = new StreamReader(File);
            var json = reader.ReadToEnd();
            Sheets = JsonSerializer.Deserialize<SheetsConfigRoot>(json) ?? new SheetsConfigRoot();

            Debug.WriteLine($"Load tables {Sheets.sheets.Count}");
        }

        public async Task<IList<IList<object>>> ReadTableAsync(string nameTable, CancellationToken ct = default)
        {
            var cfg = Sheets.sheets
            .FirstOrDefault(s => s.name.Equals(nameTable, StringComparison.OrdinalIgnoreCase));

            if (cfg is null)
                throw new KeyNotFoundException(
                    $"Table reports not found in '{File.Position}'");

            Debug.WriteLine("загружаю таблицу");
            return await _sheetsService.ReadTable(cfg.sheetname, cfg.id, ct);
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
