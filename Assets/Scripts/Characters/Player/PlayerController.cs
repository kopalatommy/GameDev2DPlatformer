using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Platformer;
using Platformer.Character.Enemy;

namespace Platformer.Character.Player
{
    public class PlayerController : KinematicCharacter
    {
        [SerializeField] private Camera playerCamera = null;

        [SerializeField] private int maxLives = 3;
        [SerializeField] private int remainingLives = 3;

        [SerializeField] private LayerMask enemyLayer;

        public int RemainingLives { get { return remainingLives; } }

        public UnityEvent onPlayerSucceed;
        public UnityEvent onPlayerFail;

        private new void Awake()
        {
            if (onPlayerFail == null)
                onPlayerFail = new UnityEvent();
            if (onPlayerSucceed == null)
                onPlayerSucceed = new UnityEvent();

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
                Debug.Log("Player has failed level");
                onPlayerFail.Invoke();
            }
        }

        protected override void HandleTrigger(Collider2D col)
        {
            base.HandleTrigger(col);

            if (col.tag == "Goal")
            {
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //Debug.Log("Player collision: " + collision.gameObject.name);
            if (((1 << collision.gameObject.layer) & enemyLayer.value) != 0)
            {
                //Debug.Log(collision.contacts[0].point.y + " <= " + (groundCheckPosition.position.ToVector2_XY().y - 0.02));
                //Debug.Log(groundCheckPosition.position.ToVector2_XY().y - collision.contacts[0].point.y + 0.02);
                if(groundCheckPosition.position.ToVector2_XY().y - collision.contacts[0].point.y + 0.02 > 0)
                {
                    Debug.Log("Player killed enemy");
                    //Debug.Log(GetAngle(transform.position, collision.contacts[0].point));
                    collision.gameObject.GetComponent<EnemyController>().Kill();
                }
                else
                {
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
