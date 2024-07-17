using MineSweeper.ConsoleInteractor.Interface;

namespace MineSweeper.ConsoleInteractor
{
    public class ConsoleWriter : IConsoleWriter
    {
        public void PrintNewLine() => Console.WriteLine();
        public void PrintMessageLine(string message) => Console.WriteLine(message);
        public void PrintMessage(string message) => Console.Write(message);

    }
}