namespace minihw2.Application;

public class ZooStatisticsService
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IEnclosureRepository _enclosureRepository;

    public ZooStatisticsService(IAnimalRepository animalRepository, IEnclosureRepository enclosureRepository)
    {
        _animalRepository = animalRepository;
        _enclosureRepository = enclosureRepository;
    }

    public (int totalAnimals, int freeEnclosures) GetZooStatistics()
    {
        var totalAnimals = _animalRepository.GetAll().Count();
        var freeEnclosures = _enclosureRepository.GetAll().Count(e => e.CurrentCount < e.Capacity);
        return (totalAnimals, freeEnclosures);
    }
}