using System.Text.Json;
using FluentAssertions;
using HseBank.DataExport;
using HseBank.DataImport;
using HseBank.Domain;

namespace HseBankTest;

public class JsonTests
{
    [Fact]
    public void Export_ShouldCreateValidJsonFile()
    {
        var exporter = new JsonExporter();
        var filePath = Path.GetTempFileName();

        var accounts = new List<BankAccount> { new("Мой счёт", 1000) };
        var categories = new List<Category> { new(OperationType.Income, "Зарплата") };
        var operations = new List<Operation>
        {
            new(OperationType.Income, accounts[0], 500, categories[0], description: "Зарплата за месяц")
        };

        exporter.ExportDataToFile(filePath, accounts, categories, operations);

        File.Exists(filePath).Should().BeTrue("Файл должен быть создан после экспорта");

        var content = File.ReadAllText(filePath);
        content.Should().NotBeNullOrWhiteSpace("Файл не должен быть пустым после экспорта");

        var importedData = JsonSerializer.Deserialize<JsonImporter.ImportData>(content);
        importedData.Should().NotBeNull();
        importedData.Accounts.Should().HaveCount(1);
        importedData.Accounts[0].Name.Should().Be("Мой счёт");
        importedData.Accounts[0].Balance.Should().Be(1500);

        importedData.Categories.Should().HaveCount(1);
        importedData.Categories[0].Name.Should().Be("Зарплата");
        importedData.Categories[0].Type.Should().Be(OperationType.Income);

        importedData.Operations.Should().HaveCount(1);
        importedData.Operations[0].Amount.Should().Be(500);
        importedData.Operations[0].Description.Should().Be("Зарплата за месяц");

        File.Delete(filePath);
    }

    [Fact]
    public void Import_ShouldLoadDataCorrectly()
    {
        var exporter = new JsonExporter();
        var importer = new JsonImporter();
        var filePath = Path.GetTempFileName();

        var accounts = new List<BankAccount> { new("Мой счёт", 1000) };
        var categories = new List<Category> { new(OperationType.Income, "Зарплата") };
        var operations = new List<Operation>
        {
            new(OperationType.Income, accounts[0], 500, categories[0], description: "Зарплата за месяц")
        };

        exporter.ExportDataToFile(filePath, accounts, categories, operations);
        var importedData = importer.ImportDataFromFile(filePath);

        importedData.Accounts.Should().HaveCount(1);
        importedData.Accounts[0].Name.Should().Be("Мой счёт");
        importedData.Accounts[0].Balance.Should().Be(1500);

        importedData.Categories.Should().HaveCount(1);
        importedData.Categories[0].Name.Should().Be("Зарплата");
        importedData.Categories[0].Type.Should().Be(OperationType.Income);

        importedData.Operations.Should().HaveCount(1);
        importedData.Operations[0].Amount.Should().Be(500);
        importedData.Operations[0].Description.Should().Be("Зарплата за месяц");

        File.Delete(filePath);
    }
}