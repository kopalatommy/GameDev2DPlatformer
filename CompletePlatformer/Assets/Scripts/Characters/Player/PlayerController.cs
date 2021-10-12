using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Platformer;
using Platformer.Character.Enemy;
using Platformer.World;

namespace Platformer.Character.Player
{
    public class PlayerController : KinematicCharacter
    {
        //The camera used by the player
        private Camera playerCamera = null;

        [Tooltip("The max number of lives the character has")]
        [SerializeField] private int maxLives = 3;
        private int remainingLives = 3;

        private int currentPoints = 0;

        [Tooltip("The layers that enemy characters reside in")]
        [SerializeField] private LayerMask enemyLayers;
        [Tooltip("The layers that collectables reside in")]
        [SerializeField] private LayerMask collectableLayers;

        [Tooltip("The animator attached to the character")]
        [SerializeField] private Animator animator = null;

        //Triggered when the character passes a level
        public UnityEvent onPlayerSucceed;
        //Triggered when a character runs out of lives
        public UnityEvent onPlayerFail;
        //Triggered when the player scores points. Calls with currentPoints
        public UnityEvent<int> onPlayerScorePoints;

        //Getter functions
        public int RemainingLives { get { return remainingLives; } }
        public int CurrentPoints
        {
            get { return currentPoints; }
            set
            {
                currentPoints = value;
                onPlayerScorePoints.Invoke(currentPoints);
            }
        }

        protected override void Awake()
        {
            base.Awake();

            if (onPlayerFail == null)
                onPlayerFail = new UnityEvent();
            if (onPlayerSucceed == null)
                onPlayerSucceed = new UnityEvent();
            if (onPlayerScorePoints == null)
                onPlayerScorePoints = new UnityEvent<int>();

            onCharacterDie.AddListener(OnPlayerDie);

            animator.SetBool("InAir", false);
            animator.SetFloat("Speed", 0);
        }

        protected override void Update()
        {
            //Move based on the player input
            Move(Input.GetAxis("Horizontal"), Input.GetButtonDown("Jump"));

            //Update the animator values
            /*if (animator.GetBool("InAir") != isGrounded)
            {
                animator.SetBool("InAir", isGrounded);
            }*/
            animator.SetFloat("Speed", Mathf.Abs(_rigidbody.velocity.x));

            base.Update();
        }

        //When the character dies, the player should lose a life
        //Attaches the onCharacterDie event
        private void OnPlayerDie()
        {
            Debug.Log("Player died");
            remainingLives--;

            if (remainingLives == 0)
            {
                canMove = false;
                Debug.Log("Player has failed level");
                onPlayerFail.Invoke();
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            int layerValue = 1 << collision.gameObject.layer;

            //Check if the trigger is a collectable
            if ((layerValue & collectableLayers.value) != 0)
            {
                currentPoints += collision.gameObject.GetComponent<PointsCollectable>().PointsValue;
                onPlayerScorePoints.Invoke(currentPoints);
                collision.gameObject.SetActive(false);
            }
            //Check if the trigger is the goal
            else if (collision.tag == "Goal")
            {
                canMove = false;
                _rigidbody.velocity = Vector2.zero;
                _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
                Debug.Log("Player reached goal");
                onPlayerSucceed.Invoke();
            }
            //Pass through to parent version
            base.OnTriggerEnter2D(collision);
        }

        private float GetAngle(Vector2 pointA, Vector2 pointB)
        {
            Vector2 target = pointB - pointA;
            float angle = Vector2.Angle(pointA, pointB);
            float orientation = Mathf.Sign(pointA.x * target.y - pointA.y * target.x);
            return (360 - orientation * angle) % 360;
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            //Debug.Log("Player collision: " + collision.gameObject.name);
            if (((1 << collision.gameObject.layer) & enemyLayers.value) != 0)
            {
                //Debug.Log(collision.contacts[0].point.y + " <= " + (groundCheckPosition.position.ToVector2_XY().y - 0.02));
                //Debug.Log(groundCheckPosition.position.ToVector2_XY().y - collision.contacts[0].point.y + 0.02);
                if (groundCheckPosition.position.y - collision.contacts[0].point.y + groundCheckRadius > 0)
                {
                    Debug.Log("Player killed enemy");
                    //Debug.Log(GetAngle(transform.position, collision.contacts[0].point));
                    collision.gameObject.GetComponent<EnemyController>().Kill();
                    currentPoints += collision.gameObject.GetComponent<EnemyController>().KillPoints;
                    onPlayerScorePoints.Invoke(currentPoints);
                }
                else
                {
                    Debug.Log(groundCheckPosition.position.y + " : " + collision.contacts[0].point.y);
                    Debug.Log("Player got killed by enemy");
                    onCharacterDie.Invoke();
                }
            }
        }
    }
}
