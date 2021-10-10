using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Character.Enemy
{
    public class SmartEnemyController : EnemyController
    {
        [SerializeField] private Transform floorCheckLocation = null;

        private float moveDir = 1;

        protected override void Update()
        {
            bool flip = false;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(floorCheckLocation.position, groundCheckRadius, groundLayers);
            for(int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    flip = true;
                    break;
                }
            }
            flip = !flip;

            if(!flip)
            {
                Debug.Log("Checking if agaisnt wall");
                colliders = Physics2D.OverlapCircleAll(wallCheckLocation.position, groundCheckRadius, groundLayers);
                Debug.Log("Collision count: " + colliders.Length);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        Debug.Log("Is against wall");
                        flip = true;
                        break;
                    }
                }
            }

            if (flip)
            {
                Debug.Log("No collisions");
                //FlipCharacter();
                Vector3 v = _rigidbody.velocity;
                v.x = 0;
                _rigidbody.velocity = v;
                moveDir *= -1;
            }

            Move(moveDir, false);
        }
    }
}
