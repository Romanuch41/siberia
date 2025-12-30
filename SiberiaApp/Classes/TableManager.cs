using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace SiberiaApp.Classes
{

    public class TableManager
    {
        public TableSheetsList tables;


        public TableManager() { }

        public void GetTables(string name)
        {
            try
            {
                if (File.Exists("tablecontent.json"))
                    throw new FileNotFoundException("File not found");

                string json = File.ReadAllText("tablecontent.json");
                if (string.IsNullOrWhiteSpace(json))
                    throw new ArgumentException("JSON file is empty or null", nameof(json));

                tables = JsonSerializer.Deserialize<TableSheetsList>(json)
                    ?? throw new InvalidOperationException("Filed while deserilization");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while load tables {ex.Message}");
            }
        }

        public TableFromJson SearchTable(string name)
        {
            if (tables?.sheets == null || tables.sheets.Count == 0)
                throw new InvalidOperationException("Data tables is empty");

            return tables?.sheets?.FirstOrDefault(b => b.name == name) ?? throw new InvalidOperationException("Table is not found");
        }
    }

    public class TableSheetsList
    {
        [JsonPropertyName("sheets")]
        public required List<TableFromJson> sheets { get; set; }
    }

    public class TableFromJson
    {
        [JsonPropertyName("id")]
        public required string id { get; set; }
        [JsonPropertyName("name")]
        public required string name { get; set; }
    }
}
