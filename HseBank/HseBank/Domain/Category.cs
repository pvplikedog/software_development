namespace HseBank.Domain;

public class Category
{
    public Category()
    {
    }

    public Category(OperationType type, string name)
    {
        Id = Guid.NewGuid();
        Type = type;
        Name = name;
    }

    public Guid Id { get; set; }
    public OperationType Type { get; set; }
    public string Name { get; set; }
}