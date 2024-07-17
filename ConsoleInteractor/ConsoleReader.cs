using MineSweeper.ConsoleInteractor.Interface;

namespace MineSweeper.ConsoleInteractor
{
    public class ConsoleReader : IConsoleReader
    {
        public string? ReadCoordinate() => Console.ReadLine();
    }
}