using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Examples
{
    //It is possbile to extend/(add functionality) to built in classes
    public static class ExtendingExample
    {
        //Any varaibles used must be static
        static List<GameObject> cachedGameobjects = new List<GameObject>();

        //Any extra function will take the argument (this GameObject obj). This referes to the gameobject this is called in
        static void CacheGameObject(this GameObject obj)
        {
            if(cachedGameobjects.Contains(obj))
            {
                Debug.Log("Already cached " + obj.name);
            }
            else
            {
                cachedGameobjects.Add(obj);
                Debug.Log("Cached " + obj.name);
            }
        }

        //You can add extra fields
        static void CacheGameObject(this GameObject obj, int val)
        {
            Debug.Log("Called CachedGameObject with a value of " + val);
            if (cachedGameobjects.Contains(obj))
            {
                Debug.Log("Already cached " + obj.name);
            }
            else
            {
                cachedGameobjects.Add(obj);
                Debug.Log("Cached " + obj.name);
            }
        }
    }
}
