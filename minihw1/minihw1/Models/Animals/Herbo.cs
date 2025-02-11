namespace hw1;

public class Herbo : Animal
{
    public int KindnessLevel { get; set; }
    
    public Herbo(string name, int food, int number, int kindnessLevel) : base(name, food, number)
    {
        KindnessLevel = kindnessLevel;
    }
}