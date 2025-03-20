using HseBank.Domain;

namespace HseBank.Factories;

public static class DomainFactory
{
    public static BankAccount CreateBankAccount(string name, decimal balance = 0)
    {
        return new BankAccount(name, balance);
    }

    public static Category CreateCategory(OperationType type, string name)
    {
        return new Category(type, name);
    }

    public static Operation CreateOperation(OperationType type, BankAccount bankAccount, decimal amount,
        Category category, string description = "")
    {
        return new Operation(type, bankAccount, amount, category, description: description);
    }
}