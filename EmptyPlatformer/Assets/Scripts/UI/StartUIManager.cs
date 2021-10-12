using Platformer.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Platformer.UI
{
    //This class manages the UI elements in the landing scene
    public class StartUIManager : MonoBehaviour
    {
        #region LandingPage
        [Tooltip("The parent object of the landing page")]
        [SerializeField] private Transform landingParent;
        [Tooltip("The start button on the starting page")]
        [SerializeField] private Button startButton = null;
        [Tooltip("The quit button on the landing page")]
        [SerializeField] private Button quitButton = null;
        #endregion

        #region LevelSelector
        [Tooltip("The parent object of the level selector page")]
        [SerializeField] private Transform levelSelectorParent;
        [Tooltip("Reference to the level button prefab")]
        [SerializeField] private GameObject levelButtonPrefab;
        [Tooltip("Reference to the levels scroll rect")]
        [SerializeField] private ScrollRect levels;
        [Tooltip("Refernce to the level selector content parent")]
        [SerializeField] private Transform levelSelectorContent;
        [Tooltip("The back button attached to the level selector pages")]
        [SerializeField] private Button levelSelectBackButton;
        #endregion

        private void Start()
        {
            /*startButton.onClick.AddListener(OnStartClicked);
            quitButton.onClick.AddListener(OnQuitClicked);

            levelSelectBackButton.onClick.AddListener(OnLevelSelectBackClicked);

            landingParent.gameObject.SetActive(true);
            levelSelectorParent.gameObject.SetActive(false);

            BuildLevelButtons();*/
        }

        private void BuildLevelButtons()
        {
            /*for (int i = 0; i < LevelManager.numLevels; i++)
            {
                LevelButton levelB = Instantiate(levelButtonPrefab, levelSelectorContent).GetComponent<LevelButton>();
                levelB.Populate("Level_Image", "Level " + (i + 1), OnLevelButtonClicked, i + 1);
            }*/
        }

        public void OnStartClicked()
        {
            /*landingParent.gameObject.SetActive(false);
            levelSelectorParent.gameObject.SetActive(true);*/
        }

        public void OnQuitClicked()
        {
            //Perform a normal exit
            //Application.Quit(0);
        }

        public void OnLevelButtonClicked(int sceneIndex)
        {
            //Must add to build settings
            //File -> Build Settings -> Add open scenes
            //SceneManager.LoadScene(sceneIndex);
        }

        public void OnLevelSelectBackClicked()
        {
            /*landingParent.gameObject.SetActive(true);
            levelSelectorParent.gameObject.SetActive(false);*/
        }
    }
}
