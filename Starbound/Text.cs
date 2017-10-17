class Text {
    protected short x, y;
    protected byte r, g, b, delayText;
    protected string damage;

    public Text() {
        r = g = b = 255;
    }

    public Text(short x, short y, byte r, byte g, byte b,
            byte delayText, string damage) {
        this.x = x;
        this.y = y;
        this.r = r;
        this.g = g;
        this.b = b;
        this.delayText = delayText;
        this.damage = damage;
    }


    public void SetX(short x) {
        this.x = x;
    }

    public void SetY(short y) {
        this.y = y;
    }

    public byte GetDelay() {
        return delayText;
    }

    public void SetDelay(byte delay) {
        delayText = delay;
    }

    public void SetRGB(byte r, byte g, byte b) {
        this.r = r;
        this.g = g;
        this.b = b;
    }

    public short GetY() {
        return y;
    }

    public void DrawText(string t, Font f) {
        Hardware.WriteHiddenText(t, x, y, r, g, b, f);
    }

    public void DrawText(Font f) {
        Hardware.WriteHiddenText(damage, x, y, r, g, b, f);
    }
}

