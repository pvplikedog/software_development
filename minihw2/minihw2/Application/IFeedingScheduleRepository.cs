using minihw2.Domain;

namespace minihw2.Application;

public interface IFeedingScheduleRepository
{
    FeedingSchedule GetById(Guid id);
    void Add(FeedingSchedule schedule);
    void Remove(Guid id);
    IEnumerable<FeedingSchedule> GetAll();
}