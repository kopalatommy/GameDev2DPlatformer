using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using Platformer.Managers;

namespace Platformer.UI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text pointsText;
        [SerializeField] private RawImage levelImage;
        [SerializeField] private Image img;
        [SerializeField] private Button button;

        private UnityAction<int> clickAction;
        private int sceneIndex;

        public void Populate(string imgName, string levelName, UnityAction<int> clickAction, int sceneIndex)
        {
            levelImage.texture = Resources.Load<Texture2D>(imgName);
            levelText.SetText(levelName);
            pointsText.SetText(ScoreManager.Instance.GetHighScore(sceneIndex).ToString());

            this.clickAction = clickAction;
            this.sceneIndex = sceneIndex;

            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            clickAction.Invoke(sceneIndex);
        }
    }
}
