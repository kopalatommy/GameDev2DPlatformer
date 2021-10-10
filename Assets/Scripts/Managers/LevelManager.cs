using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Platformer.UI;
using Platformer.Character;
using UnityEngine.SceneManagement;
using Platformer.Character.Player;
using Platformer.Character.Enemy;

namespace Platformer.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameUIManager uiManager = null;

        [SerializeField] private Transform playerSpawnLoc;
        [SerializeField] private PlayerController player;

        [SerializeField] private int currentLevel = 1;

        public int CurrentLevel { get { return currentLevel; } }

        [SerializeField] private List<EnemyController> enemies = new List<EnemyController>();
        [SerializeField] private List<Transform> enemySpawnLocs = new List<Transform>();

        private void Awake()
        {
            player.transform.position = playerSpawnLoc.position;

            player.onCharacterDie.AddListener(OnPlayerDie);

            uiManager.ContinueButton.onClick.AddListener(MoveToNextScene);

            ResetEnemies();
        }

        private void OnPlayerDie()
        {
            player.transform.position = playerSpawnLoc.position;
            ResetEnemies();
        }

        private void ResetEnemies()
        {
            if(enemySpawnLocs.Count != enemies.Count)
            {
                Debug.LogError("Unable to reset enemeis. The number of enemies does not match their spawn locs");
            }
            else
            {
                for(int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].transform.position = enemySpawnLocs[i].position;
                    enemies[i].transform.rotation = enemySpawnLocs[i].rotation;
                    enemies[i].gameObject.SetActive(true);
                }
            }
        }

        private void MoveToNextScene()
        {
            SceneManager.LoadScene(currentLevel + 1);
        }
    }
}
