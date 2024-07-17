using MineSweeper.ConsoleInteractor.Interface;
using System.Text;

namespace MineSweeper
{
    public class Game
    {
        readonly IConsoleUtility _consoleUtility;
        readonly IConsoleReader _consoleReader;
        readonly IConsoleWriter _consoleWriter;
        GameStatus status;

        int x; int y;
        readonly int areaWidth;
        readonly int areaHeight;
        readonly bool[,] unCoveredField;
        readonly Area area;
        public Game(int size, int mines,
            IConsoleUtility consoleUtility,
            IConsoleReader consoleReader, IConsoleWriter
            consoleWriter)
        {
            _consoleUtility = consoleUtility;
            _consoleReader = consoleReader;
            _consoleWriter = consoleWriter;

            status = GameStatus.Continue;
            area = new Area(size, size, mines);
            areaWidth = area.field.GetLength(0);
            areaHeight = area.field.GetLength(1);
            unCoveredField = new bool[size, size];
            _consoleUtility.SetCursorVisible(false);
        }
        public void Start()
        {
            while (status == GameStatus.Continue)
            {
                _consoleUtility.CleanConsole();
                ShowArea();
                Move();
                CheckGameStatus();
            }
            ShowArea();
            EndGame();
        }
        void CheckGameStatus()
        {
            if (area[y, x] == -1)
            {
                for (int i = 0; i < areaWidth; i++)
                    for (int j = 0; j < areaHeight; j++)
                        if (area[i, j] == -1)
                            unCoveredField[i, j] = true;
                status = GameStatus.Lose;
            }
            else
            {
                status = GameStatus.Win;
                UnCover(y, x);
                for (int i = 0; i < areaWidth; i++)
                    for (int j = 0; j < areaHeight; j++)
                        if (area[i, j] >= 0 && !unCoveredField[i, j])
                        {
                            status = GameStatus.Continue;
                            return;
                        }
            }
        }
        private void ShowArea()
        {
            StringBuilder stringBuilder = new("  |");
            for (int i = 1; i <= areaWidth; i++)
            {
                stringBuilder.Append(" " + i.ToString().PadRight(2));
            }
            stringBuilder.Append('|');
            _consoleWriter.PrintMessageLine(stringBuilder.ToString());
            _consoleWriter.PrintMessageLine("--+-".PadRight(areaWidth * 3 + 3, '-') + "|--");
            for (int i = 0; i < areaWidth; i++)
            {
                _consoleWriter.PrintMessage((i + 1).ToString().PadLeft(2) + "|");
                for (int j = 0; j < areaHeight; j++)
                {
                    if (unCoveredField[i, j])
                    {
                        if (area[i, j] == 0)
                            _consoleWriter.PrintMessage("   ");
                        else if (area[i, j] > 0)
                            CollorChar(area[i, j].ToString(), ConsoleColor.Blue);
                        else
                            CollorChar("*", ConsoleColor.Red);
                    }
                    else
                        _consoleWriter.PrintMessage(" # ");

                }
                _consoleWriter.PrintMessage("|" + (i + 1).ToString().PadLeft(2));
                _consoleWriter.PrintNewLine();
            }
            _consoleWriter.PrintMessageLine("--+-".PadRight(areaWidth * 3 + 3, '-') + "|--");
            _consoleWriter.PrintMessageLine(stringBuilder.ToString());
        }

        private void CollorChar(string letter, ConsoleColor color)
        {
            var originalColor = _consoleUtility.ConsoleColorActual();
            _consoleUtility.SetCollor(color);
            _consoleWriter.PrintMessage($" {letter} ");
            _consoleUtility.SetCollor(originalColor);
        }

        private void Move()
        {
            string invalidCoordinates = "invalid coordinates";
            _consoleWriter.PrintNewLine();
            _consoleWriter.PrintMessage("X: ");
            while (!int.TryParse(_consoleReader.ReadCoordinate(), out x) || x <= 0 || x > areaWidth)
            {
                _consoleWriter.PrintMessageLine(invalidCoordinates);
            }
            _consoleWriter.PrintMessage("Y: ");
            while (!int.TryParse(_consoleReader.ReadCoordinate(), out y) || y <= 0 || y > areaHeight)
            {
                _consoleWriter.PrintMessageLine(invalidCoordinates);
            }
            if (unCoveredField[y - 1, x - 1])
            {
                _consoleWriter.PrintMessageLine("This cell is already uncovered");
                Move();
            }
            else
            {
                x -= 1; y -= 1;
            }

        }
        private void UnCover(int xx, int yy)
        {
            if (xx >= 0 && xx < areaWidth
                   && yy >= 0 && yy < areaHeight && !unCoveredField[xx, yy])
            {
                unCoveredField[xx, yy] = true;
                if (area[xx, yy] == 0)
                {
                    for (int i = -1; i <= 1; i++)
                        for (int j = -1; j <= 1; j++)
                            UnCover(xx + j, yy + i);
                }
            }
        }
        private void EndGame()
        {
            switch (status)
            {
                case GameStatus.Lose:
                    _consoleWriter.PrintMessageLine("You lose!");
                    break;
                case GameStatus.Win:
                    _consoleWriter.PrintMessageLine("You win!");
                    break;
            }
        }
    }
}