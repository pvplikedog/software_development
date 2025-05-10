using FluentAssertions;
using HseBank.Domain;
using HseBank.Repositories;
using HseBank.Services;
using Moq;

namespace HseBankTest;

public class FinanceFacadeTests
{
    private readonly FinanceFacade _facade;
    private readonly Mock<DataRepository> _repositoryMock;
    private readonly RepositoryProxy _repositoryProxy;

    public FinanceFacadeTests()
    {
        _repositoryMock = new Mock<DataRepository>();
        _repositoryProxy = new RepositoryProxy(_repositoryMock.Object);
        _facade = new FinanceFacade(_repositoryProxy);
    }

    [Fact]
    public void CreateAccount_ShouldAddAccount()
    {
        var account = _facade.CreateAccount("Мой счёт", 1000);

        account.Should().NotBeNull();
        account.Balance.Should().Be(1000);
        _repositoryProxy.Get("accounts", account.Id).Should().Be(account);
    }

    [Fact]
    public void CreateCategory_ShouldAddCategory()
    {
        var category = _facade.CreateCategory(OperationType.Expense, "Кафе");

        category.Should().NotBeNull();
        category.Type.Should().Be(OperationType.Expense);
        _repositoryProxy.Get("categories", category.Id).Should().Be(category);
    }

    [Fact]
    public void CreateOperation_ShouldUpdateBalance()
    {
        var account = new BankAccount("Тестовый счёт", 1000);
        var category = new Category(OperationType.Expense, "Кафе");

        _repositoryProxy.Save("accounts", account.Id, account);
        _repositoryProxy.Save("categories", category.Id, category);

        var operation = _facade.CreateOperation(OperationType.Expense, account.Id, 200, category.Id, "Ужин в кафе");

        operation.Should().NotBeNull();
        operation.Amount.Should().Be(200);
        account.Balance.Should().Be(800);
    }

    [Fact]
    public void GetBalanceDifference_ShouldReturnCorrectSum()
    {
        var account = new BankAccount("Счёт", 1000);
        var category = new Category(OperationType.Income, "Зарплата");
        var operation1 = new Operation(OperationType.Income, account, 500, category);
        var operation2 = new Operation(OperationType.Expense, account, 200, category);

        _repositoryProxy.Save("operations", operation1.Id, operation1);
        _repositoryProxy.Save("operations", operation2.Id, operation2);

        var balanceDiff = _facade.GetBalanceDifference(DateTime.MinValue, DateTime.MaxValue);

        balanceDiff.Should().Be(300);
    }
}