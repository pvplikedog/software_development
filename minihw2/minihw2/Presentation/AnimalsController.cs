namespace minihw2.Presentation;

using Microsoft.AspNetCore.Mvc;
using minihw2.Application;
using minihw2.Domain;
using System;

[ApiController]
[Route("api/animals")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IEnclosureRepository _enclosureRepository;
    private readonly AnimalTransferService _animalTransferService;
    
    public AnimalsController(
        IAnimalRepository animalRepository,
        IEnclosureRepository enclosureRepository,
        AnimalTransferService animalTransferService)
    {
        _animalRepository = animalRepository;
        _enclosureRepository = enclosureRepository;
        _animalTransferService = animalTransferService;
    }

    // Пример: GET для получения списка животных
    [HttpGet]
    public IActionResult GetAllAnimals() => Ok(_animalRepository.GetAll());

    // Пример: POST для добавления животного (DTO аналогичный предыдущему примеру)
    [HttpPost]
    public IActionResult AddAnimal([FromBody] AnimalDto dto)
    {
        var animal = new Animal(dto.Species, dto.Nickname, dto.BirthDate, dto.Gender, dto.FavoriteFood);
        _animalRepository.Add(animal);
        return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animal);
    }

    [HttpGet("{id}")]
    public IActionResult GetAnimal(Guid id)
    {
        var animal = _animalRepository.GetById(id);
        return animal is not null ? Ok(animal) : NotFound();
    }

    // Новый endpoint для перемещения животного
    [HttpPost("{animalId}/transfer/{enclosureId}")]
    public IActionResult TransferAnimal(Guid animalId, Guid enclosureId)
    {
        try
        {
            var transferEvent = _animalTransferService.TransferAnimal(animalId, enclosureId);
            return Ok(new 
            { 
                Message = "Животное успешно перемещено", 
                AnimalId = transferEvent.Animal.Id,
                From = transferEvent.OldEnclosure?.Id,
                To = transferEvent.NewEnclosure.Id,
                OccurredOn = transferEvent.OccurredOn
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}

public class AnimalDto
{
    public string Species { get; set; }
    public string Nickname { get; set; }
    public DateTime BirthDate { get; set; }
    public string Gender { get; set; }
    public string FavoriteFood { get; set; }
}
