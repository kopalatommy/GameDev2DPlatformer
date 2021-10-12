using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    //Additions to the Vector3 class
    public static class Vector3Additions
    {
        public static Vector2 ToVector2_XY(this Vector3 vec)
        {
            return new Vector2(vec.x, vec.y);
        }
    }
}
