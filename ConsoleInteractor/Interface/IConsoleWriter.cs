namespace MineSweeper.ConsoleInteractor.Interface;

public interface IConsoleWriter
{
    void PrintNewLine();
     void PrintMessage(string message) => Console.Write(message);
    void PrintMessageLine(string message);
}