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
        [SerializeField] protected float jumpForce = 100f;
        [SerializeField] protected float movementSmoothing = 0.05f;
        [SerializeField] protected float movementSpeed = 10f;

        [SerializeField] protected LayerMask groundLayers;
        [SerializeField] protected Transform groundCheckPosition;
        [SerializeField] protected float groundCheckRadius = 0.2f;

        [SerializeField] protected Transform wallCheckLocation = null;

        [SerializeField] protected Rigidbody2D _rigidbody = null;

        [SerializeField] protected bool controlInAir = true;
        [SerializeField] protected float airMovmentFactor = 0.75f;

        [SerializeField] protected bool isGrounded = false;
        [SerializeField] protected bool againstWall = false;
        private bool tempAirLock;
        private float tempAirLockTime;
        protected bool facingRight = true;

        private Vector2 velocity;

        //Character events
        public UnityEvent onLandEvent = null;
        public UnityEvent onFlipCharacterEvent = null;
        public UnityEvent onCharacterDie = null;

        [SerializeField] private LayerMask killLayers;

        [SerializeField] private LayerMask platformLayer;
        protected bool onPlatform = false;
        protected MovingPlatform platform;

        public bool canMove = true;

        protected virtual void Awake()
        {
            SetUpCharacter();
        }

        protected virtual void SetUpCharacter()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            if (onLandEvent == null)
                onLandEvent = new UnityEvent();

            if (onCharacterDie == null)
                onCharacterDie = new UnityEvent();
        }

        private void Update()
        {
            OnUpdate();
        }

        protected void OnUpdate()
        {
            if (!canMove)
                return;

            if (tempAirLock && tempAirLockTime < Time.time)
            {
                tempAirLock = false;
            }
        }

        protected void FixedUpdate()
        {
            if (!canMove)
                return;

            bool wasGrounded = isGrounded;
            isGrounded = false;
            againstWall = false;

            onPlatform = false;
            platform = null;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPosition.position, groundCheckRadius, groundLayers);
            for(int i = 0; i < colliders.Length; i++)
            {
                if(colliders[i].gameObject != gameObject)
                {
                    isGrounded = true;
                    if(!wasGrounded)
                    {
                        onLandEvent.Invoke();
                    }

                    if(((1 << colliders[i].gameObject.layer) & platformLayer.value) != 0)
                    {
                        onPlatform = true;
                        platform = colliders[i].gameObject.GetComponent<MovingPlatform>();
                    }
                }
            }

            colliders = Physics2D.OverlapCircleAll(wallCheckLocation.position, groundCheckRadius, groundLayers);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    againstWall = true;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(groundCheckPosition.position, groundCheckRadius);
        }

        public void Move(float direction, bool jump)
        {
            if (!canMove)
                return;

            if (!tempAirLock && (isGrounded || controlInAir))
            {
                Vector2 targetVelocity = new Vector2(direction * movementSpeed, _rigidbody.velocity.y);
                if (!isGrounded)
                    targetVelocity.x = targetVelocity.x * airMovmentFactor;
                else if(isGrounded && targetVelocity.y < 3)
                    targetVelocity.y = 0;

                if (!isGrounded && againstWall)
                {
                    Vector2 vel = _rigidbody.velocity;
                    vel.x = 0;
                    _rigidbody.velocity = vel;
                    targetVelocity.x = 0;
                }

                _rigidbody.velocity = Vector2.SmoothDamp(_rigidbody.velocity, targetVelocity, ref velocity, movementSmoothing);

                if(direction > 0 && !facingRight)
                {
                    FlipCharacter();
                }
                else if(direction < 0 && facingRight)
                {
                    FlipCharacter();
                }
            }

            if (onPlatform)
            {
                transform.Translate(platform.Velocity);
            }

            if (jump)
            {
                if(isGrounded)
                {
                    isGrounded = false;
                    Vector2 vel = _rigidbody.velocity;
                    vel.y = 0;
                    _rigidbody.velocity = vel;
                    _rigidbody.AddForce(new Vector2(0f, jumpForce));
                }
                else if(againstWall)
                {
                    againstWall = false;
                    Vector2 vel = _rigidbody.velocity;
                    vel.y = 0;
                    _rigidbody.velocity = vel;
                    _rigidbody.AddForce(new Vector2(jumpForce * -1 * transform.localScale.x, jumpForce));
                    FlipCharacter();
                    tempAirLock = true;
                    tempAirLockTime = Time.time + 0.5f;
                    //Debug.Log("Started air lock at " + Time.time + " : " + tempAirLockTime);
                }
            }
        }

        protected virtual void FlipCharacter()
        {
            facingRight = !facingRight;

            Vector3 cScale = transform.localScale;
            cScale.x *= -1;
            transform.localScale = cScale;

            onFlipCharacterEvent.Invoke();
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            HandleTrigger(collision);
        }

        /*protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (((1 << collision.gameObject.layer) & platformLayer.value) != 0)
            {
                onPlatform = true;
                platform = collision.gameObject.GetComponent<MovingPlatform>();
            }
        }*/

        /*protected virtual void OnCollisionExit2D(Collision2D collision)
        {
            if (((1 << collision.gameObject.layer) & platformLayer.value) != 0)
            {
                onPlatform = false;
                platform = null;
                Debug.Log(name + " is on platform");
            }
        }*/

        protected virtual void HandleTrigger(Collider2D col)
        {
            if (((1 << col.gameObject.layer) & killLayers.value) != 0)
            {
                onCharacterDie.Invoke();
            }
        }
    }
}
