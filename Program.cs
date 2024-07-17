using MineSweeper;

Game game = CreateGame();
game.Start();
Console.Read();

Game CreateGame()
{
    string sizeText = "what size will the field be?: ";
    string minesText = "number of mines?: ";
    int size; int mines;
    Console.Write(sizeText);
    while (!int.TryParse(Console.ReadLine(), out size) || size <= 0 || size > 30)
    {
        Console.Clear();
        Console.Write(sizeText);
    }
    Console.Write(minesText);
    while (!int.TryParse(Console.ReadLine(), out mines) || (size * size) < mines)
    {
        Console.Clear();
        Console.Write(minesText);
    }
      return new Game(size, mines);
}