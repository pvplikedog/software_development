namespace hw1;

public class Computer : Thing
{
    public Computer(string name, int number) : base(name, number) { }
    
    public override string ToString() =>
        $"Компьютер: {Name}, Инв. № {Number}";
}