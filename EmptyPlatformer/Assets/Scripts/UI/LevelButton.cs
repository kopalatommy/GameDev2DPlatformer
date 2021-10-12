using Platformer.Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Platformer.UI
{
    //This class handles level buttons in the 
    public class LevelButton : MonoBehaviour
    {
        [Tooltip("The tect field that holds the level name")]
        [SerializeField] private TMP_Text levelText;
        [Tooltip("Text field that holds the max score")]
        [SerializeField] private TMP_Text pointsText;
        [Tooltip("Image field that holds the levels picture")]
        [SerializeField] private RawImage levelImage;
        [Tooltip("The button object this is attached to")]
        [SerializeField] private Button button;

        private UnityAction<int> clickAction;
        private int sceneIndex;

        //Populates the buttons UI elements based on the provided info
        public void Populate(string imgName, string levelName, UnityAction<int> clickAction, int sceneIndex)
        {
            /*levelImage.texture = Resources.Load<Texture2D>(imgName);
            levelText.SetText(levelName);
            pointsText.SetText(ScoreManager.Instance.GetHighScore(sceneIndex).ToString());

            this.clickAction = clickAction;
            this.sceneIndex = sceneIndex;

            button.onClick.AddListener(OnClick);*/
        }

        private void OnClick()
        {
            //clickAction.Invoke(sceneIndex);
        }
    }
}
