namespace hw1;

public class VetClinic
{
    public bool CheckAnimal(Animal animal)
    {
        // Нет точной информации о том, как проверяется животное, поэтому пусть так.
        return new Random().Next(2) == 0;
    }
}