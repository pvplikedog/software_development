namespace homework1;

public class Car
{
    private static Random _random = new();
    
    public required int Number { get; set; }
    
    public Engine Engine { get; init; }

    public Car()
    {
        Engine = new Engine { PedalSize = _random.Next(1, 10) };
    }

    public override string ToString()
    {
        return $"Car number #{Number} with pedal size of {Engine.PedalSize}";
    }
}