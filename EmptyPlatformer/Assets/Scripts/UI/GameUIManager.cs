using Platformer.Characters.Players;
using Platformer.Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Platformer.UI
{
    //This class manages the in game UI. 
    public class GameUIManager : MonoBehaviour
    {
        #region GeneralElements
        [Tooltip("The text that displays the level name")]
        [SerializeField] private TMP_Text levelText = null;
        [Tooltip("An array of images used to display the remaining player lives")]
        [SerializeField] private Image[] lifeImages = null;
        [Tooltip("The text field used to display the elapsed time")]
        [SerializeField] private TMP_Text timerText;
        [Tooltip("The text field to display the midgame score")]
        [SerializeField] private TMP_Text pointsText;
        #endregion

        #region FailedPage
        [Tooltip("The parent of the failedUI page")]
        [SerializeField] private GameObject failedUIParent = null;
        [Tooltip("The exit button attached to the failed page")]
        [SerializeField] private Button failedExitButton = null;
        [Tooltip("The restart button attached to the failed page")]
        [SerializeField] private Button failedRestartButton = null;
        #endregion

        #region SuccessPage
        [Tooltip("The parent of the success page")]
        [SerializeField] private GameObject succeessUIParent = null;
        [Tooltip("The exit button attached to the success page")]
        [SerializeField] private Button succeessExitButton = null;
        [Tooltip("The continue button attached to the success page")] 
        [SerializeField] private Button successContinueButton = null;
        [Tooltip("The text field attached to the success page that shows the final score")]
        [SerializeField] private TMP_Text successPointsText = null;
        #endregion

        [Tooltip("Reference to the player character")]
        [SerializeField] private PlayerController player = null;
        [Tooltip("Reference to the level manager")]
        [SerializeField] private LevelManager levelManager;

        private void Awake()
        {
            /*player.onCharacterDie.AddListener(OnPlayerDie);
            player.onPlayerSucceed.AddListener(OnPlayerSucceed);
            player.onPlayerFail.AddListener(OnPlayerFail);
            player.onPlayerScorePoints.AddListener(OnPlayerScorePoints);
            UpdateUI();

            failedUIParent.SetActive(false);
            failedExitButton.onClick.AddListener(ExitLevel);
            failedRestartButton.onClick.AddListener(RestartLevel);

            succeessUIParent.SetActive(false);
            successContinueButton.onClick.AddListener(OnPlayerSucceed);
            succeessExitButton.onClick.AddListener(ExitLevel);*/
        }

        private void Update()
        {
            /*int t = (int)levelManager.ElapsedTime;
            int mins = t / 60;
            int secs = t % 60;
            timerText.SetText($"{mins}:{secs}");*/
        }

        //Updates general UI elements
        private void UpdateUI()
        {
            /*levelText.SetText("Level " + levelManager.CurrentLevel);

            int i;
            for (i = 0; i < player.RemainingLives; i++)
            {
                lifeImages[i].gameObject.SetActive(true);
            }
            for (; i < lifeImages.Length; i++)
            {
                lifeImages[i].gameObject.SetActive(false);
            }*/
        }

        public void OnPlayerDie()
        {
            //UpdateUI();
        }

        public void OnPlayerFail()
        {
            //failedUIParent.SetActive(true);
        }

        private void OnPlayerSucceed()
        {
            /*succeessUIParent.SetActive(true);

            levelManager.OnPlayerSucceed();
            successPointsText.SetText(player.CurrentPoints.ToString());
            successContinueButton.gameObject.SetActive(levelManager.CurrentLevel < levelManager.NumLevels);*/
        }

        private void OnPlayerScorePoints(int newScore)
        {
            //pointsText.SetText(newScore.ToString());
        }

        private void RestartLevel()
        {
            //SceneManager.LoadScene(levelManager.CurrentLevel);
        }

        private void ExitLevel()
        {
            //SceneManager.LoadScene(0);
        }
    }
}
