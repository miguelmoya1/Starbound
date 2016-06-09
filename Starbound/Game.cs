using System;
using System.Collections.Generic;

class Game
{
    private Item items;
    private List<Text> texts;
    private Font font18;
    private Player player;
    private List<Item> item;
    private char a;
    private bool finished;
    private int lives, x, y, invulnerable, sleepHome;
    private byte levels, levelActual, lastLevel, actualItem, delay, heal;
    private Level[] currentLevel;
    private List<Enemy> enemy;
    private Tool playerBar, gameMenu, rightBar, topBar;
    private Tool[] live;
    private long lavaDamage;
    private bool keyESCPresed;
    // private Rain[] rain;
    private Weapon weapon, w, peak;


    public Game()
    {
        lavaDamage = 1;
        font18 = new Font("data/Joystix.ttf", 18);
        playerBar = new Tool(new Sprite("data/playerBar.png"));
        gameMenu = new Tool(new Sprite("data/GameMenu.png"));
        rightBar = new Tool(new Sprite("data/RightBar.png"));
        topBar = new Tool(new Sprite("data/topBar.png"));
        const int SIZE = 15;
        live = new Tool[SIZE];
        for (int i = 0; i < SIZE; i++)
            live[i] = new Tool(new Sprite("data/live.png"));
        //const int SIZERAIN = 0;
        //rain = new Rain[SIZERAIN];

        texts = new List<Text>();

        weapon = new Weapon(15);
        w = new Weapon('c');
        peak = new Weapon('p');
        topBar.AddItem(w);
        topBar.AddItem(peak);
        keyESCPresed = false;
        finished = false;
        levels = 2;
        actualItem = 0;
        levelActual = 0;
        sleepHome = 0;
        delay = 0;
        heal = 0;
        item = new List<Item>();
        player = new Player(this);
        lives = 100;
        x = player.GetX();
        y = player.GetY();
        currentLevel = new Level[levels];
        for (byte i = 0; i < levels; i++)
            currentLevel[i] = new Level(player, i);
        Random r = new Random();
        enemy = new List<Enemy>();
        for (int i = 0; i < 6; i++)
        {
            int xEnemy = r.Next(600, 2500);
            int yEnemy = r.Next(300, 400);
            enemy.Add(new Enemy(xEnemy, yEnemy, currentLevel[levelActual]));
        }

        //for (int i = 0; i < SIZERAIN; i++)
        //    rain[i] = new Rain(r.Next(100, 1500), 100, 0, currentLevel[levelActual]);

        invulnerable = 0;
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
        if (topBar.GetItemAt(actualItem) == w)
        {
            weapon.DrawOnHiddenScreen();
        }
        weapon.DrawShot();
        currentLevel[levelActual].DrawOnHiddenScreen();

        if (lastLevel != levelActual)
        {
            player.Restart();
            if (levelActual == 1)
            {
                // To move to player to correct position
                const int UP = -97;
                player.SetY(player.GetY() + UP);
            }
        }

        playerBar.DrawOnHiddenScreen();

        for (int i = 0; i < item.Count; i++)
            item[i].DrawOnHiddenScreen();

        if (levelActual == 0)
            for (int i = 0; i < enemy.Count; i++)
                enemy[i].DrawOnHiddenScreen();

        //for (int i = 0; i < rain.Length; i++)
        //    rain[i].DrawOnHiddenScreen();

        if (keyESCPresed)
            gameMenu.DrawOnHiddenScreen();

        rightBar.DrawOnHiddenScreen();
        topBar.DrawOnHiddenScreen();
        for (int i = 0; i < 10; i++)
            if (topBar.GetItemAt(i) != null)
                topBar.DrawItem(i);
        const int NUMBEROFLIVES = 7;
        for (int i = 0; i < (lives / NUMBEROFLIVES) + 1; i++)
            live[i].DrawOnHiddenScreen();

        for (int i = 0; i < texts.Count; i++)
            texts[i].DrawText(font18);

        Hardware.ShowHiddenScreen();

        lastLevel = levelActual;

    }

    public void MoveElementsHorizontaly()
    {
        int toMoveX = player.GetX() - player.GetStartX();

        playerBar.SetX(toMoveX);

        // To set the live to the correct pos I need to desplace the initial
        // space and the size of the live
        const int DESP1 = 44, DESP2 = 6;
        for (int i = 0; i < live.Length; i++)
            live[i].SetX(toMoveX + DESP1 + (DESP2 * i));

        const int WIDHT = 300;
        gameMenu.SetX(toMoveX + WIDHT);

        const int WIDHTTOP = 300;
        topBar.SetX(toMoveX + WIDHTTOP);

        const int WIDHTRIGHTBAR = 980;
        rightBar.SetX(toMoveX + WIDHTRIGHTBAR);

        int xMouse = Mouse.GetX() + toMoveX;
        if (xMouse < player.GetX())
        {
            const int DESPLACEWEAPONX = 22;
            weapon.SetX(player.GetX() - DESPLACEWEAPONX);
        }
        else
        {
            const int DESPLACEWEAPONX = 22;
            weapon.SetX(player.GetX() + DESPLACEWEAPONX);
        }
        const int DESPLACEWEAPONY = 29;
        weapon.SetY(player.GetY() + DESPLACEWEAPONY);

        const byte TOTALBUTTOMS = 10;
        const int INITIAL = 19, TOMOVE = 42;
        int right = INITIAL;
        for (int i = 0; i < TOTALBUTTOMS; i++)
        {
            if (topBar.GetItemAt(i) != null)
                topBar.SetImageX(right + WIDHTTOP + toMoveX, i);
            right += TOMOVE;
        }
    }



    public void MoveElements()
    {
        for (int i = 0; i < texts.Count; i++)
        {
            if (texts[i].GetDelay() > 0)
            {
                texts[i].SetY((short)(texts[i].GetY() - 1));
                texts[i].SetDelay((byte)(texts[i].GetDelay() - 1));
            }
            else
                texts.RemoveAt(i);

        }

        const int HEIGHTI = 545, WIGTHI = 433;
        //for (int i = 0; i < rain.Length; i++)
        //    rain[i].Move();
        // The x mouse and the Y mouse
        int xMouse = (Mouse.GetX() + (x - HEIGHTI));
        int yMouse = (Mouse.GetY() + (y - WIGTHI));
        if (currentLevel[levelActual].GetPosicion((short)(xMouse / 16),
                    (short)(yMouse / 16)) != '.')
            a = currentLevel[levelActual].GetPosicion((short)(xMouse / 16),
                        (short)(yMouse / 16));

        if (topBar.GetItemAt(actualItem) == w)
        {
            const byte RIGHT = 0, LEFT = 1;
            if (xMouse < player.GetX())
            {
                weapon.ChangeDirection(LEFT);
                player.ChangeDirection(LEFT);
            }
            else
            {
                weapon.ChangeDirection(RIGHT);
                player.ChangeDirection(RIGHT);
            }
        }

        const char FLOOR = '_', STONE = 'w';
        // Break the stone

        if (topBar.GetItemAt(actualItem) == peak)
        {
            if (player.Break(currentLevel[levelActual], xMouse / 16,
                    yMouse / 16))
            {
                const int AJUSTINGL = 60, AJUSTINGT = 60;
                item.Add(new Item(xMouse + AJUSTINGL, yMouse + AJUSTINGT
                    , a, currentLevel[levelActual]));
                player.Break(currentLevel[levelActual], xMouse / 16,
                yMouse / 16);
            }
        }
        else if (topBar.GetItemAt(actualItem) != null)
            if (topBar.GetItemAt(actualItem).GetChar() == FLOOR &&
                topBar.GetItemAt(actualItem).GetTotal() > 0)
            {
                if (Mouse.Clic() == 1 &&
                        currentLevel[levelActual].GetPosicion((short)(xMouse / 16),
                        (short)(yMouse / 16)) != '_' &&
                        currentLevel[levelActual].GetPosicion((short)(xMouse / 16),
                        (short)(yMouse / 16)) != 'w' &&
                        currentLevel[levelActual].GetPosicion((short)(xMouse / 16),
                        (short)(yMouse / 16)) != 'x' &&
                        currentLevel[levelActual].GetPosicion((short)(xMouse / 16),
                        (short)(yMouse / 16)) != 'L')
                {
                    currentLevel[levelActual].SetPosition((short)(xMouse / 16),
                    (short)(yMouse / 16), FLOOR);
                    topBar.GetItemAt(actualItem).LessItems();
                }
            }
            else if (topBar.GetItemAt(actualItem).GetChar() == STONE &&
                topBar.GetItemAt(actualItem).GetTotal() > 0)
            {
                if (Mouse.Clic() == 1 &&
                        currentLevel[levelActual].GetPosicion((short)(xMouse / 16),
                        (short)(yMouse / 16)) != '_' &&
                        currentLevel[levelActual].GetPosicion((short)(xMouse / 16),
                        (short)(yMouse / 16)) != 'w' &&
                        currentLevel[levelActual].GetPosicion((short)(xMouse / 16),
                        (short)(yMouse / 16)) != 'x' &&
                        currentLevel[levelActual].GetPosicion((short)(xMouse / 16),
                        (short)(yMouse / 16)) != 'L')
                {
                    currentLevel[levelActual].SetPosition((short)(xMouse / 16),
                    (short)(yMouse / 16), STONE);
                    topBar.GetItemAt(actualItem).LessItems();
                }

            }

        player.Move();
        weapon.MoveShot(player.GetX());

        // if isn't at base, then move the enemies
        if (levelActual == 0)
            for (int i = 0; i < enemy.Count; i++)
                enemy[i].Move(player);


        int toMoveY = player.GetY() - player.GetStartY();
        playerBar.SetY(toMoveY);

        const int DESPLACETOP = 19;
        for (int i = 0; i < live.Length; i++)
            live[i].SetY(toMoveY + DESPLACETOP);
        const int HEIGHT = 100;
        gameMenu.SetY(toMoveY + HEIGHT);

        const sbyte PIXELAJUSTING = -2;
        topBar.SetY(toMoveY + PIXELAJUSTING);

        rightBar.SetY(toMoveY);

        const int WIDHTRIGHTBAR = 980;
        const int MARGINRIGHT = WIDHTRIGHTBAR + 36;
        const int MARGINTOP1 = 242;
        const int MARGINTOP2 = 275;
        const int WIDHT = 300;

        int toMoveX = player.GetX() - player.GetStartX();

        // 1 is Left clic
        if (topBar.GetItemAt(actualItem) == w)
            if (delay <= 0)
            {
                if (Mouse.Clic() == 1)
                {
                    delay = 5;
                    if (weapon.currentDirection == 0)
                    {
                        const int DESPLACEX = 50, DESPLACEY = 30;
                        weapon.Shot(this, player.GetX() + DESPLACEX
                            , player.GetY() + DESPLACEY);
                    }
                    else
                    {
                        const int DESPLACEX = 25, DESPLACEY = 30;
                        weapon.Shot(this, player.GetX() - DESPLACEX
                            , player.GetY() + DESPLACEY);
                    }
                }
            }
            else
                delay--;

        yMouse = Mouse.GetY() + toMoveY;
        xMouse = Mouse.GetX() + toMoveX;

        // To check the top bar buttons 
        const byte TOTALBUTTOMS = 10;
        const int TOP = 7, TOP2 = 45, INITIAL = 19,
            TOMOVE = 52, SIZE = 36;
        int right = INITIAL, left = INITIAL + SIZE;
        for (int i = 0; i < TOTALBUTTOMS; i++)
        {
            if (topBar.GetItemAt(i) != null)
                topBar.SetImageY(TOP + toMoveY, i);

            // Check the key
            if (Mouse.Clic() == 1 &&
                    (xMouse >= right + toMoveX + WIDHT &&
                    xMouse <= left + toMoveX + WIDHT) &&
                    (yMouse >= TOP + toMoveY + HEIGHT &&
                    yMouse <= TOP2 + toMoveY + HEIGHT))
            {
                actualItem = (byte)i;
            }
            right += TOMOVE;
            left += TOMOVE;
        }

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
        const byte RIGHT = 0, LEFT = 1;
        if (Hardware.KeyPressed(Hardware.KEY_SPC) ||
            Hardware.KeyPressed(Hardware.KEY_W))
        {
            if (Hardware.KeyPressed(Hardware.KEY_D))
            {
                player.JumpRight();
                if (topBar.GetItemAt(actualItem) != w)
                    player.ChangeDirection(RIGHT);
            }
            if (Hardware.KeyPressed(Hardware.KEY_A))
            {
                player.JumpLeft();
                if (topBar.GetItemAt(actualItem) != w)
                    player.ChangeDirection(LEFT);
            }
            else
                player.Jump();
        }
        if (Hardware.KeyPressed(Hardware.KEY_D))
        {
            player.MoveRight();
            if (topBar.GetItemAt(actualItem) != w)
                player.ChangeDirection(RIGHT);
        }

        if (Hardware.KeyPressed(Hardware.KEY_A))
        {
            player.MoveLeft();
            if (topBar.GetItemAt(actualItem) != w)
                player.ChangeDirection(LEFT);
        }

        if (Hardware.KeyPressed(Hardware.KEY_1))
            actualItem = 0;
        else if (Hardware.KeyPressed(Hardware.KEY_2))
            actualItem = 1;
        else if (Hardware.KeyPressed(Hardware.KEY_3))
            actualItem = 2;
        else if (Hardware.KeyPressed(Hardware.KEY_4))
            actualItem = 3;
        else if (Hardware.KeyPressed(Hardware.KEY_5))
            actualItem = 4;
        else if (Hardware.KeyPressed(Hardware.KEY_6))
            actualItem = 5;
        else if (Hardware.KeyPressed(Hardware.KEY_7))
            actualItem = 6;
        else if (Hardware.KeyPressed(Hardware.KEY_8))
            actualItem = 7;
        else if (Hardware.KeyPressed(Hardware.KEY_9))
            actualItem = 8;
        else if (Hardware.KeyPressed(Hardware.KEY_0))
            actualItem = 9;

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
        if (invulnerable > 0)
            invulnerable--;
        if (levelActual == 0)
            for (int i = 0; i < enemy.Count; i++)
                if (enemy[i].CollisionsWith(player))
                    if (invulnerable <= 0)
                    {
                        texts.Add(new Text((short)player.GetX(), (short)
                            player.GetY(), 255, 0, 0, 30,
                            Convert.ToString(enemy[i].GetDamage())));
                        lives -= enemy[i].GetDamage();
                        invulnerable = 25;
                    }
        if (heal == 0)
        {
            if (lives < 100)
                lives++;
            heal = 10;
        }
        heal--;

        if (lives <= 0)
        {
            finished = true;
            Hardware.ResetScroll();
        }

        for (int i = 0; i < item.Count; i++)
            if (player.CollisionsWith(item[i]))
            {
                if (!topBar.ContainsChar(a))
                {
                    topBar.AddItem(new Item(
                        item[i].GetChar()));
                    int j;
                    for (j = 0; j < topBar.TotalItems(); j++)
                        if (topBar.GetItemAt(j).GetChar() ==
                            item[i].GetChar())
                            topBar.GetItemAt(j).MoreItems();
                }
                else
                {
                    for (int j = 0; j < topBar.TotalItems(); j++)
                        if (topBar.GetItemAt(j).GetChar() ==
                                item[i].GetChar())
                            topBar.AddItems(j);
                }
                player.AddToInventory(item[i]);
                item.RemoveAt(i);
            }

        for (int i = 0; i < enemy.Count; i++)
            if (weapon.ShotCollisionsWith(enemy[i]))
            {
                texts.Add(new Text((short)enemy[i].GetX(), (short)
                    enemy[i].GetY(), 255, 0, 0, 20,
                    Convert.ToString(weapon.GetDamage())));
                enemy[i].SetLive(enemy[i].GetLive() - weapon.GetDamage());
                if (enemy[i].GetLive() <= 0)
                    enemy.RemoveAt(i);
            }

        if (invulnerable <= 0)
            if (!currentLevel[levelActual].IsLava(player.GetX(), player.GetY(),
                player.GetX() + player.GetWidth(), player.GetY() +
                player.GetHeight()))
            {
                texts.Add(new Text((short)player.GetX(), (short)
                            player.GetY(), 255, 0, 0, 30,
                            Convert.ToString(lavaDamage)));
                lives -= (int) lavaDamage;
                invulnerable = 25;
                lavaDamage *= 2;
            }
            else
                lavaDamage = 1;
    }
}