
class Item : Sprite
{
    char type;
    Level l;
    public Item(int x, int y, char c, Level level)
    {
        this.x = x;
        this.y = y;
        ySpeed = 1;
        height = 7;
        width = 7;
        type = c;
        switch (c)
        {
            case '_':
                LoadImage("data/floor1.jpg");
                break;
        }
        l = level;
    }
}

