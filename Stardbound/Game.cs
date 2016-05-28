using System;
using System.Collections.Generic;

class Game
{
    private Font font18;
    private Player player;
    private List<Item> item;
    private bool finished;
    private int lives, x, y, enemies, vulnerable, sleepHome;
    private byte levels, levelActual, lastLevel;
    private Level[] currentLevel;
    private Enemy[] enemy;
    private Tool playerBar, gameMenu, rightBar, topBar;
    private bool keyESCPresed;


    public Game()
    {
        font18 = new Font("data/Joystix.ttf", 18);
        playerBar = new Tool(new Sprite("data/playerBar.png"));
        gameMenu = new Tool(new Sprite("data/GameMenu.png"));
        rightBar = new Tool(new Sprite("data/RightBar.png"));
        topBar = new Tool(new Sprite("data/topBar.png"));
        keyESCPresed = false;
        finished = false;
        levels = 2;
        levelActual = 0;
        sleepHome = 0;
        item = new List<Item>();
        player = new Player(this);
        lives = 100;
        x = player.GetX();
        y = player.GetY();
        currentLevel = new Level[levels];
        for (byte i = 0; i < levels; i++)
            currentLevel[i] = new Level(player, i);
        enemies = 5;
        Random r = new Random();
        enemy = new Enemy[enemies];
        for (int i = 0; i < enemies; i++)
        {
            int xEnemy = r.Next(600, 2500);
            int yEnemy = r.Next(300, 400);
            enemy[i] = new Enemy(xEnemy, yEnemy, currentLevel[levelActual]);
        }
        vulnerable = 0;
    }


    public void Run()
    {
        while (!finished)
        {
            CheckCollisions();
            MoveElementsHorizontaly();
            DrawElements();
            MoveElements();
            CheckKeys();
            MoveScroll();
            PauseTillNextFrame();
        }
    }

    public void DrawElements()
    {
        Hardware.ClearScreen();

        if (levelActual == 0)
            Hardware.DrawHiddenImage(currentLevel[levelActual].GetBackGround(), player.GetX() - 480, -50);
        else
            Hardware.DrawHiddenImage(currentLevel[levelActual].GetBackGround(), 0, 0);

        player.DrawOnHiddenScreen();
        currentLevel[levelActual].DrawOnHiddenScreen();

        if (lastLevel != levelActual)
        {
            player.Restart();
            if (levelActual == 1)
            {
                // To move to player to correct position
                const int up = -97;
                player.SetY(player.GetY() + up);
            }

        }


        playerBar.DrawOnHiddenScreen();

        if (levelActual == 0)
            for (int i = 0; i < item.Count; i++)
                item[i].DrawOnHiddenScreen();

        if (levelActual == 0)
            for (int i = 0; i < enemies; i++)
                enemy[i].DrawOnHiddenScreen();

        if (keyESCPresed)
            gameMenu.DrawOnHiddenScreen();

        rightBar.DrawOnHiddenScreen();
        topBar.DrawOnHiddenScreen();

        Hardware.ShowHiddenScreen();

        lastLevel = levelActual;

    }

    public void MoveElementsHorizontaly()
    {
        int toMoveX = player.GetX() - player.GetStartX();


        playerBar.SetX(toMoveX);

        const int WIDHT = 300;
        gameMenu.SetX(toMoveX + WIDHT);

        const int WIDHTTOP = 300;
        topBar.SetX(toMoveX + WIDHTTOP);

        const int WIDHTRIGHTBAR = 980;
        rightBar.SetX(toMoveX + WIDHTRIGHTBAR);

        int xMouse = Mouse.GetX() + toMoveX;
    }



    public void MoveElements()
    {
        const int HEIGHTI = 545, WIGTHI = 433;

        // The x mouse and the Y mouse
        int xMouse = (Mouse.GetX() + (x - HEIGHTI));
        int yMouse = (Mouse.GetY() + (y - WIGTHI));

        // Break the stone
        if (player.Break(currentLevel[levelActual], xMouse / 16,
            yMouse / 16))
        {
            const int AJUSTINGL = 60, AJUSTINGT = 60;
            item.Add(new Item(xMouse + AJUSTINGL, yMouse + AJUSTINGT
                , '_', currentLevel[levelActual]));
            player.Break(currentLevel[levelActual], xMouse / 16,
            yMouse / 16);
        }

        player.Move();

        // if isn't at base, then move the enemies
        if (levelActual == 0)
            for (int i = 0; i < enemies; i++)
                enemy[i].Move(player);


        int toMoveY = player.GetY() - player.GetStartY();
        playerBar.SetY(toMoveY);

        const int HEIGHT = 100;
        gameMenu.SetY(toMoveY + HEIGHT);

        topBar.SetY(toMoveY);

        if (player.ContainsItems())
        {
            // TO DO
        }

        rightBar.SetY(toMoveY);
        
        const int WIDHTRIGHTBAR = 980;
        const int MARGINRIGHT = WIDHTRIGHTBAR + 36;
        const int MARGINTOP1 = 242;
        const int MARGINTOP2 = 275;
        const int WIDHT = 300;

        int toMoveX = player.GetX() - player.GetStartX();
        
        yMouse = Mouse.GetY() + toMoveY;
        xMouse = Mouse.GetX() + toMoveX;

        // Time to cant use the buttom to go Home
        if (sleepHome > 0)
            sleepHome--;
        // To check the kolision with the buttom to go to home
        if (Mouse.Clic() == 1 &&
                (xMouse >= toMoveX + WIDHTRIGHTBAR &&
                xMouse <= MARGINRIGHT + toMoveX + WIDHTRIGHTBAR) &&
                (yMouse >= MARGINTOP1 + toMoveY &&
                yMouse <= MARGINTOP2 + toMoveY))
        {
            if (sleepHome <= 0)
            {
                if (levelActual == 0)
                    levelActual = 1;
                else
                    levelActual = 0;
                // ~ 5 seg
                sleepHome = 50;
            }
        }

        if (keyESCPresed)
        {
            const int RIGHT = 17, LEFT = 400;
            // 72, 140, 147... is the height of the Menu when you clic esc.
            // TO DO const
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
        if (Hardware.KeyPressed(Hardware.KEY_SPC) ||
            Hardware.KeyPressed(Hardware.KEY_W))
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
            keyESCPresed = true;
    }

    public bool IsValidMove(int xMin, int yMin, int xMax, int yMax)
    {
        return currentLevel[levelActual].IsValidMove(xMin, yMin, xMax, yMax);
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
        if (levelActual == 0)
            for (int i = 0; i < enemies; i++)
                if (enemy[i].CollisionsWith(player))
                    if (vulnerable <= 0)
                    {
                        lives -= 10;
                        vulnerable = 50;
                    }
        if (lives <= 0)
        {
            finished = true;
            Hardware.ResetScroll();
        }

        for (int i = 0; i < item.Count; i++)
            if (player.CollisionsWith(item[i]))
            {
                player.AddToInventory(item[i]);
                item.RemoveAt(i);
            }
    }
}