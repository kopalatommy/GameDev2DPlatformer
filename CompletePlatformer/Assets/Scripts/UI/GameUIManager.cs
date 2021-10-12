using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Platformer.Character;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Platformer.Managers;
using Platformer.Character.Player;

namespace Platformer.UI
{
    public class GameUIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text levelText = null;

        [SerializeField] private Image[] lifeImages = null;
        [SerializeField] private PlayerController player = null;

        [SerializeField] private GameObject failedUIParent = null;
        [SerializeField] private Button failedExitButton = null;
        [SerializeField] private Button failedRestartButton = null;

        [SerializeField] private GameObject succeededUIParent = null;
        [SerializeField] private Button succeededExitButton = null;
        [SerializeField] private Button succeededContinueButton = null;
        [SerializeField] private TMP_Text succeedPointsText = null;

        [SerializeField] private LevelManager levelManager;

        [SerializeField] private TMP_Text timerText;

        [SerializeField] private TMP_Text pointsText;

        public Button ContinueButton { get { return succeededContinueButton; } }

        private void Awake()
        {
            player.onCharacterDie.AddListener(OnPlayerDie);
            player.onPlayerSucceed.AddListener(OnPlayerSucceed);
            player.onPlayerFail.AddListener(OnPlayerFail);
            player.onPlayerScorePoints.AddListener(OnPlayerScorePoints);
            UpdateUI();

            failedUIParent.SetActive(false);
            failedExitButton.onClick.AddListener(ExitLevel);
            failedRestartButton.onClick.AddListener(RestartLevel);

            succeededUIParent.SetActive(false);
            succeededContinueButton.onClick.AddListener(OnPlayerSucceed);
            succeededExitButton.onClick.AddListener(ExitLevel);
        }

        private void Update()
        {
            int t = (int)levelManager.ElapsedTime;
            int mins = t / 60;
            int secs = t % 60;
            timerText.SetText($"{mins}:{secs}");
        }

        private void UpdateUI()
        {
            levelText.SetText("Level " + levelManager.CurrentLevel);

            int i;
            for (i = 0; i < player.RemainingLives; i++)
            {
                lifeImages[i].gameObject.SetActive(true);
            }
            for (; i < lifeImages.Length; i++)
            {
                lifeImages[i].gameObject.SetActive(false);
            }
        }

        public void OnPlayerDie()
        {
            UpdateUI();
        }

        public void OnPlayerFail()
        {
            failedUIParent.SetActive(true);
        }

        private void OnPlayerSucceed()
        {
            succeededUIParent.SetActive(true);

            levelManager.OnPlayerSucceed();
            succeedPointsText.SetText(player.CurrentPoints.ToString());
            succeededContinueButton.gameObject.SetActive(levelManager.CurrentLevel < levelManager.NumLevels);
        }

        private void OnPlayerScorePoints(int newScore)
        {
            pointsText.SetText(newScore.ToString());
        }

        private void RestartLevel()
        {
            SceneManager.LoadScene(levelManager.CurrentLevel);
        }

        private void ExitLevel()
        {
            SceneManager.LoadScene(0);
        }
    }
}