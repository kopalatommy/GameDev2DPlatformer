using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Managers
{
    //Basically used to set a target framereate
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            //Have this persist between scenes
            /*DontDestroyOnLoad(gameObject);
            //Limit to 100 fps
            Application.targetFrameRate = 100;
            //Initialize the score manager
            ScoreManager i = ScoreManager.Instance;*/
        }
    }
}
