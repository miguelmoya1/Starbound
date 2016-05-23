
class Enemy : Sprite
{
    Level currentLevel;

    public Enemy(int newX, int newY, Level g)
    {
        LoadSequence(RIGHT, new string[] { "data/EnemyRight1.png",
            "data/EnemyRight2.png", "data/EnemyRight3.png" });
        LoadSequence(LEFT, new string[] { "data/EnemyLeft1.png",
            "data/EnemyLeft2.png", "data/EnemyLeft3.png",});
        this.x = newX;
        this.y = newY;
        xSpeed = 6;
        ySpeed = 1;
        width = 64;
        height = 64;
        currentLevel = g;
        ChangeDirection(RIGHT);

    }

    public void Move(Player p)
    {
        if (currentLevel.IsValidMove(
            x, y + ySpeed, x + width, y + height + ySpeed))
        {
            y += ySpeed;
        }
        else if (x - p.GetX() < -150 && x - p.GetX() < 0)
        {
            if (!currentLevel.IsValidMove(x + xSpeed, y, x + width + xSpeed,
                    y + height))
                x += xSpeed;
        }
        else if (x - p.GetX() > 150 && x - p.GetX() > 0)
        {
            if (!currentLevel.IsValidMove(x - xSpeed, y, x + width - xSpeed,
                    y + height))
                x -= xSpeed;
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
        if (xSpeed < 0)
            ChangeDirection(LEFT);
        else
            ChangeDirection(RIGHT);
        x = (short)(x + xSpeed);
        NextFrame();
    }
}

