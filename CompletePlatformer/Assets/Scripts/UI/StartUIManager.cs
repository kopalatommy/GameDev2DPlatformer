using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using Platformer.Managers;

namespace Platformer.UI
{
    public class StartUIManager : MonoBehaviour
    {
        #region LandingPage
        [SerializeField] private Transform landingParent;
        [SerializeField] private Button startButton = null;
        [SerializeField] private Button quitButton = null;
        #endregion

        #region LevelSelector
        [SerializeField] private Transform levelSelectorParent;
        [SerializeField] private GameObject levelButtonPrefab;
        [SerializeField] private ScrollRect levels;
        [SerializeField] private Transform levelSelectorContent;
        [SerializeField] private Button levelSelectBackButton;
        #endregion

        private void Start()
        {
            startButton.onClick.AddListener(OnStartClicked);
            quitButton.onClick.AddListener(OnQuitClicked);

            levelSelectBackButton.onClick.AddListener(OnLevelSelectBackClicked);

            landingParent.gameObject.SetActive(true);
            levelSelectorParent.gameObject.SetActive(false);

            BuildLevelButtons();
        }

        private void BuildLevelButtons()
        {
            for (int i = 0; i < LevelManager.numLevels; i++)
            {
                LevelButton levelB = Instantiate(levelButtonPrefab, levelSelectorContent).GetComponent<LevelButton>();
                levelB.Populate("Level_Image", "Level " + (i + 1), OnLevelButtonClicked, i + 1);
            }
        }

        public void OnStartClicked()
        {
            landingParent.gameObject.SetActive(false);
            levelSelectorParent.gameObject.SetActive(true);
        }

        public void OnQuitClicked()
        {
            //Perform a normal exit
            Application.Quit(0);
        }

        public void OnLevelButtonClicked(int sceneIndex)
        {
            //Must add to build settings
            //File -> Build Settings -> Add open scenes
            SceneManager.LoadScene(sceneIndex);
        }

        public void OnLevelSelectBackClicked()
        {
            landingParent.gameObject.SetActive(true);
            levelSelectorParent.gameObject.SetActive(false);
        }
    }
}
