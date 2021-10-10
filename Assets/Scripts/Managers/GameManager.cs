using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Managers
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            Application.targetFrameRate = 100;
        }
    }
}
