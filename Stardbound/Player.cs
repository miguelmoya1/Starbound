
class Player : Sprite
{
    protected Game myGame;
    protected bool jumping, falling;
    protected int jumpXspeed;
    protected int jumpFrame;
    protected int[] jumpSteps =
    {
            -32, -32, -32, -32, -16, -16, -16, -16, -16, -16, -8, -8, -8, -4, -4, -1, -1, 0,
            0, 1, 1, 4, 4, 8, 8, 8, 8, 16, 16, 16, 16, 16, 16, 32, 32, 32, 32
    };

    public Player(Game g)
    {
        myGame = g;
        LoadSequence(LEFT,
            new string[] { "data/playerLeft1.png", "data/playerLeft2.png",
            "data/playerLeft3.png","data/playerLeft4.png"});

        LoadSequence(RIGHT,
            new string[] { "data/PlayerRight1.png", "data/playerRight2.png",
            "data/playerRight3.png","data/playerRight4.png"});

        ChangeDirection(RIGHT);

        x = 480;
        y = 368;
        xSpeed = 8;
        ySpeed = 8;
        width = 64;
        height = 63;
        jumpXspeed = 0;
        jumping = false;
        jumpFrame = 0;
        falling = false;
    }


    public void SetHeight(int h)
    {
        this.height = h;
    }

    public void SetY(int y)
    {
        this.y = y;
    }

    public void MoveRight()
    {
        if (myGame.IsValidMove(x + xSpeed, y, x + width + xSpeed,
            y + height))
        {
            x += xSpeed;
            ChangeDirection(RIGHT);
            NextFrame();
        }
    }

    public void MoveLeft()
    {
        if (myGame.IsValidMove(x - xSpeed, y, x + width - xSpeed,
            y + height))
        {
            x -= xSpeed;
            ChangeDirection(LEFT);
            NextFrame();
        }
    }

    public void MoveUp()
    {
        Jump();
    }

    public void Jump()
    {
        if (jumping || falling)
            return;
        jumping = true;
        jumpXspeed = 0;
    }


    // Starts the jump sequence to the right
    public void JumpRight()
    {
        Jump();
        jumpXspeed = xSpeed;
    }


    public void JumpLeft()
    {
        Jump();
        jumpXspeed = -xSpeed;
    }

    public override void Move()
    {
        // If the player is not jumping, it might need to fall down
        if (!jumping)
        {
            if (myGame.IsValidMove(
                x, y + ySpeed, x + width, y + height + ySpeed))
            {
                y += ySpeed;
            }
        }
        else
        // If jumping, it must go on with the sequence
        {
            // Let's calculate the next positions
            short nextX = (short)(x + jumpXspeed);
            short nextY = (short)(y + jumpSteps[jumpFrame]);

            // If the player can still move, let's do it
            if (myGame.IsValidMove(
                nextX, nextY,
                nextX + width, nextY + height))
            {
                x = nextX;
                y = nextY;
            }
            // If it cannot move, then it must fall
            else
            {
                jumping = false;
                jumpFrame = 0;
            }

            // And let's prepare the next frame, maybe with a different speed
            jumpFrame++;
            if (jumpFrame >= jumpSteps.Length)
            {
                jumping = false;
                jumpFrame = 0;
            }
        }
    }

    public void Break(Level CurrentLevel)
    {
        int xMouse = (Mouse.GetX() + (x - 545)) / 16;
        int yMouse = (Mouse.GetY() + (y - 433)) / 16;
        if (CurrentLevel.GetPosicion((short)xMouse, (short)yMouse) == '_'
            && Mouse.Clic() == 1)
        {
            CurrentLevel.DeletePosition((short)xMouse, (short)yMouse);
        }
    }
}
