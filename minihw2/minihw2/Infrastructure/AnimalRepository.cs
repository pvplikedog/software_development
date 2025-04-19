using minihw2.Domain;
using minihw2.Application;

namespace minihw2.Infrastructure;

public class AnimalRepository : IAnimalRepository
{
    private readonly Dictionary<Guid, Animal> _store = new Dictionary<Guid, Animal>();
    public void Add(Animal animal) => _store[animal.Id] = animal;
    public IEnumerable<Animal> GetAll() => _store.Values;
    public Animal GetById(Guid id) => _store.ContainsKey(id) ? _store[id] : null;
    public void Remove(Guid id) => _store.Remove(id);
}