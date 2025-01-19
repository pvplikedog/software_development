namespace homework1;

public class Customer
{
    public string Name { get; set; }
    
    public Car? Car { get; set; }
    
    public override string ToString()
    {
        return $"Customer's name is {Name}, and he has {(Car == null ? "no car" : $"car with number {Car.Number}")}";
    }
}