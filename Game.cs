namespace ProgWGrProgr_Zad3;

public class Game
{
    private Direction CurrentDirection { get; set; }
    private int GameSleepInterval { get; set; }
    private int Score { get; set; }
    private Point Food { get; set; }
    private int Height { get; set; } = 10;
    private int Width { get; set; } = 30;
    private bool _gameOver = false;

    private List<Point> _snake = new()
    {
        new Point(3, 3)
    };
    
    public Game(Direction direction, int gameSleepInterval)
    {
        CurrentDirection = direction;
        GameSleepInterval = gameSleepInterval;
        Score = 0;
    }

    public void Start()
    {
        SpawnFood();
        GameLoop();
    }

    private void GameLoop()
    {
        while (!_gameOver)
        {
            HandleInput();
            UpdateMovement();
            Draw();
            
            Thread.Sleep(GameSleepInterval);
        }
        GameOverScreen();
    }

    private void GameOverScreen()
    {
        Console.Clear();
        Console.WriteLine($"Game over, score: {Score}");
    }
    
    private void Draw()
    {
        Console.Clear();
        
        for (int x = 0; x < Width; x++)
        {
            Console.SetCursorPosition(x, 0);
            Console.Write("#");
            Console.SetCursorPosition(x, Height - 1);
            Console.Write("#");
        }

        for (int y = 0; y < Height; y++)
        {
            Console.SetCursorPosition(0, y);
            Console.Write("#");
            Console.SetCursorPosition(Width - 1, y);
            Console.Write("#");
        }
        
        foreach (var part in _snake)
        {
            Console.SetCursorPosition(part.X, part.Y);
            Console.Write("O");
        }
        
        Console.SetCursorPosition(Food.X, Food.Y);
        Console.Write("X");
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
        if (newHead.X <= 0 || newHead.X >= Width - 1 ||
            newHead.Y <= 0 || newHead.Y >= Height - 1 || 
            _snake.Skip(1).Any(p => p.X == newHead.X && p.Y == newHead.Y))
        {
            _gameOver = true;
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