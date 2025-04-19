namespace minihw2.Presentation;

using Microsoft.AspNetCore.Mvc;
using minihw2.Application;
using minihw2.Domain;
using System;

[ApiController]
[Route("api/feeding")]
public class FeedingController : ControllerBase
{
    private readonly FeedingOrganizationService _feedingOrganizationService;

    public FeedingController(FeedingOrganizationService feedingOrganizationService)
    {
        _feedingOrganizationService = feedingOrganizationService;
    }

    // Выполнение кормления по идентификатору расписания
    [HttpPost("{scheduleId}/execute")]
    public IActionResult ExecuteFeeding(Guid scheduleId)
    {
        try
        {
            var feedingEvent = _feedingOrganizationService.ExecuteFeeding(scheduleId);
            return Ok(new 
            { 
                Message = "Кормление выполнено",
                FeedingTime = feedingEvent.OccurredOn
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    // Обновление расписания кормления
    [HttpPost("{scheduleId}/update")]
    public IActionResult UpdateFeedingSchedule(
        Guid scheduleId, 
        [FromBody] FeedingScheduleDto dto)
    {
        try
        {
            _feedingOrganizationService.UpdateFeedingSchedule(scheduleId, dto.FeedingTime, dto.FoodType);
            return Ok(new { Message = "Расписание обновлено" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}

public class FeedingScheduleDto
{
    public DateTime FeedingTime { get; set; }
    public string FoodType { get; set; }
}