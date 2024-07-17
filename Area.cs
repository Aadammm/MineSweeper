namespace MineSweeper
{
    internal class Area
    {
        public readonly int[,] field;
        public int mine = -1;
        public int this[int row, int column]
        {
            get { return field[row, column]; }
        }

        public Area(int width, int height, int numberOfMines)
        {
            field = new int[width, height];
            FillField(numberOfMines);
        }
        private void FillField(int mines)
        {
            Random random = new();
            int width = field.GetLength(0);
            int height = field.GetLength(1);
            int x =  random.Next(width);
            int y =  random.Next(height);
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