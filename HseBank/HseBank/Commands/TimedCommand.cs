using System.Diagnostics;

namespace HseBank.Commands;

public class TimedCommand : ICommand
{
    private readonly ICommand _command;

    public TimedCommand(ICommand command)
    {
        _command = command;
    }

    public void Execute()
    {
        var stopwatch = Stopwatch.StartNew();
        _command.Execute();
        stopwatch.Stop();
        Console.WriteLine($"Время выполнения: {stopwatch.Elapsed.TotalSeconds:F4} секунд");
    }
}