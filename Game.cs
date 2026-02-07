namespace ProgWGrProgr_Zad3;

public class Game
{
    private Direction CurrentDirection { get; set; }
    private long GameSleepInterval { get; set; }
    private int Score { get; set; }
    private Point Food { get; set; }
    private int Height { get; set; } = 50;
    private int Width { get; set; } = 50;

    private List<Point> _snake = new()
    {
        new Point(3, 3)
    };
    
    public Game(Direction direction, long gameSleepInterval)
    {
        CurrentDirection = direction;
        GameSleepInterval = gameSleepInterval;
        Score = 0;
    }

    public void Start()
    {
            
    }

    private void GameLoop()
    {
        HandleInput();
        UpdateMovement();
    }
    
    private void UpdateMovement()
    {
        Point head = _snake[0];

        // wow, użyłem context actions i zamienił zwykłego switcha z przypisaniem do newHead na to
        Point newHead = CurrentDirection switch 
        {
            Direction.Up => new Point(head.X, head.Y - 1),
            Direction.Down => new Point(head.X, head.Y + 1),
            Direction.Left => new Point(head.X - 1, head.Y),
            Direction.Right => new Point(head.X + 1, head.Y),
            _ => head
        };

        _snake.Insert(0, newHead);
        
        if (newHead.X == Food.X && newHead.Y == Food.Y)
        {
            Score++;
            SpawnFood();
            return;
        }
        
        if(_snake.Count > 1) _snake.RemoveAt(_snake.Count - 1);
    }

    private void SpawnFood()
    {
        Random random = new();
        Food = new Point(
                random.Next(1, Width - 1),
                random.Next(1,  Height - 1)
            );
    }
    
    private void HandleInput()
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