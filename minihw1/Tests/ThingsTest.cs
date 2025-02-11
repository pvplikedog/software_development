namespace hw1.Tests;

using Xunit;

public class ThingsTest
{
    [Fact]
    public void Computer_Constructor_SetsProperties()
    {
        // Arrange
        var name = "комплюктер";
        var number = 1;

        // Act
        var computer = new Computer(name, number);

        // Assert
        Assert.Equal(name, computer.Name);
        Assert.Equal(number, computer.Number);
    }
    
    [Fact]
    public void Table_Constructor_SetsProperties()
    {
        // Arrange
        var name = "столик";
        var number = 52;

        // Act
        var table = new Table(name, number);

        // Assert
        Assert.Equal(name, table.Name);
        Assert.Equal(number, table.Number);
    }
}