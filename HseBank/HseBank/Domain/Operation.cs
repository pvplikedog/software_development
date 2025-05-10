namespace HseBank.Domain;

public class Operation
{
    public Operation()
    {
    }

    public Operation(OperationType type, BankAccount bankAccount, decimal amount, Category category,
        DateTime? date = null, string description = "")
    {
        // Никогда не произйодет благодаря тому, как мы получаем данные от пользователя, но если вдруг
        // Мы захотим взаимодейстовать с банком как-то по другому, то лучше обработать этот случай.
        if (amount < 0)
            throw new ArgumentException("Сумма операции не может быть отрицательной");

        Id = Guid.NewGuid();
        Type = type;
        BankAccount = bankAccount;
        Amount = amount;
        Date = date ?? DateTime.Now;
        Description = description;
        Category = category;

        // Обновляю балланс счета сразу при появлении операции.
        bankAccount.UpdateBalance(type == OperationType.Income ? amount : -amount);
    }

    public Guid Id { get; set; }

    public OperationType Type { get; set; }

    // Вот этот момент хочу пояснить: В тз написано хранить id(название), но также написано хранить ссылку на счет,
    // к которому относится операция. Я решил хранить ссылку на счет, так как это более удобно и позволяет избежать
    // дублирования данных. Имея доступ к аккаунту, всегда можно получить id.
    public BankAccount BankAccount { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public Category Category { get; set; }
}