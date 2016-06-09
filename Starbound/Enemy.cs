
class Enemy : Sprite
{
    Level currentLevel;
    int live;
    int damage;

    public Enemy(int newX, int newY, Level g)
    {
        LoadSequence(RIGHT, new string[] { "data/EnemyRight1.png",
            "data/EnemyRight2.png", "data/EnemyRight3.png" });
        LoadSequence(LEFT, new string[] { "data/EnemyLeft1.png",
            "data/EnemyLeft2.png", "data/EnemyLeft3.png",});
        this.x = newX;
        this.y = newY;
        xSpeed = 6;
        damage = 15;
        ySpeed = 1;
        width = 64;
        height = 64;
        live = 100;
        currentLevel = g;
        ChangeDirection(RIGHT);

    }

    public int GetDamage()
    {
        return damage;
    }

    public void Move(Player p)
    {
        if (currentLevel.IsValidMove(
            x, y + ySpeed, x + width, y + height + ySpeed))
        {
            y += ySpeed;
        }
        else if (p.GetX() - x > -350 && p.GetX() - x < 0)
        {
            if (currentLevel.IsValidMove(x + xSpeed, y, x + width + xSpeed,
                    y + height))
            {
                if (xSpeed > 0)
                    xSpeed = -xSpeed;
            }
            else
                xSpeed = -xSpeed;
        }
        else if (p.GetX() - x < 350 && p.GetX() - x > 0)
        {
            if (currentLevel.IsValidMove(x - xSpeed, y, x + width - xSpeed,
                    y + height))
            {
                if (xSpeed < 0)
                    xSpeed = -xSpeed;
            }
            else
                xSpeed = -xSpeed;
        }
        else if (!currentLevel.IsValidMove(x - xSpeed, y, x + width - xSpeed,
                    y + height))
        {
            xSpeed = -xSpeed;
        }
        else if (!currentLevel.IsValidMove(x + xSpeed, y, x + width + xSpeed,
                y + height))
        {
            xSpeed = -xSpeed;
        }
        x += xSpeed;

        if (xSpeed < 0)
            ChangeDirection(LEFT);
        else
            ChangeDirection(RIGHT);
        NextFrame();
    }

    public void SetLive(int live)
    {
        this.live = live;
    }

    public int GetLive()
    {
        return live;
    }
}

