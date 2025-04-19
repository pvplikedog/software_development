using minihw2.Domain;

namespace minihw2.Application;

public class AnimalTransferService
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IEnclosureRepository _enclosureRepository;

    public AnimalTransferService(IAnimalRepository animalRepository, IEnclosureRepository enclosureRepository)
    {
        _animalRepository = animalRepository;
        _enclosureRepository = enclosureRepository;
    }

    public AnimalMovedEvent TransferAnimal(Guid animalId, Guid newEnclosureId)
    {
        var animal = _animalRepository.GetById(animalId);
        var newEnclosure = _enclosureRepository.GetById(newEnclosureId);
        var animalMovedEvent = animal.MoveTo(newEnclosure);
        // Возможно, сохраняем изменения в репозитории
        return animalMovedEvent;
    }
}