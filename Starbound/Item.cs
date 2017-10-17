
using System;

class Item : Sprite {
    char type;
    Level l;
    Text t;
    Font f;

    public Item(int x, int y, char c, Level level) {
        t = new Text();
        f = new Font("data/Joystix.ttf", 12);
        total = 1;
        this.x = x;
        this.y = y;
        ySpeed = 1;
        height = 36;
        width = 36;
        type = c;
        switch (c) {
            case '_':
                LoadImage("data/floor1.jpg");
                break;
            case 'w':
                LoadImage("data/stoneFloor.png");
                break;
        }
        l = level;
    }

    public Item(char c) {
        t = new Text();
        f = new Font("data/Joystix.ttf", 12);
        type = c;
        total = 0;
        switch (c) {
            case 'w':
                LoadImage("data/stoneInventory.png");
                break;
            case '_':
                LoadImage("data/floorInventory.png");
                break;
        }
    }
    public override void MoreItems() {
        total++;
    }

    public override char GetChar() {
        return type;
    }

    public override void LessItems() {
        if (total > 0)
            total--;
    }
    public override void SetX(int x) {
        this.x = x;
        t.SetX((short)(x + height));
    }

    public override void SetY(int y) {
        this.y = y;
        t.SetY((short)(y + width));
    }

    public override void DrawOnHiddenScreen() {
        if (!visible)
            return;
        if (total > 0)
            Hardware.DrawHiddenImage(image, x, y);
        if (total > 1)
            t.DrawText(
            Convert.ToString(total), f);
    }
}

