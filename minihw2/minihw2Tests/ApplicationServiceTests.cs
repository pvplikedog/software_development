namespace minihw2Tests;

using System;
using System.Linq;
using Xunit;
using minihw2.Domain;
using minihw2.Application;
using minihw2.Infrastructure;

public class ApplicationServiceTests
{
    [Fact]
    public void AnimalTransferService_TransferAnimal_Succeeds()
    {
        var animalRepo = new AnimalRepository();
        var enclosureRepo = new EnclosureRepository();
        var enclosure1 = new Enclosure("E1", 100, 2);
        var enclosure2 = new Enclosure("E2", 100, 2);
        var animal = new Animal("S", "N", DateTime.Now.AddYears(-1), "F", "Food");
        enclosure1.AddAnimal(animal);
        animalRepo.Add(animal);
        enclosureRepo.Add(enclosure1);
        enclosureRepo.Add(enclosure2);

        var service = new AnimalTransferService(animalRepo, enclosureRepo);
        var evt = service.TransferAnimal(animal.Id, enclosure2.Id);

        Assert.Equal(enclosure2, animal.CurrentEnclosure);
        Assert.Equal(enclosure2, evt.NewEnclosure);
    }

    [Fact]
    public void FeedingOrganizationService_ExecuteFeeding_MarksFeedingDoneAndFeedsAnimal()
    {
        var feedRepo = new FeedingScheduleRepository();
        var animal = new Animal("S", "N", DateTime.Now.AddYears(-1), "M", "Food");
        var schedule = new FeedingSchedule(animal, DateTime.Now.AddHours(1), "Food");
        feedRepo.Add(schedule);

        var service = new FeedingOrganizationService(feedRepo);
        var evt = service.ExecuteFeeding(schedule.Id);

        Assert.True(schedule.IsDone);
        Assert.Equal(schedule, evt.Schedule);
    }

    [Fact]
    public void FeedingOrganizationService_UpdateFeedingSchedule_ChangesValues()
    {
        var feedRepo = new FeedingScheduleRepository();
        var schedule = new FeedingSchedule(
            new Animal("S", "N", DateTime.Now.AddYears(-1), "F", "Food1"),
            DateTime.Today.AddHours(8),
            "Food1"
        );
        feedRepo.Add(schedule);
        var service = new FeedingOrganizationService(feedRepo);

        var newTime = DateTime.Today.AddHours(15);
        var newFood = "Food3";
        service.UpdateFeedingSchedule(schedule.Id, newTime, newFood);

        Assert.Equal(newTime, schedule.FeedingTime);
        Assert.Equal(newFood, schedule.FoodType);
    }

    [Fact]
    public void ZooStatisticsService_GetZooStatistics_ReturnsCorrectCounts()
    {
        var animalRepo = new AnimalRepository();
        var enclosureRepo = new EnclosureRepository();
        // Setup
        var animal1 = new Animal("S1", "A1", DateTime.Now.AddYears(-1), "M", "F");
        var animal2 = new Animal("S2", "A2", DateTime.Now.AddYears(-2), "F", "F");
        animalRepo.Add(animal1);
        animalRepo.Add(animal2);
        var enclosure1 = new Enclosure("T1", 100, 2);
        var enclosure2 = new Enclosure("T2", 100, 1);
        enclosure1.AddAnimal(animal1);
        enclosure2.AddAnimal(animal2);
        enclosureRepo.Add(enclosure1);
        enclosureRepo.Add(enclosure2);

        var service = new ZooStatisticsService(animalRepo, enclosureRepo);
        var (totalAnimals, freeEnclosures) = service.GetZooStatistics();

        Assert.Equal(2, totalAnimals);
        Assert.Equal(1, freeEnclosures); // оба вольера заполнены
    }
}
