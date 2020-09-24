using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;





public struct Pos2D
{
    public int x;
    public int y;

    public Pos2D(int x_, int y_)
    {
        x = x_;
        y = y_;
    }

    // numeric operators:
    public static Pos2D operator +(Pos2D pos1, Pos2D pos2) => new Pos2D(pos1.x + pos2.x, pos1.y + pos2.y);
    public static Pos2D operator -(Pos2D pos1, Pos2D pos2) => new Pos2D(pos1.x - pos2.x, pos1.y - pos2.y);
    public static Pos2D operator *(Pos2D pos1, int i) => new Pos2D(pos1.x * i, pos1.y * i);
    public static Pos2D operator *(int i, Pos2D pos1) => new Pos2D(pos1.x * i, pos1.y * i);
    public static Vector3 operator +(Vector3 v3, Pos2D pos) => new Vector3(v3.x + pos.x, v3.y, v3.z + pos.y);
    public static Vector3 operator +(Pos2D pos, Vector3 v3) => new Vector3(v3.x + pos.x, v3.y, v3.z + pos.y);
    public static Vector3 operator -(Vector3 v3, Pos2D pos) => new Vector3(v3.x - pos.x, v3.y, v3.z - pos.y);

    public static implicit operator Pos2D(int[] arr) => new Pos2D(arr[0], arr[1]);

    public static bool operator ==(Pos2D pos1, Pos2D pos2)
    {
        return pos1.x == pos2.x && pos1.y == pos2.y;
    }

    public static bool operator !=(Pos2D pos1, Pos2D pos2)
    {
        return pos1.x != pos2.x || pos1.y != pos2.y;
    }

    public override string ToString()
    {
        return String.Format("[{0},{1}]", x, y);
    }

    public Pos2D[] mirrorPositions()
    {
        return new Pos2D[] { new Pos2D(this.x, this.y), new Pos2D(-this.x, this.y), new Pos2D(this.x, -this.y), new Pos2D(-this.x, -this.y) };
    }

}


