namespace hw1;

public abstract class Thing : IInventory
{
    public int Number { get; set; }
    public string Name { get; set; }

    protected Thing(string name, int number)
    {
        Name = name;
        Number = number;
    }
}