namespace minihw2.Presentation;

using Microsoft.AspNetCore.Mvc;
using minihw2.Application;
using minihw2.Domain;
using System;

[ApiController]
[Route("api/enclosures")]
public class EnclosuresController : ControllerBase
{
    private readonly IEnclosureRepository _enclosureRepository;
    private readonly IAnimalRepository _animalRepository;

    public EnclosuresController(
        IEnclosureRepository enclosureRepository,
        IAnimalRepository animalRepository)
    {
        _enclosureRepository = enclosureRepository;
        _animalRepository = animalRepository;
    }

    // GET: /api/enclosures
    [HttpGet]
    public IActionResult GetAllEnclosures() =>
        Ok(_enclosureRepository.GetAll());

    // GET: /api/enclosures/{id}
    [HttpGet("{id}")]
    public IActionResult GetEnclosure(Guid id)
    {
        var enclosure = _enclosureRepository.GetById(id);
        return enclosure != null ? Ok(enclosure) : NotFound();
    }

    // POST: /api/enclosures
    [HttpPost]
    public IActionResult AddEnclosure([FromBody] EnclosureDto dto)
    {
        var enclosure = new Enclosure(dto.Type, dto.Size, dto.Capacity);
        _enclosureRepository.Add(enclosure);
        return CreatedAtAction(nameof(GetEnclosure), new { id = enclosure.Id }, enclosure);
    }

    // DELETE: /api/enclosures/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteEnclosure(Guid id)
    {
        _enclosureRepository.Remove(id);
        return NoContent();
    }

    // POST: /api/enclosures/{enclosureId}/animals/{animalId}
    // Добавить животное в вольер
    [HttpPost("{enclosureId}/animals/{animalId}")]
    public IActionResult AddAnimalToEnclosure(Guid enclosureId, Guid animalId)
    {
        var enclosure = _enclosureRepository.GetById(enclosureId);
        var animal = _animalRepository.GetById(animalId);

        if (enclosure == null || animal == null)
            return NotFound(new { Error = "Вольер или животное не найдены." });

        try
        {
            enclosure.AddAnimal(animal);
            return Ok(new { Message = "Животное добавлено в вольер." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    // DELETE: /api/enclosures/{enclosureId}/animals/{animalId}
    // Убрать животное из вольера
    [HttpDelete("{enclosureId}/animals/{animalId}")]
    public IActionResult RemoveAnimalFromEnclosure(Guid enclosureId, Guid animalId)
    {
        var enclosure = _enclosureRepository.GetById(enclosureId);
        var animal = _animalRepository.GetById(animalId);

        if (enclosure == null || animal == null)
            return NotFound(new { Error = "Вольер или животное не найдены." });

        enclosure.RemoveAnimal(animal);
        return Ok(new { Message = "Животное удалено из вольера." });
    }
}

// DTO для создания вольера
public class EnclosureDto
{
    public string Type { get; set; }
    public double Size { get; set; }
    public int Capacity { get; set; }
}