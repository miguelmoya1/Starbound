
class Tool
{
    protected Sprite image;

    public Tool(Sprite i)
    {
        image = i;
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
