using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.World
{
    public class PointsCollectable : MonoBehaviour
    {
        [SerializeField] private int pointsValue = 100;

        public int PointsValue { get { return pointsValue; } }
    }
}
