using minihw2.Domain;

namespace minihw2.Application;

public interface IEnclosureRepository
{
    Enclosure GetById(Guid id);
    void Add(Enclosure enclosure);
    void Remove(Guid id);
    IEnumerable<Enclosure> GetAll();
}