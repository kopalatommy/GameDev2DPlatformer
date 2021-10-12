using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Character.Enemy
{
    public class SmartEnemyController : EnemyController
    {
        [Tooltip("Location to check if the floor is present")]
        [SerializeField] protected Transform floorCheckLocation = null;

        private int moveDirection = -1;

        protected override void Update()
        {
            //Should the character change direction
            bool flip = false;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(floorCheckLocation.position, groundCheckRadius, groundLayers);
            //Check if there is ground
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    flip = true;
                    break;
                }
            }
            //Since this checks for ground, a true condition means that this shouldn't flip
            flip = !flip;

            //If this is already flipping, then no need to perform this check
            if (!flip)
            {
                //Perform same wall check because the other one may not have been called yet
                colliders = Physics2D.OverlapCircleAll(wallCheckLocation.position, groundCheckRadius, groundLayers);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        flip = true;
                        break;
                    }
                }
            }

            if (flip)
            {
                //Prevent the rigidbody from carrying the character over the ledge
                Vector3 v = _rigidbody.velocity;
                v.x = 0;
                _rigidbody.velocity = v;
                //Flip direction, not character
                moveDirection *= -1;
            }
            //ApplyMovement
            Move(moveDirection, false);
        }
    }
}
