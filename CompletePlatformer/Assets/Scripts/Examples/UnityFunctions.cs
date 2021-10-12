using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Examples
{
    public class UnityFunctions : MonoBehaviour
    {
        /*
         There are 3 main functions that most scrits will use:
        Awake, Start, Update
         */

        //This is the first built in function called. It is called when the object is loaded
        private void Awake()
        {
            Debug.Log("Awake");
        }

        //Called after awake and OnEnable. Whenever the object needs to start working
        private void Start()
        {
            Debug.Log("Start");
        }

        //Called every frame
        private void Update()
        {
            Debug.Log("Update");
        }

        //Called after the update loop. Triggered every frame
        //Generally used with animation stuff
        private void LateUpdate()
        {
            Debug.Log("Late update");
        }

        //Called every x seconds independent of the update loop. Generally used for physics stuff
        //The interval can be set under Edit -> Settings -> Time -> FixedTimeStep
        private void FixedUpdate()
        {
            Debug.Log("Fixed update");
        }

        //Triggered when the attached gameobject is enabled
        private void OnEnable()
        {
            Debug.Log("OnEnable");
        }

        //Triggered when the attached gameobject is disnabled
        private void OnDisable()
        {
            Debug.Log("OnDisable");
        }

        //Called when the object is being destroyed. Use to clean up or do final actions
        private void OnDestroy()
        {
            Debug.Log("OnDestroy");
        }
    }
}
