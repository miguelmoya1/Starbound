using System.Collections.Generic;

class Player : Sprite {
    protected Game myGame;
    protected List<Item>[] inventory;
    protected bool jumping, falling;
    protected int jumpXspeed, positionOfInventory;
    protected int jumpFrame;
    protected int[] jumpSteps =
    {
             -32, -32, -32, -16, -16, -16, -16, -8, -8, -4, -4, -1, -1, 0,
            0, 1, 1, 4, 4, 8, 8, 16, 16, 16, 16, 32, 32, 32
    };

    public Player(Game g) {
        myGame = g;
        LoadSequence(LEFT,
            new string[] { "data/playerLeft1.png", "data/playerLeft2.png",
            "data/playerLeft3.png","data/playerLeft4.png"});

        LoadSequence(RIGHT,
            new string[] { "data/PlayerRight1.png", "data/playerRight2.png",
            "data/playerRight3.png","data/playerRight4.png"});

        ChangeDirection(RIGHT);
        const int SIZEINVENTORY = 40;
        inventory = new List<Item>[SIZEINVENTORY];
        for (int i = 0; i < inventory.Length; i++)
            inventory[i] = new List<Item>();
        x = 480;
        startX = x;
        y = 368;
        startY = y;
        xSpeed = 8;
        ySpeed = 8;
        width = 32;
        height = 63;
        positionOfInventory = 0;
        jumpXspeed = 0;
        jumping = false;
        jumpFrame = 0;
        falling = false;
    }

    public int GetStartX() {
        return startX;
    }

    public int GetStartY() {
        return startY;
    }

    public void MoveRight() {
        const int UPTORAMP = 32;
        if (myGame.IsValidMove(x + xSpeed, y, x + width + xSpeed,
            y + height)) {
            x += xSpeed;
            NextFrame();
        } else if (myGame.IsValidMove(x + xSpeed, y - UPTORAMP, x + width +
              xSpeed, y - UPTORAMP + height)) {
            y -= UPTORAMP;
            x += xSpeed;
        }
    }

    public void MoveLeft() {
        const int UPTORAMP = 32;
        if (myGame.IsValidMove(x - xSpeed, y, x + width - xSpeed,
            y + height)) {
            x -= xSpeed;
            NextFrame();
        } else if (myGame.IsValidMove(x - xSpeed, y - UPTORAMP, x + width -
             xSpeed, y - UPTORAMP + height)) {
            y -= UPTORAMP;
            x -= xSpeed;
        }
    }


    public void Jump() {
        if (jumping || falling)
            return;
        jumping = true;
        jumpXspeed = 0;
    }


    // Starts the jump sequence to the right
    public void JumpRight() {
        Jump();
        jumpXspeed = xSpeed;
    }


    public void JumpLeft() {
        Jump();
        jumpXspeed = -xSpeed;
    }

    public override void Move() {

        // If the player is not jumping, it might need to fall down
        if (!jumping) {
            if (myGame.IsValidMove(
                x, y + ySpeed, x + width, y + height + ySpeed)) {
                y += ySpeed;
            }
        } else
          // If jumping, it must go on with the sequence
          {
            // Let's calculate the next positions
            short nextX = (short)(x + jumpXspeed);
            short nextY = (short)(y + jumpSteps[jumpFrame]);

            // If the player can still move, let's do it
            if (myGame.IsValidMove(
                nextX, nextY,
                nextX + width, nextY + height)) {
                x = nextX;
                y = nextY;
            }
            // If it cannot move, then it must fall
            else {
                jumping = false;
                jumpFrame = 0;
            }

            // And let's prepare the next frame, maybe with a different speed
            jumpFrame++;
            if (jumpFrame >= jumpSteps.Length) {
                jumping = false;
                jumpFrame = 0;
            }
        }
    }
    /// <summary>
    /// To break the stone
    /// </summary>
    /// <param name="CurrentLevel"></param>
    public bool Break(Level CurrentLevel, int x, int y) {
        if ((CurrentLevel.GetPosicion((short)x, (short)y) == '_' ||
            CurrentLevel.GetPosicion((short)x, (short)y) == 'w')
            && Mouse.Clic() == 1) {
            CurrentLevel.DeletePosition((short)x, (short)y);
            return true;
        }
        return false;
    }


    public void AddToInventory(Item toAdd) {
        bool contains = false;
        for (int i = 0; i < positionOfInventory && !contains; i++)
            if (inventory[i].Contains(toAdd)) {
                inventory[i].Add(toAdd);
                contains = true;
            }
        if (!contains && positionOfInventory < 39) {
            inventory[positionOfInventory].Add(toAdd);
            positionOfInventory++;
        }
    }

    public void RemoveInventory() {
        // TODO
    }

    public bool ContainsItems() {
        return inventory.Length > 0;
    }

    public Item GetItemAt(int i) {
        return inventory[i][0];
    }
}
