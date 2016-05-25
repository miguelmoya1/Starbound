
class Item : Sprite
{
    char type;

    public Item(int x, int y, char c)
    {
        this.x = x;
        this.y = y;
        type = c;
        switch (type)
        {
            case '_':
                image = new Image("data/floor1.jpg");
                break;
        }
    }


}

