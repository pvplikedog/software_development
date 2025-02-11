namespace hw1;

public abstract class Animal : IAlive, IInventory
{
    public int Food { get; set; }
    public int Number { get; set; }
    public string Name { get; set; }

    protected Animal(string name, int food, int number)
    {
        Name = name;
        Food = food;
        Number = number;
    }
    
    
}