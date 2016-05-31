
using System.Collections.Generic;

class Tool
{
    protected Sprite image;
    protected List<Item>[] item;
    public Tool(Sprite sprite)
    {
        image = sprite;
        const int SIZEINVENTORY = 30;
        item = new List<Item>[SIZEINVENTORY];
        for (int i = 0; i < item.Length; i++)
            item[i] = new List<Item>();
    }

    public void SetItemAt(int i, Item item)
    {
        this.item[i].Add(item);
    }

    public void DrawOnHiddenScreen()
    {
        image.DrawOnHiddenScreen();
    }

    public void SetX(int x)
    {
        image.SetX(x); 
    }

    public void SetY(int y)
    {
        image.SetY(y);
    }
}
