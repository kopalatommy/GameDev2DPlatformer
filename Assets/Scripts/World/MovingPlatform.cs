using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer;

namespace Platformer.World
{
    public class MovingPlatform : MonoBehaviour
    {
        private int moveDirection = 1;
        [SerializeField] private float moveSpeed = 10;

        [SerializeField] private Vector2 startLoc = Vector2.zero;
        [SerializeField] private Vector2 endLoc = Vector2.zero;

        private Vector2 velocity = Vector2.zero;

        public Vector2 Velocity { get { return velocity; } }

        [SerializeField] private Vector2 moveDist;

        float interval = 3;
        float duration = 3; //secs
        float time;
        private void Update()
        {
            if(time < duration)
            {
                Vector2 prev = transform.position.ToVector2_XY();
                transform.position = Vector2.Lerp(startLoc, endLoc, time / duration);
                time += Time.deltaTime * moveSpeed;
                velocity = transform.position.ToVector2_XY() - prev;

                if(time > duration)
                {
                    Vector2 t = startLoc;
                    startLoc = endLoc;
                    endLoc = t;
                    time = 0;
                    duration = interval;
                }
            }

            /*float t = 1 / Time.time;
            t = t * t * (3f - 2f * t);

            transform.position = Vector3(startPosition, endPosition, t);

            start = Vector2.Lerp(start, end, Time.deltaTime);
            Debug.Log(Time.time + " : " + start);*/

            /*Vector2 prevPos = transform.position;
            moveDist = Vector2.Lerp(prevPos, moveDirection > 0 ? endLoc : startLoc, moveSpeed * Time.deltaTime) - prevPos;


            transform.Translate(moveDist);
            if (transform.position.ToVector2_XY() == endLoc)
            {
                moveDirection = -1;
            }
            else if(transform.position.ToVector2_XY() == startLoc)
            {
                moveDirection = 1;
            }

            velocity = transform.position.ToVector2_XY() - prevPos;*/
        }
    }
}
