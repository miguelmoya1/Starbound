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
    private int enemies;
    private Enemy[] enemy;
    private int vulnerable;
    private Sprite playerBar, gameMenu;
    private bool keyESCPresed;


    public Game()
    {
        font18 = new Font("data/Joystix.ttf", 18);
        playerBar = new Sprite("data/playerBar.png");
        gameMenu = new Sprite("data/GameMenu.png");
        keyESCPresed = false;
        finished = false;
        player = new Player(this);
        lives = 100;
        x = player.GetX();
        y = player.GetY();
        currentLevel = new Level(player);
        enemies = 5;
        Random r = new Random();
        enemy = new Enemy[enemies];
        for (int i = 0; i < enemies; i++)
        {
            int xEnemy = r.Next(600, 2500);
            int yEnemy = r.Next(300, 400);
            enemy[i] = new Enemy(xEnemy, yEnemy, currentLevel);
        }
        vulnerable = 0;
    }


    public void Run()
    {
        while (!finished)
        {
            CheckCollisions();
            MoveElements();
            DrawElements();
            CheckKeys();
            MoveScroll();
            PauseTillNextFrame();
        }
    }


    public void DrawElements()
    {
        Hardware.ClearScreen();

        int toMoveX = player.GetX() - player.GetStartX();
        int toMoveY = player.GetY() - player.GetStartY();
        currentLevel.DrawOnHiddenScreen();
        player.DrawOnHiddenScreen();
        playerBar.DrawOnHiddenScreen();
        for (int i = 0; i < enemies; i++)
            enemy[i].DrawOnHiddenScreen();
        if (keyESCPresed)
            gameMenu.DrawOnHiddenScreen();
        Hardware.ShowHiddenScreen();
        player.Break(currentLevel);
    }


    public void MoveElements()
    {
        player.Move();

        for (int i = 0; i < enemies; i++)
            enemy[i].Move(player);

        int toMoveX = player.GetX() - player.GetStartX();
        int toMoveY = player.GetY() - player.GetStartY();
        playerBar.SetY(toMoveY);
        playerBar.SetX(toMoveX);

        const int HEIGHT = 100, WIDHT = 300;
        gameMenu.SetX(toMoveX + WIDHT);
        gameMenu.SetY(toMoveY + HEIGHT);

        if (keyESCPresed)
        {
            int xMouse = Mouse.GetX() + toMoveX;
            int yMouse = Mouse.GetY() + toMoveY;

            const int RIGHT = 17, LEFT = 400;
            // 72, 140, 147... is the height  in the Menu when you clic esc.
            if (Mouse.Clic() == 1 &&
                (xMouse >= RIGHT + toMoveX + WIDHT &&
                xMouse <= LEFT + toMoveX + WIDHT) &&
                (yMouse >= 72 + toMoveY + HEIGHT &&
                yMouse <= 140 + toMoveY + HEIGHT))
            {
                keyESCPresed = false;
            }
            if (Mouse.Clic() == 1 &&
                (xMouse >= RIGHT + toMoveX + WIDHT &&
                xMouse <= LEFT + toMoveX + WIDHT) &&
                (yMouse >= 147 + toMoveY + HEIGHT &&
                yMouse <= 219 + toMoveY + HEIGHT))
            {
                // TO DO
            }
            if (Mouse.Clic() == 1 &&
                (xMouse >= RIGHT + toMoveX + WIDHT &&
                xMouse <= LEFT + toMoveX + WIDHT) &&
                (yMouse >= 226 + toMoveY + HEIGHT &&
                yMouse <= 299 + toMoveY + HEIGHT))
            {
                finished = true;
                Hardware.ResetScroll();
            }
        }
    }


    public void MoveScroll()
    {
        int moved = player.GetX() - x;
        Hardware.ScrollHorizontally((short)-moved);
        x = player.GetX();
        moved = player.GetY() - y;
        Hardware.ScrollVertically((short)-moved);
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
            keyESCPresed = true;
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
        if (vulnerable > 0)
            vulnerable--;
        for (int i = 0; i < enemies; i++)
            if (enemy[i].CollisionsWith(player))
            {
                if (vulnerable <= 0)
                {
                    lives -= 10;
                    vulnerable = 50;
                }
            }
        if (lives <= 0)
            finished = true;
    }
}