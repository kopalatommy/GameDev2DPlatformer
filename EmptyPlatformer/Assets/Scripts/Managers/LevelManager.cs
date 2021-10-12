using Platformer.Characters.Enemy;
using Platformer.Characters.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.Managers
{
    //There is a level manager for each level. It holds important information
    //about the level. Also handles Enemies
    public class LevelManager : MonoBehaviour
    {
        [Tooltip("The UI manager for the level")]
        [SerializeField] private GameUIManager uiManager = null;

        [Tooltip("Where should the player spawn")]
        [SerializeField] private Transform playerSpawnLoc;
        [Tooltip("Reference to the player")]
        [SerializeField] private PlayerController player;

        [Tooltip("The level's index. Used for scoring and moving in levels")]
        [SerializeField] private int currentLevel = 1;
        //Total number of levels in the game
        public const int numLevels = 6;

        [Tooltip("Elapsed time the player must beat to get the time bonus")]
        [SerializeField] private int parTimeSecs = 15;
        [Tooltip("The number of bonus points the player will receive for beating the par time")]
        [SerializeField] private int parTimeBonus = 1000;
        //Time counter
        private float elapsedTime = 0;

        [Tooltip("A list of all enemies in the level")]
        [SerializeField] private List<EnemyController> enemies = new List<EnemyController>();
        [Tooltip("A list of spawn locations for the enemies")]
        [SerializeField] private List<Transform> enemySpawnLocs = new List<Transform>();

        bool playerHasFinished = false;

        public int ParTimeSecs { get { return parTimeSecs; } }
        public int ParTimeBonus { get { return parTimeBonus; } }
        public float ElapsedTime { get { return elapsedTime; } }
        public int CurrentLevel { get { return currentLevel; } }
        public int NumLevels { get { return numLevels; } }

        private void Awake()
        {
            //Move player to start location
            /*player.transform.position = playerSpawnLoc.position;
            //Attach OnPlayerDie function to player OnCharacterDie Event
            player.onCharacterDie.AddListener(OnPlayerDie);

            uiManager.ContinueButton.onClick.AddListener(MoveToNextScene);
            
            //Set all enemies to initial locations
            ResetEnemies();*/
        }

        private void Update()
        {
            /*if(!playerHasFinished)
                elapsedTime += Time.deltaTime;*/
        }

        //Triggered when the player sucessfuly finishes a level
        public void OnPlayerSucceed()
        {
            /*playerHasFinished = true;

            int points = player.CurrentPoints;

            //Check if player beat the par time
            if (parTimeSecs > elapsedTime)
            {
                points += ParTimeBonus;
            }
            //Add 100 points for each remaining life
            points += (player.RemainingLives) * 100;
            player.CurrentPoints = points;
            //Update the high score file
            ScoreManager.Instance.SetHighScore(currentLevel, points);*/
        }

        private void OnPlayerDie()
        {
            //Move character back to start location
            /*player.transform.position = playerSpawnLoc.position;
            ResetEnemies();*/
        }

        //Moves all enemies to start locations and makes sure they are enabled/active
        private void ResetEnemies()
        {
            //Requires that each enemy has a spawn loc
            /*if (enemySpawnLocs.Count != enemies.Count)
            {
                Debug.LogError("Unable to reset enemeis. The number of enemies does not match their spawn locs");
            }
            else
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].transform.position = enemySpawnLocs[i].position;
                    enemies[i].transform.rotation = enemySpawnLocs[i].rotation;
                    enemies[i].gameObject.SetActive(true);
                }
            }*/
        }

        private void MoveToNextScene()
        {
            //Scene 0 is the landing scene, so add 1 to scene index to match global index
            //SceneManager.LoadScene(currentLevel + 1);
        }
    }
}
