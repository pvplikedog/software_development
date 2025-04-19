namespace minihw2.Domain;

public class FeedingTimeEvent
{
    public FeedingSchedule Schedule { get; }
    public DateTime OccurredOn { get; }

    public FeedingTimeEvent(FeedingSchedule schedule)
    {
        Schedule = schedule;
        OccurredOn = DateTime.UtcNow;
    }
}