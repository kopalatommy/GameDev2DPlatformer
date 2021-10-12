using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer.Characters.Players
{
    //The player controller class is a child class of the kinematic character class
    //Allows the player to control the character
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
            /*base.Awake();

            if (onPlayerFail == null)
                onPlayerFail = new UnityEvent();
            if (onPlayerSucceed == null)
                onPlayerSucceed = new UnityEvent();
            if (onPlayerScorePoints == null)
                onPlayerScorePoints = new UnityEvent<int>();

            onCharacterDie.AddListener(OnPlayerDie);

            animator.SetBool("InAIr", false);
            animator.SetFloat("Speed", 0);*/
        }

        protected override void Update()
        {
            //Move based on the player input
            /*Move(Input.GetAxis("Horizontal"), Input.GetButtonDown("Jump"));

            //Update the animator values
            if (animator.GetBool("InAir") != isGrounded)
            {
                animator.SetBool("InAIr", isGrounded);
            }
            animator.SetFloat("Speed", _rigidbody.velocity.magnitude);

            base.Update();*/
        }

        //When the character dies, the player should lose a life
        //Attaches the onCharacterDie event
        private void OnPlayerDie()
        {
            /*Debug.Log("Player died");
            remainingLives--;

            if (remainingLives == 0)
            {
                canMove = false;
                Debug.Log("Player has failed level");
                onPlayerFail.Invoke();
            }*/
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            /*int layerValue = 1 << collision.gameObject.layer;

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
            base.OnTriggerEnter2D(collision);*/
        }
    }
}
