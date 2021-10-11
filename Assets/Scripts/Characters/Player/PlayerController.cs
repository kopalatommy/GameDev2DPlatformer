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
        [SerializeField] private Camera playerCamera = null;

        [SerializeField] private int maxLives = 3;
        [SerializeField] private int remainingLives = 3;

        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private LayerMask collectableLayer;

        private int currentPoints = 0;

        public int RemainingLives { get { return remainingLives; } }
        public int CurrentPoints { get { return currentPoints; } set { currentPoints = value; 
                                                                        onPlayerScorePoints.Invoke(currentPoints); } }

        public UnityEvent onPlayerSucceed;
        public UnityEvent onPlayerFail;
        //Uses total points as arg
        public UnityEvent<int> onPlayerScorePoints;

        private new void Awake()
        {
            if (onPlayerFail == null)
                onPlayerFail = new UnityEvent();
            if (onPlayerSucceed == null)
                onPlayerSucceed = new UnityEvent();
            if (onPlayerScorePoints == null)
                onPlayerScorePoints = new UnityEvent<int>();

            base.Awake();
            onCharacterDie.AddListener(OnPlayerDie);
        }

        private void Update()
        {
            OnUpdate();
        }

        protected new void OnUpdate()
        {
            base.OnUpdate();
            //Debug.Log(Input.GetAxis("Horizontal"));
            Move(Input.GetAxis("Horizontal"), Input.GetButtonDown("Jump"));
        }

        private void OnPlayerDie()
        {
            Debug.Log("Player died");
            remainingLives--;

            if(remainingLives == 0)
            {
                canMove = false;
                Debug.Log("Player has failed level");
                onPlayerFail.Invoke();
            }
        }

        protected override void HandleTrigger(Collider2D col)
        {
            base.HandleTrigger(col);

            int layer = 1 << col.gameObject.layer;

            if((layer & collectableLayer.value) != 0)
            {
                currentPoints += col.gameObject.GetComponent<PointsCollectable>().PointsValue;
                onPlayerScorePoints.Invoke(currentPoints);
                col.gameObject.SetActive(false);
            }
            else if (col.tag == "Goal")
            {
                canMove = false;
                _rigidbody.velocity = Vector2.zero;
                _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
                Debug.Log("Player reached goal");
                onPlayerSucceed.Invoke();
            }
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
            if (((1 << collision.gameObject.layer) & enemyLayer.value) != 0)
            {
                //Debug.Log(collision.contacts[0].point.y + " <= " + (groundCheckPosition.position.ToVector2_XY().y - 0.02));
                //Debug.Log(groundCheckPosition.position.ToVector2_XY().y - collision.contacts[0].point.y + 0.02);
                if(groundCheckPosition.position.y - collision.contacts[0].point.y + groundCheckRadius > 0)
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

                return;

                if(GetAngle(transform.position, collision.contacts[0].point) < 90)
                {
                    Debug.Log("Player killed enemy");
                    Debug.Log(GetAngle(transform.position, collision.contacts[0].point));
                }
                else
                {
                    Debug.Log("Player got killed by enemy");
                    onCharacterDie.Invoke();
                }
            }
        }
    }
}
