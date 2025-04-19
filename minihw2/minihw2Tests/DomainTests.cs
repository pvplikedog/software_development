namespace minihw2Tests;

using System;
using System.Linq;
using Xunit;
using minihw2.Domain;
using minihw2.Application;
using minihw2.Infrastructure;

public class DomainTests
{
    [Fact]
    public void Enclosure_AddAnimal_WhenCapacityExceeded_ThrowsInvalidOperationException()
    {
        var enclosure = new Enclosure("Test", 50, 1);
        var animal1 = new Animal("Species", "A1", DateTime.Now.AddYears(-1), "M", "Food");
        var animal2 = new Animal("Species", "A2", DateTime.Now.AddYears(-1), "F", "Food");
        enclosure.AddAnimal(animal1);
        Assert.Throws<InvalidOperationException>(() => enclosure.AddAnimal(animal2));
    }

    [Fact]
    public void Animal_MoveTo_UpdatesCurrentEnclosureAndGeneratesEvent()
    {
        var enclosure1 = new Enclosure("Type1", 100, 5);
        var enclosure2 = new Enclosure("Type2", 200, 5);
        var animal = new Animal("Species", "N", DateTime.Now.AddYears(-2), "M", "Food");
        animal.MoveTo(enclosure1);

        var evt = animal.MoveTo(enclosure2);

        Assert.Equal(enclosure2, animal.CurrentEnclosure);
        Assert.Equal(enclosure1, evt.OldEnclosure);
        Assert.Equal(enclosure2, evt.NewEnclosure);
        Assert.True(evt.OccurredOn <= DateTime.UtcNow);
    }

    [Fact]
    public void FeedingSchedule_UpdateSchedule_ChangesTimeAndFoodType()
    {
        var animal = new Animal("Species", "N", DateTime.Now.AddYears(-1), "F", "Food1");
        var schedule = new FeedingSchedule(animal, DateTime.Today.AddHours(9), "Food1");
        var newTime = DateTime.Today.AddHours(12);
        var newFood = "Food2";

        schedule.UpdateSchedule(newTime, newFood);

        Assert.Equal(newTime, schedule.FeedingTime);
        Assert.Equal(newFood, schedule.FoodType);
    }

    [Fact]
    public void FeedingSchedule_MarkFeedingAsDone_SetsIsDoneTrue()
    {
        var schedule = new FeedingSchedule(
            new Animal("Species", "N", DateTime.Now.AddYears(-1), "M", "Food"),
            DateTime.Now,
            "Food"
        );
        Assert.False(schedule.IsDone);

        schedule.MarkFeedingAsDone();

        Assert.True(schedule.IsDone);
    }
}