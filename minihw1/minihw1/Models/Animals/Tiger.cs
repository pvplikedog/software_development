namespace hw1;

public class Tiger : Predator
{
    public Tiger(string name, int food, int number) : base(name, food, number) { }
    
    public override string ToString() =>
        $"Тигр: {Name}, Еда: {Food} кг, Инв. № {Number}";
}