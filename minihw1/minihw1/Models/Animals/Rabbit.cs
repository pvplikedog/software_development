namespace hw1;

public class Rabbit: Herbo
{
    public Rabbit(string name, int food, int number, int kindnessLevel)
        : base(name, food, number, kindnessLevel)
    { }

    public override string ToString() =>
        $"Кролик: {Name}, Еда: {Food} кг, Доброта: {KindnessLevel}, Инв. № {Number}";
}