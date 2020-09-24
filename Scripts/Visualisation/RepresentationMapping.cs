using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepresentationMapping
{

    public static readonly float SCALINGFACTOR = 1;

    public static Vector3 MapBoardPos2D(Pos2D pos)
    {
        return (Vector3.one + pos) * SCALINGFACTOR;
    }
}
