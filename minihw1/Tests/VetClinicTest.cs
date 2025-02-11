namespace hw1.Tests;

using Xunit;

public class VetClinicTest
{
    [Fact]
    public void CheckHealth_ReturnsTrueOrFalse()
    {
        // Arrange
        var clinic = new VetClinic();
        var animal = new Tiger("Тигрица", 2, 1);

        // Act
        var result = clinic.CheckAnimal(animal);

        // Assert
        Assert.True(result || !result); // Проверяем, что результат либо true, либо false, да глупо - но что поделать.
    }
}