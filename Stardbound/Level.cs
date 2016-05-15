﻿using System;
using System.IO;

class Level
{
    byte tileWidth, tileHeight;
    short levelWidth, levelHeight;
    byte leftMargin, topMargin, rigthMargin, bottomMargin;
    string[] levelDescription;
    Image floor, bg;
    Player player;

    public Level(Player p)
    {
        tileWidth = 16;
        tileHeight = 16;
        levelWidth = 511;
        levelHeight = 121;
        leftMargin = 64;
        rigthMargin = 64;
        bottomMargin = 64;
        topMargin = 64;
        player = p;

        levelDescription = new string[121];
        try
        {
            StreamReader file = File.OpenText("map.sb");
            string line;
            int i = 0;
            do
            {
                line = file.ReadLine();
                if (line != null)
                {
                    levelDescription[i] = line;
                    i++;
                }
            }
            while (line != null);
            file.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        floor = new Image("data\\floor.jpg");
        bg = new Image("data\\bgg.jpg");
    }
    public void DrawOnHiddenScreen()
    {
        Hardware.DrawHiddenImage(bg, player.GetX() - 480, 0);
        for (int row = 0; row < levelHeight; row++)
            for (int col = 0; col < levelWidth; col++)
            {
                int xPos = leftMargin + col * tileWidth;
                int yPos = topMargin + row * tileHeight;
                switch (levelDescription[row][col])
                {
                    case '_': Hardware.DrawHiddenImage(floor, xPos, yPos); break;
                }
            }
    }

    public char GetPosicion(short x, short y)
    {
        try
        {
            return levelDescription[y][x];
        }
        catch (Exception)
        {
            return ' ';
        }
    }


    public void DeletePosition(short x, short y)
    {
        levelDescription[y] = levelDescription[y].Remove(x, 1);
        levelDescription[y] = levelDescription[y].Insert(x, ".");
    }



    public bool IsValidMove(int xMin, int yMin, int xMax, int yMax)
    {
        for (int row = 0; row < levelHeight; row++)
            for (int col = 0; col < levelWidth; col++)
            {
                char tileType = levelDescription[row][col];
                // If we don't need to check collisions with this tile, we skip it
                if ((tileType == '.') || (tileType == 'z'))
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