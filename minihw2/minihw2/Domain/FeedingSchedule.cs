namespace minihw2.Domain;

public class FeedingSchedule
{
    public Guid Id { get; }
    public Animal Animal { get; }
    public DateTime FeedingTime { get; private set; }
    public string FoodType { get; private set; }
    public bool IsDone { get; private set; }

    public FeedingSchedule(Animal animal, DateTime feedingTime, string foodType)
    {
        Id = Guid.NewGuid();
        Animal = animal;
        FeedingTime = feedingTime;
        FoodType = foodType;
        IsDone = false;
    }

    public void UpdateSchedule(DateTime newTime, string newFoodType)
    {
        FeedingTime = newTime;
        FoodType = newFoodType;
    }

    public void MarkFeedingAsDone()
    {
        IsDone = true;
        // Генерация доменного события времени кормления
        // (если требуется, можно возвращать FeedingTimeEvent)
    }
}