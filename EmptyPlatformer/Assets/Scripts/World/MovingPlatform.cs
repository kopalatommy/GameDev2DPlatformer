using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.WorldObjects
{
    //This class handles moving platforms
    public class MovingPlatform : MonoBehaviour
    {
        [Tooltip("Time in seconds for platform to complete interval")]
        [SerializeField] private float interval = 3;
        //Current time elapsed for interval
        private float duration = 0; //secs
        //End time for interval
        private float time = 0;

        [Tooltip("The starting position of the platform")]
        [SerializeField] private Vector2 startLoc = Vector2.zero;
        [Tooltip("The ending positon of the platform movement interval")]
        [SerializeField] private Vector2 endLoc = Vector2.zero;

        //The previous velocity of the platform
        private Vector2 velocity = Vector2.zero;

        public Vector2 Velocity { get { return velocity; } }

        private void Update()
        {
            if (time < duration)
            {
                Vector2 prev = transform.position.ToVector2_XY();
                transform.position = Vector2.Lerp(startLoc, endLoc, time / duration);
                time += Time.deltaTime;
                velocity = transform.position.ToVector2_XY() - prev;

                if (time > duration)
                {
                    Vector2 t = startLoc;
                    startLoc = endLoc;
                    endLoc = t;
                    time = 0;
                    duration = interval;
                }
            }
        }
    }
}
