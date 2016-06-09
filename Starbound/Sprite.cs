class Sprite
{
    protected int x, y;
    protected int startX, startY;
    protected int width, height;
    protected int xSpeed, ySpeed;
    protected bool visible;
    protected Image image;
    protected Image[][] sequence;
    protected bool containsSequence;
    protected int currentFrame;
    protected int total;

    protected byte numDirections = 4;
    public byte currentDirection;
    public const byte RIGHT = 0;
    public const byte LEFT = 1;
    public const byte DOWN = 2;
    public const byte UP = 3;


    public Sprite()
    {
        startX = 480;
        startY = 568;
        width = 16;
        height = 16;
        visible = true;
        sequence = new Image[numDirections][];
        currentDirection = RIGHT;
    }

    public Sprite(string imageName)
        : this()
    {
        LoadImage(imageName);
    }

    public int GetTotal()
    {
        return total;
    }

    public virtual void SetX(int x)
    {
        this.x = x;
    }
    public virtual void SetY(int y)
    {
        this.y = y;
    }
    public void AddItems()
    {
        // TO DO
    }

    public virtual void LessItems()
    {
        // TO DO
    }

    public virtual char GetChar()
    {
        return ' ';
        // TO DO
    }

    public virtual void MoreItems()
    {
        // TO DO
    }

    public Sprite(string[] imageNames)
        : this()
    {
        LoadSequence(imageNames);
    }

    public void LoadImage(string name)
    {
        image = new Image(name);
        containsSequence = false;
    }

    public void LoadSequence(byte direction, string[] names)
    {
        int amountOfFrames = names.Length;
        sequence[direction] = new Image[amountOfFrames];
        for (int i = 0; i < amountOfFrames; i++)
            sequence[direction][i] = new Image(names[i]);
        containsSequence = true;
        currentFrame = 0;
    }

    public void LoadSequence(string[] names)
    {
        LoadSequence(RIGHT, names);
    }

    public int GetX()
    {
        return x;
    }

    public int GetY()
    {
        return y;
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public int GetSpeedX()
    {
        return xSpeed;
    }

    public int GetSpeedY()
    {
        return ySpeed;
    }

    public bool IsVisible()
    {
        return visible;
    }

    public void MoveTo(int newX, int newY)
    {
        x = newX;
        y = newY;
        if (startX == -1)
        {
            startX = x;
            startY = y;
        }
    }

    public void SetSpeed(int newXSpeed, int newYSpeed)
    {
        xSpeed = newXSpeed;
        ySpeed = newYSpeed;
    }

    public void Show()
    {
        visible = true;
    }

    public void Hide()
    {
        visible = false;
    }

    public virtual void Move()
    {
    }

    public virtual void DrawOnHiddenScreen()
    {
        if (!visible)
            return;

        if (containsSequence)
            Hardware.DrawHiddenImage(
                sequence[currentDirection][currentFrame], x, y);
        else
            Hardware.DrawHiddenImage(image, x, y);
    }

    public void NextFrame()
    {
        currentFrame++;
        if (currentFrame >= sequence[currentDirection].Length)
            currentFrame = 0;
    }

    public void ChangeDirection(byte newDirection)
    {
        if (!containsSequence) return;
        if (currentDirection != newDirection)
        {
            currentDirection = newDirection;
            currentFrame = 0;
        }

    }

    public bool CollisionsWith(Sprite otherSprite)
    {
        return (visible && otherSprite.IsVisible() &&
            CollisionsWith(otherSprite.GetX(),
                otherSprite.GetY(),
                otherSprite.GetX() + otherSprite.GetWidth(),
                otherSprite.GetY() + otherSprite.GetHeight()));
    }

    public bool CollisionsWith(int xStart, int yStart, int xEnd, int yEnd)
    {
        if (visible &&
                (x < xEnd) &&
                (x + width > xStart) &&
                (y < yEnd) &&
                (y + height > yStart)
                )
            return true;
        return false;
    }

    public void Restart()
    {
        x = startX;
        y = startY;
    }

    public static bool CheckCollisions(
        int r1xStart, int r1yStart, int r1xEnd, int r1yEnd,
        int r2xStart, int r2yStart, int r2xEnd, int r2yEnd)
    {
        if ((r2xStart < r1xEnd) &&
                (r2xEnd > r1xStart) &&
                (r2yStart < r1yEnd) &&
                (r2yEnd > r1yStart)
                )
            return true;
        else
            return false;
    }
}
