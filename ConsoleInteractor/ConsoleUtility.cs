using MineSweeper.ConsoleInteractor.Interface;

namespace MineSweeper.ConsoleInteractor
{
    public class ConsoleUtility : IConsoleUtility
    {
        public void CleanConsole()                         => Console.Clear();

        public ConsoleColor ConsoleColorActual() => Console.BackgroundColor;

        public void SetCollor(ConsoleColor color)            => Console.BackgroundColor = color;
        public void SetCursorVisible(bool showCursor)    => Console.CursorVisible = showCursor;
    }
}