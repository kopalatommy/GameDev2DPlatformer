using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.Examples
{
    //Coroutines are a C# way of splitting up functions
    public class CoroutineExamples : MonoBehaviour
    {
        public bool stringCall = false;

        public bool blockingExample = false;
        public bool waitExample = false;
        public bool startConditionExample = false;
        public bool exampleCondition = false;

        public bool stepCorutine = false;

        public Image fadeImage;
        public bool stopFade = false;
        public bool startFade = false;
        Coroutine fadeExmaple = null;

        IEnumerator BasicCorutine()
        {
            Debug.Log("Started basic corutine");
            yield return null;
        }

        //Corutines can be used to run functions that would normally block the main thread.
        //The yield return null funtion returns from the function without losing the functions context.
        IEnumerator BlockingFunctionExample()
        {
            int counter = 0;
            while(true)
            {
                counter++;
                Debug.Log("Blocking function before: " + counter);
                yield return null;
                Debug.Log("Blocking function after: " + counter);
            }
        }

        //Corutines can also be used to put a non blocking wait command inside of the function
        IEnumerator WaitCorutine()
        {
            Debug.Log("Before delay: " + Time.time);
            yield return new WaitForSeconds(10);
            Debug.Log("After delay: " + Time.time);
        }

        IEnumerator WaitConditionExample()
        {
            Debug.Log("Before condition");
            yield return new WaitUntil(ExampleConditionFunction);
            Debug.Log("After condition");
        }

        private bool ExampleConditionFunction()
        {
            return exampleCondition;
        }

        //Here are all of the wait functions
        IEnumerator WaitFunctions()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForFixedUpdate();
            yield return new WaitForSeconds(10);
            yield return new WaitForSecondsRealtime(10);
            yield return new WaitUntil(ExampleConditionFunction);
            yield return new WaitWhile(ExampleConditionFunction);
        }

        private void Start()
        {
            if(stringCall)
            {
                StringCall();
            }
            else
            {
                DirectCall();
            }
            if(blockingExample)
                StartCoroutine(BlockingFunctionExample());
            if(waitExample)
                StartCoroutine(WaitCorutine());
            if (startConditionExample)
                StartCoroutine(WaitConditionExample());
        }

        //There are two ways to trigger a coroutine.
        //The first and best way is to pass the function to StartCoroutine directly
        private void DirectCall()
        {
            StartCoroutine(BasicCorutine());
        }

        //The other way is to pass a string of the function name to the StartCorutine function.
        //This is not recommended because string comparisons are notoriously slow
        private void StringCall()
        {
            StartCoroutine("BasicCorutine");
        }

        private void Update()
        {
            if(startFade)
            {
                startFade = false;
                fadeExmaple = StartCoroutine(FadeExample());
            }
            if(stopFade)
            {
                if(fadeExmaple != null)
                    StopCoroutine(fadeExmaple);
            }
        }

        IEnumerator FadeExample()
        {
            Color c = fadeImage.material.color;
            c.a = 1;
            fadeImage.material.color = c;
            for (float ft = 1f; ft >= 0; ft -= 0.1f)
            {
                c = fadeImage.material.color;
                c.a = ft;
                fadeImage.material.color = c;
                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
            Debug.Log("Finished");
            StopCoroutine(fadeExmaple);
        }
    }
}
