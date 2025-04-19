namespace minihw2.Domain;

public class Animal
{
    public Guid Id { get; }
    public string Species { get; }
    public string Nickname { get; }
    public DateTime BirthDate { get; }
    public string Gender { get; }
    public string FavoriteFood { get; }
    public bool IsHealthy { get; private set; }
    public Enclosure CurrentEnclosure { get; private set; }
    
    public Animal(string species, string nickname, DateTime birthDate, string gender, string favoriteFood)
    {
        Id = Guid.NewGuid();
        Species = species;
        Nickname = nickname;
        BirthDate = birthDate;
        Gender = gender;
        FavoriteFood = favoriteFood;
        IsHealthy = true;
    }

    public void Feed()
    {
        // Логика кормления животного.
    }

    public void Treat()
    {
        IsHealthy = true;
        // Дополнительная логика лечения.
    }
    
    public AnimalMovedEvent MoveTo(Enclosure newEnclosure)
    {
        if (CurrentEnclosure != null)
        {
            CurrentEnclosure.RemoveAnimal(this);
        }
        newEnclosure.AddAnimal(this);
        var oldEnclosure = CurrentEnclosure;
        CurrentEnclosure = newEnclosure;
        // Генерация доменного события перемещения животного
        return new AnimalMovedEvent(this, oldEnclosure, newEnclosure);
    }
}