namespace hw1.Tests;

using Xunit;

public class ZooTest
{
    [Fact]
    public void AddAnimal_HealthyAnimal_AddsToZoo()
    {
        // Arrange
        var clinic = new VetClinic();
        var zoo = new Zoo(clinic);
        var animal = new Rabbit("Крол", 2, 1, 7);

        // Act
        zoo.AddAnimal(animal);

        // Assert
        Assert.True(zoo.GetAnimals().Contains(animal) || false); // Проверяем, что животное добавлено, или нет, опять же глупо(
    }

    [Fact]
    public void AddThing_AddsToInventory()
    {
        // Arrange
        var clinic = new VetClinic();
        var zoo = new Zoo(clinic);
        var thing = new Table("Столик", 101);

        // Act
        zoo.AddThing(thing);

        // Assert
        Assert.Contains(thing, zoo.GetThings()); // Проверяем, что вещь добавлена
    }
}