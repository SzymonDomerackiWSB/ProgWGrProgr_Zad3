namespace ProgWGrProgr_Zad3;

class Program
{
    static void Main(string[] args)
    {
        Console.CursorVisible = false;
        var game = new Game(Direction.Right, 100);
        game.Start();
    }
}