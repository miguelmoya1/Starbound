
using System.Collections.Generic;

class Tool
{
    protected Sprite image;
    protected List<Sprite> item;
    protected int actual;
    public Tool(Sprite sprite)
    {
        actual = 0;
        image = sprite;
        const int SIZEINVENTORY = 40;
        item = new List<Sprite>();
    }


    public void AddItem(Sprite item)
    {
        this.item.Add(item);
    }

    public void SetItemAt(int i, Sprite item)
    {
        this.item[i] = item;
    }

    public Sprite GetItemAt(int i)
    {
        return item.Count > i?item[i]:null;
        
    }

    public void RemoveItemAt(int i)
    {
        item[i] = null;
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

    public void SetImageX(int x, int i)
    {
        item[i].SetX(x);
    }

    public void SetImageY(int y, int i)
    {
        item[i].SetY(y);
    }


    public void DrawItem(int i)
    {
        item[i].DrawOnHiddenScreen();
    }

    public void AddItems(int i)
    {
        item[i].AddItems();
    }

    public void LessItems(int i)
    {
        item[i].AddItems();
    }
}
