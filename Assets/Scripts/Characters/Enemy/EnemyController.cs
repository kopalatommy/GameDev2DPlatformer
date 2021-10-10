using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Character.Enemy
{
    public class EnemyController : KinematicCharacter
    {
        protected override void SetUpCharacter()
        {
            base.SetUpCharacter();

            onCharacterDie.AddListener(OnDie);
        }

        protected virtual void Update()
        {
            
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
