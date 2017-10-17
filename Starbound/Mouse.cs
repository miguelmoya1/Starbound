class Mouse {
    public static bool ColisionWith(int xMin, int yMin, int xMax, int yMax,
            bool clic) {
        if (clic) {
            if (Hardware.GetMouse(out int x, out int y) == 1 && ((x >= xMin && x <= xMax) && (y >= yMin && y <= yMax)))
                return true;
        } else
            if (Hardware.GetMouse(out int x, out int y) == 4 && ((x >= xMin && x <= xMax) && (y >= yMin && y <= yMax)))
            return true;
        return false;
    }

    public static int GetX() {
        Hardware.GetMouse(out int x, out int y);
        return x;
    }

    public static int GetY() {
        Hardware.GetMouse(out int x, out int y);
        return y;
    }


    public static int Clic() {
        return Hardware.GetMouse(out int x, out int y);
    }
}
