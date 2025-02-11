namespace hw1;

using Spectre.Console;

public class Menu
{
    private Zoo _zoo;
    
    public Menu(Zoo zoo)
    {
        _zoo = zoo;
    }

    public void Handle()
    {
        while (true)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText("Московский Зоопарк")
                    .LeftJustified()
                    .Color(Color.Green));

            // Создание меню
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Что вы хотите сделать?")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Добавить животное",
                        "Добавить вещь",
                        "Показать отчет",
                        "Выйти"
                    }));

            // Обработка выбора пользователя
            switch (choice)
            {
                case "Добавить животное":
                    AddAnimal(_zoo);
                    break;

                case "Добавить вещь":
                    AddThing(_zoo);
                    break;

                case "Показать отчет":
                    _zoo.Report();
                    AnsiConsole.WriteLine("Нажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                    break;

                case "Выйти":
                    AnsiConsole.WriteLine("До свидания!");
                    return;
            }
        }
    }
    
    private static void AddAnimal(Zoo zoo)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(
            new FigletText("Добавить животное")
                .LeftJustified()
                .Color(Color.Blue));

        var animalType = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Выберите тип животного")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Кролик",
                    "Тигр",
                    "Обезьяна",
                    "Волк"
                }));

        var name = AnsiConsole.Ask<string>("Введите имя животного:");
        var food = AnsiConsole.Ask<int>("Введите количество еды (кг/день):");
        var number = AnsiConsole.Ask<int>("Введите инвентарный номер:");

        Animal animal = null;
        switch (animalType)
        {
            case "Кролик":
                var kindnessLevel = AnsiConsole.Ask<int>("Введите уровень доброты (1-10):");
                animal = new Rabbit(name, food, number, kindnessLevel);
                break;

            case "Тигр":
                animal = new Tiger(name, food, number);
                break;

            case "Обезьяна":
                kindnessLevel = AnsiConsole.Ask<int>("Введите уровень доброты (1-10):");
                animal = new Monkey(name, food, number, kindnessLevel);
                break;

            case "Волк":
                animal = new Wolf(name, food, number);
                break;
        }

        if (animal != null)
        {
            zoo.AddAnimal(animal);
            AnsiConsole.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
    
    private static void AddThing(Zoo zoo)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(
            new FigletText("Добавить вещь")
                .LeftJustified()
                .Color(Color.Blue));

        var thingType = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Выберите тип вещи")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Стол",
                    "Компьютер"
                }));

        var name = AnsiConsole.Ask<string>("Введите название вещи:");
        var number = AnsiConsole.Ask<int>("Введите инвентарный номер:");

        Thing thing = null;
        switch (thingType)
        {
            case "Стол":
                thing = new Table(name, number);
                break;

            case "Компьютер":
                thing = new Computer(name, number);
                break;
        }

        if (thing != null)
        {
            zoo.AddThing(thing);
            AnsiConsole.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}