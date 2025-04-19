using minihw2.Domain;
using minihw2.Application;

namespace minihw2.Infrastructure;

public class EnclosureRepository : IEnclosureRepository
{
    private readonly Dictionary<Guid, Enclosure> _store = new Dictionary<Guid, Enclosure>();
    public void Add(Enclosure enclosure) => _store[enclosure.Id] = enclosure;
    public IEnumerable<Enclosure> GetAll() => _store.Values;
    public Enclosure GetById(Guid id) => _store.ContainsKey(id) ? _store[id] : null;
    public void Remove(Guid id) => _store.Remove(id);
}