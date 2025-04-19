namespace minihw2.Domain;

public class Enclosure
{
    public Guid Id { get; }
    public string Type { get; }
    public double Size { get; }
    public int CurrentCount => Animals.Count;
    public int Capacity { get; }
    private readonly List<Animal> Animals;
    
    public Enclosure(string type, double size, int capacity)
    {
        Id = Guid.NewGuid();
        Type = type;
        Size = size;
        Capacity = capacity;
        Animals = new List<Animal>();
    }

    public void AddAnimal(Animal animal)
    {
        if (Animals.Count >= Capacity)
            throw new InvalidOperationException("Вольер переполнен.");
        Animals.Add(animal);
    }

    public void RemoveAnimal(Animal animal)
    {
        Animals.Remove(animal);
    }

    public void Clean()
    {
        // Реализация уборки вольера.
    }
}