
using System.Collections.Generic;

class Tool {
    protected Sprite image;
    protected List<Sprite> item;
    protected int actual;
    public Tool(Sprite sprite) {
        actual = 0;
        image = sprite;
        item = new List<Sprite>();
    }

    public int TotalItems() {
        return item.Count;
    }

    public bool ContainsChar(char c) {
        for (int i = 0; i < item.Count; i++)
            if (item[i].GetChar() == c)
                return true;
        return false;
    }
    public void AddItem(Sprite item) {
        this.item.Add(item);
    }

    public void SetItemAt(int i, Sprite item) {
        this.item[i] = item;
    }

    public Sprite GetItemAt(int i) {
        return item.Count > i ? item[i] : null;

    }

    public void RemoveItemAt(int i) {
        item[i] = null;
    }

    public void DrawOnHiddenScreen() {
        image.DrawOnHiddenScreen();
    }

    public void SetX(int x) {
        image.SetX(x);
    }

    public void SetY(int y) {
        image.SetY(y);
    }

    public void SetImageX(int x, int i) {
        item[i].SetX(x);
    }

    public void SetImageY(int y, int i) {
        item[i].SetY(y);
    }


    public void DrawItem(int i) {
        item[i].DrawOnHiddenScreen();
        for (int j = 0; j < item.Count; j++) {
            if (item[j].GetTotal() == 0 &&
                        item[j].GetChar() != 'c' &&
                        item[j].GetChar() != 'p')
                item.RemoveAt(j);
        }

        for (int j = 1; j < item.Count; j++)
            for (int k = j + 1; k < item.Count; k++)
                if (item[j].GetChar() == item[k].GetChar())
                    item.RemoveAt(k);


    }

    public void AddItems(int i) {
        item[i].MoreItems();
    }

    public void LessItems(int i) {
        item[i].AddItems();
    }
}
