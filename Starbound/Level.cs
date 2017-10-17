using System;
using System.IO;

class Level {

    byte tileWidth, tileHeight, currentLevel;
    short levelWidth, levelHeight;
    byte leftMargin, topMargin;
    string[] levelDescription;
    Image floor, bg, lava, stone;
    Player player;

    public Level(Player p, byte l) {
        tileWidth = 16;
        tileHeight = 16;
        if (l == 0) {
            levelWidth = 511;
            levelHeight = 121;
        } else if (l == 1) {
            levelWidth = 82;
            levelHeight = 23;
        }
        leftMargin = 64;
        topMargin = 64;
        player = p;
        currentLevel = l;

        levelDescription = new string[levelHeight];

        LoadMap(currentLevel);
        if (currentLevel == 0) {
            floor = new Image("data\\floor.jpg");
            bg = new Image("data\\bgg.jpg");
            lava = new Image("data\\lava.png");
            stone = new Image("data\\stone.png");
        } else {
            floor = new Image("data\\floor.jpg");
            bg = new Image("data\\shed.png");
            lava = new Image("data\\lava.png");
            stone = new Image("data\\stone.png");
        }
    }

    public void LoadMap(byte map) {
        switch (map) {
            case 0:
                try {
                    StreamReader file = File.OpenText("map.sb");
                    string line;
                    int i = 0;
                    do {
                        line = file.ReadLine();
                        if (line != null) {
                            levelDescription[i] = line;
                            i++;
                        }
                    }
                    while (line != null);
                    file.Close();
                } catch (Exception ex) {
                    Console.WriteLine("Error: " + ex.Message);
                }
                break;
            case 1:
                try {
                    StreamReader file = File.OpenText("home.sb");
                    string line;
                    int i = 0;
                    do {
                        line = file.ReadLine();
                        if (line != null) {
                            levelDescription[i] = line;
                            i++;
                        }
                    }
                    while (line != null);
                    file.Close();
                } catch (Exception ex) {
                    Console.WriteLine("Error: " + ex.Message);
                }
                break;
        }
    }

    public Image GetBackGround() {
        return bg;
    }

    public void DrawOnHiddenScreen() {
        for (int row = 0; row < levelHeight; row++)
            for (int col = 0; col < levelWidth; col++) {
                int xPos = leftMargin + col * tileWidth;
                int yPos = topMargin + row * tileHeight;
                switch (levelDescription[row][col]) {
                    case '_': Hardware.DrawHiddenImage(floor, xPos, yPos); break;
                    case 'L': Hardware.DrawHiddenImage(lava, xPos, yPos); break;
                    case 'w': Hardware.DrawHiddenImage(stone, xPos, yPos); break;
                }
            }
    }

    public char GetPosicion(short x, short y) {
        try {
            if (y < levelWidth + 1 && x < levelHeight + 1 &&
                x >= 0 && y >= 0)
                return levelDescription[y][x];
            else
                return '.';
        } catch (Exception) {
            return '.';
        }
    }

    public void SetPosition(short x, short y, char c) {
        try {
            levelDescription[y] = levelDescription[y].Remove(x, 1);
            levelDescription[y] = levelDescription[y].Insert(x, "" + c);
        } catch (Exception) {
            Console.WriteLine("Error, cant do it");
        }
    }


    public void DeletePosition(short x, short y) {
        levelDescription[y] = levelDescription[y].Remove(x, 1);
        levelDescription[y] = levelDescription[y].Insert(x, ".");
    }



    public bool IsValidMove(int xMin, int yMin, int xMax, int yMax) {
        for (int row = 0; row < levelHeight; row++)
            for (int col = 0; col < levelWidth; col++) {
                char tileType = levelDescription[row][col];
                // If we don't need to check collisions with this tile, we skip it
                if ((tileType == '.') || (tileType == 'L'))
                    continue;
                // Otherwise, lets calculate its corners and check rectangular collisions
                int xPos = leftMargin + col * tileWidth;
                int yPos = topMargin + row * tileHeight;
                int xLimit = leftMargin + (col + 1) * tileWidth;
                int yLimit = topMargin + (row + 1) * tileHeight;

                if (Sprite.CheckCollisions(
                        xMin, yMin, xMax, yMax,  // Coords of the sprite
                        xPos, yPos, xLimit, yLimit)) // Coords of current tile
                    return false;
            }
        // If we have not collided with anything... then we can move
        return true;
    }

    public bool IsLava(int xMin, int yMin, int xMax, int yMax) {
        for (int row = 0; row < levelHeight; row++)
            for (int col = 0; col < levelWidth; col++) {
                char tileType = levelDescription[row][col];
                // If we don't need to check collisions with this tile, we skip it
                if (tileType != 'L')
                    continue;
                // Otherwise, lets calculate its corners and check rectangular collisions
                int xPos = leftMargin + col * tileWidth;
                int yPos = topMargin + row * tileHeight;
                int xLimit = leftMargin + (col + 1) * tileWidth;
                int yLimit = topMargin + (row + 1) * tileHeight;

                if (Sprite.CheckCollisions(
                        xMin, yMin, xMax, yMax,  // Coords of the sprite
                        xPos, yPos, xLimit, yLimit)) // Coords of current tile
                    return false;
            }
        // If we have not collided with anything... then we can move
        return true;
    }
}