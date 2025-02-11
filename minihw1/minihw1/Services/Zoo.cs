namespace hw1;

public class Zoo
{
    private List<Animal> _animals = new List<Animal>();
    private List<Thing> _things = new List<Thing>();
    private VetClinic _vetClinic;
    
    public Zoo(VetClinic vetClinic)
    {
        _vetClinic = vetClinic;
    }

    public List<Animal> GetAnimals()
    {
        return _animals;
    }
    
    public List<Thing> GetThings()
    {
        return _things;
    }
    
    public void AddAnimal(Animal animal)
    {
        if (_vetClinic.CheckAnimal(animal))
        {
            _animals.Add(animal);
            Console.WriteLine($"{animal.Name} добавлен в зоопарк.");
        }
        else
        {
            Console.WriteLine($"{animal.Name} не принят в зоопарк из-за проблем со здоровьем.");
        }
    }
    
    public void AddThing(Thing thing)
    {
        _things.Add(thing);
        Console.WriteLine($"{thing.Name} добавлен в инвентарь.");
    }
    
    public void Report()
    {
        Console.WriteLine("Отчет по зоопарку:");
        Console.WriteLine($"Количество животных: {_animals.Count}");
        Console.WriteLine($"Общее количество еды в день: {_animals.Sum(a => a.Food)} кг");

        var contactZooAnimals = _animals.OfType<Herbo>().Where(h => h.KindnessLevel > 5);
        Console.WriteLine("Животные для контактного зоопарка:");
        foreach (var animal in contactZooAnimals)
        {
            Console.WriteLine(animal);
        }

        Console.WriteLine("Инвентарь:");
        foreach (var thing in _things)
        {
            Console.WriteLine(thing);
        }
        
        Console.WriteLine("Все животные:");
        foreach (var animal in _animals)
        {
            Console.WriteLine(animal);
        }
    }
}