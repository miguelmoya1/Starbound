using System;

class Game
{
    private Font font18;
    private Player player;
    private bool finished;
    private int lives;
    private int x;
    private int y;
    private Level currentLevel;


    public Game()
    {
        font18 = new Font("data/Joystix.ttf", 18);
        finished = false;
        player = new Player(this);
        lives = 100;
        x = player.GetX();
        y = player.GetY();
        currentLevel = new Level(player);
    }


    public void Run()
    {
        while (!finished)
        {
            CheckCollisions();
            DrawElements();
            CheckKeys();
            MoveScroll();
            MoveElements();
            PauseTillNextFrame();
        }
    }


    public void DrawElements()
    {
        Hardware.ClearScreen();

        currentLevel.DrawOnHiddenScreen();
        player.DrawOnHiddenScreen();
        Hardware.ShowHiddenScreen();
        player.Break(currentLevel);
    }


    public void MoveElements()
    {
        player.Move();
    }


    public void MoveScroll()
    {
        int moved = player.GetX() - x;
        Hardware.ScrollHorizontally((short) -moved);
        x = player.GetX();
        moved = player.GetY() - y;
        Hardware.ScrollVertically((short) -moved);
        y = player.GetY();
    }

        public void CheckKeys()
    {
        if (Hardware.KeyPressed(Hardware.KEY_W))
        {
            if (Hardware.KeyPressed(Hardware.KEY_D))
                player.JumpRight();
            else
            if (Hardware.KeyPressed(Hardware.KEY_A))
                player.JumpLeft();
            else
                player.Jump();
        }

        else if (Hardware.KeyPressed(Hardware.KEY_D))
            player.MoveRight();

        else if (Hardware.KeyPressed(Hardware.KEY_A))
            player.MoveLeft();

        if (Hardware.KeyPressed(Hardware.KEY_ESC))
        {
            Hardware.ResetScroll();
            finished = true;
        }
    }

    public bool IsValidMove(int xMin, int yMin, int xMax, int yMax)
    {
        return currentLevel.IsValidMove(xMin, yMin, xMax, yMax);
    }


    public void PauseTillNextFrame()
    {
        // Pause till next frame (20 ms = 50 fps)
        Hardware.Pause(20);
    }


    public void CheckCollisions()
    {
        bool colision = false;
        if (lives <= 0)
            finished = true;
        if (colision)
        {
            Hardware.ResetScroll();
            colision = false;
        }
    }
}