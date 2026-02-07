namespace ProgWGrProgr_Zad3;

public class Game
{
    private Direction CurrentDirection { get; set; }
    private long GameSleepInterval { get; set; }
    private int Score { get; set; }
    
    public Game(Direction direction, long gameSleepInterval)
    {
        CurrentDirection = direction;
        GameSleepInterval = gameSleepInterval;
        Score = 0;
    }

    public void Start(int x, int y)
    {
        
    }

    public void HandleInput()
    {
        if (!Console.KeyAvailable) return;
        
        var key = Console.ReadKey(true).Key;
        
        switch (key)
        {
            case ConsoleKey.UpArrow:
                if (CurrentDirection != Direction.Down)
                    CurrentDirection = Direction.Up;
                break;
            case ConsoleKey.DownArrow:
                if (CurrentDirection != Direction.Up)
                    CurrentDirection = Direction.Down;
                break;
            case ConsoleKey.LeftArrow:
                if (CurrentDirection != Direction.Right)
                    CurrentDirection = Direction.Left;
                break;
            case ConsoleKey.RightArrow:
                if (CurrentDirection != Direction.Left)
                    CurrentDirection = Direction.Right;
                break;
        }
    }
}