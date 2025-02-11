namespace hw1;

public class Table : Thing
{
    public Table(string name, int number) : base(name, number) { }
    
    public override string ToString() =>
        $"Стол: {Name}, Инв. № {Number}";
}