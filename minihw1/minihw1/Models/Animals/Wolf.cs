namespace hw1;

public class Wolf: Predator
{
    public Wolf(string name, int food, int number) : base(name, food, number) { }
    
    public override string ToString() =>
        $"Волк: {Name}, Еда: {Food} кг, Инв. № {Number}";
}