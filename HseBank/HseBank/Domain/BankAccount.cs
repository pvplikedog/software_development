namespace HseBank.Domain;

public class BankAccount
{
    public BankAccount()
    {
    }

    public BankAccount(string name, decimal balance = 0)
    {
        Id = Guid.NewGuid();
        Name = name;
        Balance = balance;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; }

    public void UpdateBalance(decimal amount)
    {
        Balance += amount;
    }

    public override string ToString()
    {
        return $"{Name} (Баланс: {Balance})";
    }
}