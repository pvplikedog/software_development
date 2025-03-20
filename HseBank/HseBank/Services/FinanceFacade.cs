using HseBank.DataImport;
using HseBank.Domain;
using HseBank.Factories;
using HseBank.Repositories;

namespace HseBank.Services;

public class FinanceFacade
{
    private readonly RepositoryProxy _repositoryProxy;

    public FinanceFacade(RepositoryProxy repositoryProxy)
    {
        _repositoryProxy = repositoryProxy;
    }

    public BankAccount CreateAccount(string name, decimal balance = 0)
    {
        var account = DomainFactory.CreateBankAccount(name, balance);
        _repositoryProxy.Save("accounts", account.Id, account);
        return account;
    }

    public void DeleteAccount(Guid accountId)
    {
        _repositoryProxy.Delete("accounts", accountId);
    }

    public Category CreateCategory(OperationType type, string name)
    {
        var category = DomainFactory.CreateCategory(type, name);
        _repositoryProxy.Save("categories", category.Id, category);
        return category;
    }

    public void DeleteCategory(Guid categoryId)
    {
        _repositoryProxy.Delete("categories", categoryId);
    }

    public Operation CreateOperation(OperationType type, Guid accountId, decimal amount, Guid categoryId,
        string description = "")
    {
        var account = _repositoryProxy.Get("accounts", accountId) as BankAccount;
        var category = _repositoryProxy.Get("categories", categoryId) as Category;
        if (account == null || category == null)
            throw new ArgumentException("Некорректный id счета или категории");

        var operation = DomainFactory.CreateOperation(type, account, amount, category, description);
        _repositoryProxy.Save("operations", operation.Id, operation);
        return operation;
    }

    public decimal GetBalanceDifference(DateTime startDate, DateTime endDate)
    {
        var operations = GetAllOperations();
        return operations.Where(op => op.Date >= startDate && op.Date <= endDate)
            .Sum(op => op.Type == OperationType.Income ? op.Amount : -op.Amount);
    }

    public IEnumerable<BankAccount> GetAllAccounts()
    {
        var accounts = _repositoryProxy.GetAll("accounts");
        return accounts.Cast<BankAccount>();
    }

    public IEnumerable<Category> GetAllCategories()
    {
        var categories = _repositoryProxy.GetAll("categories");
        return categories.Cast<Category>();
    }

    public IEnumerable<Operation> GetAllOperations()
    {
        var operations = _repositoryProxy.GetAll("operations");
        return operations.Cast<Operation>();
    }

    public Dictionary<string, List<Operation>> GroupOperationsByCategory()
    {
        return GetAllOperations()
            .GroupBy(op => op.Category.Name)
            .ToDictionary(g => g.Key, g => g.ToList());
    }

    public void LoadData(JsonImporter.ImportData importData)
    {
        _repositoryProxy.Clear("accounts");
        _repositoryProxy.Clear("categories");
        _repositoryProxy.Clear("operations");

        foreach (var account in importData.Accounts) _repositoryProxy.Save("accounts", account.Id, account);
        foreach (var category in importData.Categories) _repositoryProxy.Save("categories", category.Id, category);
        foreach (var operation in importData.Operations) _repositoryProxy.Save("operations", operation.Id, operation);
    }
}