namespace MineSweeper.ConsoleInteractor.Interface;

public interface IConsoleUtility
{
    void CleanConsole();
    void SetCollor(ConsoleColor color);
    void SetCursorVisible(bool showCursor);
    ConsoleColor ConsoleColorActual();
}