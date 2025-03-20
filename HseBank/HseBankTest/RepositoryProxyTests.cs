using FluentAssertions;
using HseBank.Domain;
using HseBank.Repositories;

namespace HseBankTest;

public class RepositoryProxyTests
{
    private readonly RepositoryProxy _repositoryProxy;

    public RepositoryProxyTests()
    {
        _repositoryProxy = new RepositoryProxy(new DataRepository());
    }

    [Fact]
    public void Save_ShouldStoreAndRetrieveData()
    {
        var account = new BankAccount("Тестовый счёт", 1000);

        _repositoryProxy.Save("accounts", account.Id, account);
        var retrievedAccount = _repositoryProxy.Get("accounts", account.Id) as BankAccount;

        retrievedAccount.Should().NotBeNull();
        retrievedAccount.Name.Should().Be("Тестовый счёт");
        retrievedAccount.Balance.Should().Be(1000);
    }

    [Fact]
    public void Get_ShouldReturnNullIfNotFound()
    {
        var result = _repositoryProxy.Get("accounts", Guid.NewGuid());

        result.Should().BeNull();
    }

    [Fact]
    public void GetAll_ShouldReturnAllStoredData()
    {
        var account1 = new BankAccount("Счёт 1", 1000);
        var account2 = new BankAccount("Счёт 2", 500);

        _repositoryProxy.Save("accounts", account1.Id, account1);
        _repositoryProxy.Save("accounts", account2.Id, account2);

        var accounts = _repositoryProxy.GetAll("accounts").Cast<BankAccount>().ToList();

        accounts.Should().HaveCount(2);
        accounts.Should().Contain(a => a.Name == "Счёт 1");
        accounts.Should().Contain(a => a.Name == "Счёт 2");
    }

    [Fact]
    public void Delete_ShouldRemoveData()
    {
        var account = new BankAccount("Удаляемый счёт", 1000);
        _repositoryProxy.Save("accounts", account.Id, account);

        _repositoryProxy.Delete("accounts", account.Id);
        var retrievedAccount = _repositoryProxy.Get("accounts", account.Id);

        retrievedAccount.Should().BeNull();
    }

    [Fact]
    public void Clear_ShouldRemoveAllDataForKey()
    {
        var account1 = new BankAccount("Счёт 1", 1000);
        var account2 = new BankAccount("Счёт 2", 500);
        _repositoryProxy.Save("accounts", account1.Id, account1);
        _repositoryProxy.Save("accounts", account2.Id, account2);

        _repositoryProxy.Clear("accounts");
        var accounts = _repositoryProxy.GetAll("accounts");

        accounts.Should().BeEmpty();
    }
}