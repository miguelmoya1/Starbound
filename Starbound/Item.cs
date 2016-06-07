
class Item : Sprite
{
    char type;
    Level l;
    int total;
    public Item(int x, int y, char c, Level level)
    {
        total = 0;
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

    // to see how many items I have
    public void MoreItems()
    {
        total++;
    }

    public void LessItems()
    {
        if (total > 0)
            total--;
    }
}

