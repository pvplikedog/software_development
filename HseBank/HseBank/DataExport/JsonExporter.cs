using System.Text.Json;
using HseBank.Domain;

namespace HseBank.DataExport;

public class JsonExporter
{
    public string ExportData(IEnumerable<BankAccount> accounts,
        IEnumerable<Category> categories,
        IEnumerable<Operation> operations)
    {
        var data = new
        {
            Accounts = accounts,
            Categories = categories,
            Operations = operations
        };

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        return JsonSerializer.Serialize(data, options);
    }

    public void ExportDataToFile(string filePath, IEnumerable<BankAccount> accounts,
        IEnumerable<Category> categories,
        IEnumerable<Operation> operations)
    {
        var json = ExportData(accounts, categories, operations);
        File.WriteAllText(filePath, json);
    }
}