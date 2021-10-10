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

        [SerializeField] private LevelManager levelManager;

        public Button ContinueButton { get { return succeededContinueButton; } }

        private void Awake()
        {
            player.onCharacterDie.AddListener(OnPlayerDie);
            player.onPlayerSucceed.AddListener(OnPlayerSucceed);
            player.onPlayerFail.AddListener(OnPlayerFail);
            UpdateUI();

            failedUIParent.SetActive(false);
            failedExitButton.onClick.AddListener(ExitLevel);
            failedRestartButton.onClick.AddListener(RestartLevel);

            succeededUIParent.SetActive(false);
            succeededContinueButton.onClick.AddListener(OnPlayerSucceed);
            succeededExitButton.onClick.AddListener(ExitLevel);
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