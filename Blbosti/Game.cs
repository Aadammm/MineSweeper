﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blbosti
{
    enum GameStatus { Continue, Lose, Win }
    internal class Game
    {
        int x; int y; int lengthText;
        GameStatus status;
        bool[,] unCovered;
        Area area;
        public Game(int size, int mines)
        {
            status = GameStatus.Continue;
            area = new Area(size, size, mines);
            unCovered = new bool[size, size];
            Console.CursorVisible = false;
        }
        public void GameProcess()
        {
            while (status == GameStatus.Continue)
            {
                ShowArea();
                Move();
                Check();
            }
            EndGame();
        }
        void Check()
        {
            if (area[y, x] == -1)
            {
                for (int i = 0; i < area.field.GetLength(0); i++)
                    for (int j = 0; j < area.field.GetLength(1); j++)
                        if (area[i, j] == -1)
                            unCovered[i, j] = true;
                status = GameStatus.Lose;
            }
            else
            {
                status = GameStatus.Win;
                UnCover(y, x);
                for (int i = 0; i < area.field.GetLength(0); i++)
                    for (int j = 0; j < area.field.GetLength(1); j++)
                        if (area[i, j] >= 0 && !unCovered[i, j])
                        {
                            status = GameStatus.Continue;
                            return;
                        }
            }
        }
        public void ShowArea()
        {
            Console.Clear();
            string s = "  |";
            for (int i = 1; i <= area.field.GetLength(0); i++)
            {
                s += " " + i.ToString().PadRight(2);
            }
            s += "|";
            Console.WriteLine(s);
            Console.WriteLine("--+-".PadRight(area.field.GetLength(0) * 3 + 3, '-') + "|--");
            for (int i = 0; i < area.field.GetLength(0); i++)
            {
                Console.Write((i + 1).ToString().PadLeft(2) + "|");
                for (int j = 0; j < area.field.GetLength(1); j++)
                {
                    if (unCovered[i, j])
                    {
                        if (area[i, j] == 0)
                            Console.Write("   ");
                        else if (area[i, j] > 0)
                            CollorChar(area[i, j].ToString(), ConsoleColor.Blue);
                        else
                            CollorChar("*", ConsoleColor.Red);
                    }
                    else
                        Console.Write(" # ");

                }
                Console.Write("|" + (i + 1).ToString().PadLeft(2));
                Console.WriteLine();
            }
            Console.WriteLine("--+-".PadRight(area.field.GetLength(0) * 3 + 3, '-') + "|--");
            Console.WriteLine(s);
            lengthText = s.Length;
        }

        void CollorChar(string letter, ConsoleColor color)
        {
            var originalColor = Console.BackgroundColor;
            Console.BackgroundColor = color;
            Console.Write(" {0} ", letter);
            Console.BackgroundColor = originalColor;
        }

        private void Move()
        {
            Console.WriteLine();
            Console.Write("X: ");
            while (!int.TryParse(Console.ReadLine(), out x) || x <= 0 || x > area.field.GetLength(0))
            {
                Console.WriteLine("invalid coordinates");
            }
            Console.Write("Y: ");
            while (!int.TryParse(Console.ReadLine(), out y) || y <= 0 || y > area.field.GetLength(1))
            {
                Console.WriteLine("invalid coordinates");
            }
            if (unCovered[y - 1, x - 1])
            {
                Console.WriteLine("This cell is already uncovered");
                Move();
            }
            else
            {
                x -= 1; y -= 1;
            }

        }
        void UnCover(int xx, int yy)
        {
            if (xx >= 0 && xx < area.field.GetLength(0)
                   && yy >= 0 && yy < area.field.GetLength(1) && !unCovered[xx, yy])
            {
                unCovered[xx, yy] = true;
                if (area[xx, yy] == 0)
                {
                    for (int i = -1; i <= 1; i++)
                        for (int j = -1; j <= 1; j++)
                            UnCover(xx + j, yy + i);
                }
            }
        }
        void EndGame()
        {
            ShowArea();
            switch (status)
            {
                case GameStatus.Lose:
                    Console.WriteLine("You lose!");
                    break;
                case GameStatus.Win:
                    Console.WriteLine("You win!");
                    break;
            }
        }
    }
}
