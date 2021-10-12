using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.World
{
    //This class holds information about the collectable it is attached to
    public class PointsCollectable : MonoBehaviour
    {
        [SerializeField] private int pointsValue = 100;

        public int PointsValue { get { return pointsValue; } }
    }
}

