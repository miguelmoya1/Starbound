using System;
class Rain : Sprite
{
    Level currentLevel;
    public Rain(int x, int y, int type, Level g)
    {
        if (type == 0)
            LoadImage("data/rain.jpg");
        else if (type == 1)
            LoadImage("data/snow.jpg");
        else if (type == 2)
            LoadImage("data/toxicRain.jpg");
        this.x = x;
        this.y = y;
        xSpeed = 0;
        Random r = new Random();
        ySpeed = r.Next(5, 10);
        width = 2;
        height = 2;
        currentLevel = g;
    }
    public override void Move()
    {
        if (currentLevel.IsValidMove(
                x, y + ySpeed, x + width, y + height + ySpeed))
            y += ySpeed;
        else
            y = 100;
    }
}