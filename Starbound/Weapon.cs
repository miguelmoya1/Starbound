using System.Collections.Generic;

class Weapon : Sprite
{
    protected int damage;
    protected List<Shot> shot;

    public Weapon(int damage)
    {
        this.damage = damage;
        LoadSequence(LEFT,
            new string[] { "data/Weapon1Left.png" });
        LoadSequence(RIGHT,
            new string[] { "data/Weapon1Right.png" });
        shot = new List<Shot>();
    }

    public void Shot(Game g, int x, int y)
    {
        int xSpeed = 5;
        shot.Add(new Shot(g, x, y, xSpeed, this, currentDirection));
    }

    public void DrawShot()
    {
        for (int i = 0; i < shot.Count; i++)
            shot[i].DrawOnHiddenScreen();
    }


    public void MoveShot(int x)
    {
        for (int i = 0; i < shot.Count; i++)
            shot[i].Move(x);
    }

    public void DeleteShot(Shot s)
    {
        shot.Remove(s);
    }

    public bool ShotCollisionsWith(Sprite s)
    {
        for (int i = 0; i < shot.Count; i++)
            if (shot[i].CollisionsWith(s))
            {
                DeleteShot(shot[i]);
                return true;
            }
        return false;
    }

    public int GetDamage()
    {
        return damage;
    }
}

