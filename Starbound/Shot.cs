class Shot : Sprite
{
    protected Game myGame;
    protected Weapon w;

    public Shot(Game g, int x, int y, int xSpeed, Weapon w, int dirreccion)
    {
        LoadSequence(LEFT,
            new string[] { "data/Ammo1Left.png" });
        LoadSequence(RIGHT,
            new string[] { "data/Ammo1Right.png" });

        this.w = w;
        this.x = x;
        this.y = y;
        xSpeed = 20;
        this.xSpeed = xSpeed;
        if (dirreccion == 1)
            currentDirection = LEFT;
        else
            currentDirection = RIGHT;
        width = 16;
        height = 16;
        myGame = g;
    }

    public void Move(int xPlayer)
    {
        const int SIZE = 700;
        if (xPlayer - x < SIZE && x - xPlayer < SIZE)
        {
            if (myGame.IsValidMove(this.x + xSpeed, y, this.x + width + xSpeed, y + height))
            {
                if (currentDirection == 1)
                    x -= xSpeed;
                else
                    x += xSpeed;
            }
            else
                DeleteShot(this);
        }
        else
            DeleteShot(this);
            
    }
    public void DeleteShot(Shot s)
    {
        w.DeleteShot(s);
    }
}