using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Character.Enemy
{
    public class EnemyController : KinematicCharacter
    {
        [Tooltip("The number of points earned if killed by character")]
        [SerializeField] private int killPoints = 250;

        public int KillPoints { get { return killPoints; } }

        protected override void Awake()
        {
            base.Awake();
            onCharacterDie.AddListener(OnDie);
        }

        protected override void Update()
        {
            base.Update();
            Move(-1, false);
        }

        public void Kill()
        {
            onCharacterDie.Invoke();
        }

        private void OnDie()
        {
            gameObject.SetActive(false);
        }
    }
}
