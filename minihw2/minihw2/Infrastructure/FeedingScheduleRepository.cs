using minihw2.Domain;
using minihw2.Application;

namespace minihw2.Infrastructure;

public class FeedingScheduleRepository : IFeedingScheduleRepository
{
    private readonly Dictionary<Guid, FeedingSchedule> _store = new Dictionary<Guid, FeedingSchedule>();
    public void Add(FeedingSchedule schedule) => _store[schedule.Id] = schedule;
    public IEnumerable<FeedingSchedule> GetAll() => _store.Values;
    public FeedingSchedule GetById(Guid id) => _store.ContainsKey(id) ? _store[id] : null;
    public void Remove(Guid id) => _store.Remove(id);
}