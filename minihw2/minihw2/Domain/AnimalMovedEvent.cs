namespace minihw2.Domain;

public class AnimalMovedEvent
{
    public Animal Animal { get; }
    public Enclosure OldEnclosure { get; }
    public Enclosure NewEnclosure { get; }
    public DateTime OccurredOn { get; } // Точно ли нужно?

    public AnimalMovedEvent(Animal animal, Enclosure oldEnclosure, Enclosure newEnclosure)
    {
        Animal = animal;
        OldEnclosure = oldEnclosure;
        NewEnclosure = newEnclosure;
        OccurredOn = DateTime.UtcNow;
    }
}