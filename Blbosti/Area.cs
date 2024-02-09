using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Blbosti
{
    internal class Area
    {

        Random random;
        public readonly int[,] field;
        public int mine = -1;
        public int this[int row, int column]
        {
            get { return field[row, column]; }
        }

        public Area(int width, int height, int countMines)
        {
            random = new Random();
            field = new int[width, height];
            FillField(field.GetLength(0), field.GetLength(1), countMines);
        }
        private void FillField(int width, int height, int mines)
        {
            int x = random.Next(width);
            int y = random.Next(height);
            //  fill field with mines
            for (int z = 0; z < mines; z++)
            {
                while (field[x, y] == mine)
                {
                    x = random.Next(width);
                    y = random.Next(height);
                }
                field[x, y] = mine;
                for (int i = Math.Max(x - 1, 0); i <= Math.Min(x + 1, width - 1); i++)
                    for (int j = Math.Max(y - 1, 0); j <= Math.Min(y + 1, height - 1); j++)
                        if (field[i, j] != mine)
                            field[i, j]++;
            }
        }
    }
}
