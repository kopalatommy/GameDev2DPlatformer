using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Platformer.World;

namespace Platformer.Character
{
    //Base character movement controller
    public class KinematicCharacter : MonoBehaviour
    {
        #region MovementValues
        [Tooltip("Is the character currently allowed to move")]
        public bool canMove = true;
        [Tooltip("The amount of force the character should apply when jumping")]
        [SerializeField] protected float jumpForce = 100f;
        [Tooltip("The max speed of the character")]
        [SerializeField] protected float movementSpeed = 10f;
        [Tooltip("Values used when smoothing the characters movement")]
        [SerializeField] protected float movementSmoothing = 0.05f;
        [Tooltip("Can the character change directions when in the air")]
        [SerializeField] protected bool controlInAir = true;
        [Tooltip("A factor to adjust the strength of the characters movement when in the air")]
        [SerializeField] protected float airMovmentFactor = 0.75f;

        //Velocity of the character at the previous check
        private Vector2 velocity = Vector2.zero;

        //Is the character current on the ground
        protected bool isGrounded = false;
        //Is the character currently touching a wall
        protected bool isAgainstWall = false;
        #endregion

        #region WallJumpValues
        //If the character performs a wall jump, disable horizontal input for a small interval to
        //prevent wierdness
        private bool tempAirLock;
        private float tempAirLockTime;
        #endregion

        #region CollisionValues
        [Tooltip("The layers used by the character when performing a ground check")]
        [SerializeField] protected LayerMask groundLayers;
        [Tooltip("The location the character performs the ground check")]
        [SerializeField] protected Transform groundCheckPosition;
        [Tooltip("Radius of ground check")]
        [SerializeField] protected float groundCheckRadius = 0.2f;

        [Tooltip("Location the character performs a check to so if it is against a wall")]
        [SerializeField] protected Transform wallCheckLocation = null;

        [Tooltip("Layers that will kill the character on collision")]
        [SerializeField] private LayerMask killLayers;

        [Tooltip("Used to trigger platform movement")]
        [SerializeField] private LayerMask platformLayer;
        protected bool isOnPlatform = false;
        //Reference to the platform the character is currently on
        protected MovingPlatform platform = null;
        #endregion

        //Rigidbodies are used to inform the physics engine that this object can move
        protected Rigidbody2D _rigidbody = null;

        #region Events
        //Triggered when the character lands
        public UnityEvent onLandEvent = null;
        //Triggered when the character dies
        public UnityEvent onCharacterDie = null;
        #endregion

        //Used when determining if the character should flip sprite
        protected bool facingRight = true;

        //Handles setting up references and values used by the character
        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            //We dont want to the rigidbody to rotate the character
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

            if (onLandEvent == null)
                onLandEvent = new UnityEvent();

            if (onCharacterDie == null)
                onCharacterDie = new UnityEvent();
        }

        protected virtual void Update()
        {
            if (tempAirLock && tempAirLockTime < Time.time)
            {
                tempAirLock = false;
            }
        }

        protected void FixedUpdate()
        {
            if (!canMove)
                return;

            //Save the previous grounded state. Used when triggering the landed event
            bool wasGrounded = isGrounded;

            //Clear previous state bools that are calculated in this function
            isGrounded = false;
            isAgainstWall = false;

            isOnPlatform = false;
            platform = null;

            //Get all colliders that the ground check location overlaps
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPosition.position, 
                groundCheckRadius, //Radius of circle used in check
                groundLayers);  //Layers included in the check
            for(int i = 0; i < colliders.Length; i++)
            {
                //If the ground check layers include the character's layer, this check can include
                //the character
                if(colliders[i].gameObject != gameObject)
                {
                    //If any collider is found, and not the character, the character is grounded
                    isGrounded = true;
                    //If the character has just landed, invoke the event
                    if(!wasGrounded)
                    {
                        onLandEvent.Invoke();
                    }

                    //Check if the character is on a moving platform
                    //Layers are bitmasks, so need to check is the bit is in the mask
                    if(((1 << colliders[i].gameObject.layer) & platformLayer.value) != 0)
                    {
                        isOnPlatform = true;
                        platform = colliders[i].gameObject.GetComponent<MovingPlatform>();
                    }
                }
            }

            //Perform same check., but at the wall check location
            colliders = Physics2D.OverlapCircleAll(wallCheckLocation.position, 
                groundCheckRadius, 
                groundLayers);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    isAgainstWall = true;
                }
            }
        }

        /*private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(groundCheckPosition.position, groundCheckRadius);
        }*/

        //The move function handles applying forces to the character based on the received input
        //A positive direction will move the character to the right
        public void Move(float direction, bool jump)
        {
            if (!canMove)
                return;

            //Check if the character can currenly move
            if (!tempAirLock && (isGrounded || controlInAir))
            {
                //Determine what the max velocity should be
                Vector2 targetVelocity = new Vector2(direction * movementSpeed, _rigidbody.velocity.y);
                //Apply the air movement factor if not grounded
                if (!isGrounded)
                    targetVelocity.x = targetVelocity.x * airMovmentFactor;
                //This helps the character stick to the ground
                else if(isGrounded && targetVelocity.y < 3 && targetVelocity.y > 0)
                    targetVelocity.y = 0;

                //This prevents the character from becoming stuck on a wall.
                //This fixes that by removing all horizontal movement.
                //Another fix would be to have the character bounce off the wall
                if (!isGrounded && isAgainstWall)
                {
                    Vector2 vel = _rigidbody.velocity;
                    vel.x = 0;
                    _rigidbody.velocity = vel;
                    targetVelocity.x = 0;
                }

                //Updated the characters velocity. Vector2.SmoothDamp will make the movement look more fluid
                _rigidbody.velocity = Vector2.SmoothDamp(_rigidbody.velocity, targetVelocity, ref velocity, movementSmoothing);

                //Check if the character should flip its sprite
                if(direction > 0 && !facingRight)
                    FlipCharacter();
                else if(direction < 0 && facingRight)
                    FlipCharacter();
            }

            //If the character is on a moving platform, it needs to move with the platform
            //Move the amount equal to the platform's velocity
            if (isOnPlatform)
            {
                transform.Translate(platform.Velocity);
            }

            if (jump)
            {
                //If the character is grounded, jump straight up
                if(isGrounded)
                {
                    isGrounded = false;
                    //Remove the current gravity
                    Vector2 vel = _rigidbody.velocity;
                    vel.y = 0;
                    _rigidbody.velocity = vel;
                    //Have the rigidbody apply the jump force
                    _rigidbody.AddForce(new Vector2(0f, jumpForce));
                }
                //If the character is against a wall, jump away at  a 45 degree angle
                else if(isAgainstWall)
                {
                    isAgainstWall = false;
                    //Clear gravity
                    Vector2 vel = _rigidbody.velocity;
                    vel.y = 0;
                    _rigidbody.velocity = vel;
                    //Apply the jump force
                    _rigidbody.AddForce(new Vector2(jumpForce * -1 * transform.localScale.x, jumpForce));
                    //Flip the character to match the new direction
                    FlipCharacter();
                    //Lock movement half a sec to make it look better
                    tempAirLock = true;
                    tempAirLockTime = Time.time + 0.5f;
                }
            }
        }

        //Flips the character by reversing the x value of the scale
        protected virtual void FlipCharacter()
        {
            facingRight = !facingRight;

            Vector3 cScale = transform.localScale;
            cScale.x *= -1;
            transform.localScale = cScale;
        }

        //Triggered when the collider attached to the character enters a trigger
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            //Check if the character has entered the kill layer
            if (((1 << collision.gameObject.layer) & killLayers.value) != 0)
            {
                onCharacterDie.Invoke();
            }
        }
    }
}
