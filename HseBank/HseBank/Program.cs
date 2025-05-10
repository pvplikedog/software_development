using HseBank.DataExport;
using HseBank.DataImport;
using HseBank.Domain;
using HseBank.Repositories;
using HseBank.Services;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace HseBank;

internal class Program
{
    private static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var facade = serviceProvider.GetRequiredService<FinanceFacade>();
        var exporter = serviceProvider.GetRequiredService<JsonExporter>();
        var importer = serviceProvider.GetRequiredService<JsonImporter>();

        var exportPath = "export.json";

        var exit = false;
        while (!exit)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Выберите действие:")
                    .AddChoices("Создать счёт", "Удалить счёт", "Создать категорию", "Удалить категорию",
                        "Создать операцию", "Показать баланс и аналитику", "Экспорт данных в JSON",
                        "Импорт данных из JSON", "Выход"));

            switch (choice)
            {
                case "Создать счёт":
                    CreateAccount(facade);
                    break;
                case "Удалить счёт":
                    DeleteAccount(facade);
                    break;
                case "Создать категорию":
                    CreateCategory(facade);
                    break;
                case "Удалить категорию":
                    DeleteCategory(facade);
                    break;
                case "Создать операцию":
                    CreateOperation(facade);
                    break;
                case "Показать баланс и аналитику":
                    ShowAnalytics(facade);
                    break;
                case "Экспорт данных в JSON":
                    exporter.ExportDataToFile(exportPath, facade.GetAllAccounts(), facade.GetAllCategories(),
                        facade.GetAllOperations());
                    AnsiConsole.MarkupLine($"[green]Данные экспортированы в файл: {Path.GetFullPath(exportPath)}[/]");
                    break;
                case "Импорт данных из JSON":
                    if (File.Exists(exportPath))
                    {
                        var importedData = importer.ImportDataFromFile(exportPath);
                        facade.LoadData(importedData);
                        AnsiConsole.MarkupLine("[green]Данные успешно импортированы из файла.[/]");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]Файл не найден![/]");
                    }

                    break;
                case "Выход":
                    exit = true;
                    break;
            }
        }

        AnsiConsole.MarkupLine("[blue]До свидания![/]");
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<DataRepository>();
        services.AddSingleton<RepositoryProxy>(provider =>
        {
            var repo = provider.GetRequiredService<DataRepository>();
            return new RepositoryProxy(repo);
        });
        services.AddSingleton<FinanceFacade>();
        services.AddTransient<JsonExporter>();
        services.AddTransient<JsonImporter>();
    }

    private static void CreateAccount(FinanceFacade facade)
    {
        var name = AnsiConsole.Ask<string>("Введите название счёта:");
        var balance = AnsiConsole.Ask<decimal>("Введите начальный баланс:");
        var account = facade.CreateAccount(name, balance);
        AnsiConsole.MarkupLine($"[green]Счёт [bold]{account.Name}[/] успешно создан с балансом {account.Balance}.[/]");
    }

    private static void DeleteAccount(FinanceFacade facade)
    {
        var accounts = facade.GetAllAccounts();
        if (!accounts.Any())
        {
            AnsiConsole.MarkupLine("[red]Нет счетов для удаления.[/]");
            return;
        }

        var account = AnsiConsole.Prompt(
            new SelectionPrompt<BankAccount>()
                .Title("Выберите счёт для удаления:")
                .UseConverter(acc => $"{acc.Name} (Баланс: {acc.Balance})")
                .AddChoices(accounts));

        facade.DeleteAccount(account.Id);
        AnsiConsole.MarkupLine($"[red]Счёт {account.Name} удалён.[/]");
    }

    private static void CreateCategory(FinanceFacade facade)
    {
        var name = AnsiConsole.Ask<string>("Введите название категории:");
        var typeChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Выберите тип категории:")
                .AddChoices("Доход", "Расход"));
        var type = typeChoice == "Доход" ? OperationType.Income : OperationType.Expense;
        var category = facade.CreateCategory(type, name);
        AnsiConsole.MarkupLine($"[green]Категория [bold]{category.Name}[/] успешно создана.[/]");
    }

    private static void DeleteCategory(FinanceFacade facade)
    {
        var categories = facade.GetAllCategories();
        if (!categories.Any())
        {
            AnsiConsole.MarkupLine("[red]Нет категорий для удаления.[/]");
            return;
        }

        var category = AnsiConsole.Prompt(
            new SelectionPrompt<Category>()
                .Title("Выберите категорию для удаления:")
                .UseConverter(cat => $"{cat.Name} ({(cat.Type == OperationType.Income ? "Доход" : "Расход")})")
                .AddChoices(categories));

        facade.DeleteCategory(category.Id);
        AnsiConsole.MarkupLine($"[red]Категория {category.Name} удалена.[/]");
    }

    private static void CreateOperation(FinanceFacade facade)
    {
        var accounts = facade.GetAllAccounts();
        if (!accounts.Any())
        {
            AnsiConsole.MarkupLine("[red]Сначала создайте счёт.[/]");
            return;
        }

        var account = AnsiConsole.Prompt(
            new SelectionPrompt<BankAccount>()
                .Title("Выберите счёт для операции:")
                .UseConverter(acc => $"{acc.Name} (Баланс: {acc.Balance})")
                .AddChoices(accounts));

        var categories = facade.GetAllCategories();
        if (!categories.Any())
        {
            AnsiConsole.MarkupLine("[red]Сначала создайте категорию.[/]");
            return;
        }

        var category = AnsiConsole.Prompt(
            new SelectionPrompt<Category>()
                .Title("Выберите категорию для операции:")
                .UseConverter(cat => $"{cat.Name} ({(cat.Type == OperationType.Income ? "Доход" : "Расход")})")
                .AddChoices(categories));

        var amount = AnsiConsole.Ask<decimal>("Введите сумму операции:");
        var description = AnsiConsole.Ask<string>("Введите описание операции (необязательно):", "");

        try
        {
            var op = facade.CreateOperation(category.Type, account.Id, amount, category.Id, description);
            AnsiConsole.MarkupLine($"[green]Операция успешно создана. Новый баланс: {account.Balance}.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Ошибка: {ex.Message}[/]");
        }
    }


    private static void ShowAnalytics(FinanceFacade facade)
    {
        var accounts = facade.GetAllAccounts();
        foreach (var acc in accounts) AnsiConsole.MarkupLine($"Счёт: [bold]{acc.Name}[/], Баланс: {acc.Balance}");
        var groups = facade.GroupOperationsByCategory();
        if (groups.Any())
        {
            var table = new Table();
            table.AddColumn("Категория");
            table.AddColumn("Количество операций");
            foreach (var group in groups) table.AddRow(group.Key, group.Value.Count.ToString());
            AnsiConsole.Write(table);
        }
        else
        {
            AnsiConsole.MarkupLine("[yellow]Операций не найдено.[/]");
        }
    }
}