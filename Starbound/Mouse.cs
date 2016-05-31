class Mouse
{
    public static bool ColisionWith(int xMin, int yMin, int xMax, int yMax,
            bool clic)
    {
        int x, y;
        int mouse = Hardware.GetMouse(out x, out y);
        if (clic)
        {
            if (mouse == 1 && ((x >= xMin && x <= xMax) && (y >= yMin && y <= yMax)))
                return true;
        }
        else 
            if (mouse == 4 && ((x >= xMin && x <= xMax) && (y >= yMin && y <= yMax)))
                return true;
        return false;
    }

    public static int GetX()
    {
        int x, y;
        Hardware.GetMouse(out x, out y);
        return x;
    }

    public static int GetY()
    {
        int x, y;
        Hardware.GetMouse(out x, out y);
        return y;
    }


    public static int Clic()
    {
        int x, y;
        return Hardware.GetMouse(out x, out y);
    }
}
