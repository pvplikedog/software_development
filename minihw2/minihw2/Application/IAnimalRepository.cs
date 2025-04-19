using minihw2.Domain;

namespace minihw2.Application;

public interface IAnimalRepository
{
    Animal GetById(Guid id);
    void Add(Animal animal);
    void Remove(Guid id);
    IEnumerable<Animal> GetAll();
}