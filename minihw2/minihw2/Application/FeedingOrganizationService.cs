using minihw2.Domain;

namespace minihw2.Application;

public class FeedingOrganizationService
{
    private readonly IFeedingScheduleRepository _feedingRepository;

    public FeedingOrganizationService(IFeedingScheduleRepository feedingRepository)
    {
        _feedingRepository = feedingRepository;
    }

    public FeedingTimeEvent ExecuteFeeding(Guid scheduleId)
    {
        var schedule = _feedingRepository.GetById(scheduleId);
        schedule.Animal.Feed();
        schedule.MarkFeedingAsDone();
        return new FeedingTimeEvent(schedule);
    }

    public void UpdateFeedingSchedule(Guid scheduleId, DateTime newTime, string newFoodType)
    {
        var schedule = _feedingRepository.GetById(scheduleId);
        schedule.UpdateSchedule(newTime, newFoodType);
    }
}