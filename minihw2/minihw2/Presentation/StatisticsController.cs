namespace minihw2.Presentation;

using Microsoft.AspNetCore.Mvc;
using minihw2.Application;
using minihw2.Domain;
using System;

[ApiController]
[Route("api/statistics")]
public class StatisticsController : ControllerBase
{
    private readonly ZooStatisticsService _zooStatisticsService;

    public StatisticsController(ZooStatisticsService zooStatisticsService)
    {
        _zooStatisticsService = zooStatisticsService;
    }

    [HttpGet]
    public IActionResult GetZooStatistics()
    {
        var stats = _zooStatisticsService.GetZooStatistics();
        return Ok(new 
        { 
            TotalAnimals = stats.totalAnimals, 
            FreeEnclosures = stats.freeEnclosures 
        });
    }
}