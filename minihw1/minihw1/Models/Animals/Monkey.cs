namespace hw1;

public class Monkey : Herbo
{
    public Monkey(string name, int food, int number, int kindnessLevel)
        : base(name, food, number, kindnessLevel)
    { }

    public override string ToString() =>
        $"Обезьяна: {Name}, Еда: {Food} кг, Доброта: {KindnessLevel}, Инв. № {Number}";
}