using System.Text.Json;
using HseBank.Domain;

namespace HseBank.DataImport;

public class JsonImporter
{
    public ImportData ImportDataFromFile(string filePath)
    {
        var json = File.ReadAllText(filePath);
        return ParseData(json);
    }

    private ImportData ParseData(string data)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var importData = JsonSerializer.Deserialize<ImportData>(data, options);
        return importData;
    }

    // Хочу импортировать данные в виде такой структуры.
    public class ImportData
    {
        public List<BankAccount> Accounts { get; set; }
        public List<Category> Categories { get; set; }
        public List<Operation> Operations { get; set; }
    }
}