using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Examples
{
    /*
     GameObjects are a core part of the Unity Engine. They are used for just about everything.
     Most scripts that you write will likely be attached to a gameobject
     */

    public class GameObjectExamples : MonoBehaviour
    {
        //Unlike most other objects in C#, you cannot use the 'new' keyword to make new objects.
        //This is accomplished by passing a prefab to the Instantiate function
        [SerializeField] private GameObject prefab = null;
        private GameObject obj = null;

        private void Awake()
        {
            //A useful tool for sorting through gameobjects is by using the object's instance ID
            //Any prefab, an object not in the world, will return a negative ID. 
            //Any object in the map will have a unique positive ID
            Debug.Log("Prefab instance ID: " + prefab.GetInstanceID());

            //Creates a new instance of the prefab
            obj = Instantiate(prefab);
            //Creates a new instance of the prefab at the specific location with the specified rotation
            obj = Instantiate(prefab, Vector3.one, Quaternion.identity);
            //Creates a new instance of the prefab at the specific location with the specified rotation as a child to the parent
            obj = Instantiate(prefab, Vector3.one, Quaternion.identity, this.transform);
        }

        private void Start()
        {
            //The gameobjct this script is attached to
            Debug.Log(this.gameObject);
            //The transform this script is attached to
            Debug.Log(this.transform);
            //Is the gameobject enabled in the heirarchy
            Debug.Log(this.enabled);


            //The getcomponent function is used to retreive scripts/components attached to a gameobject.
            //This is an expensive process, so limit calls to this
            Transform t = GetComponent<Transform>();

            Transform[] lst = GetComponents<Transform>();

            t = GetComponentInChildren<Transform>();

            lst = GetComponentsInChildren<Transform>();

            t = GetComponentInParent<Transform>();

            lst = GetComponentsInParent<Transform>();

            bool found = TryGetComponent<Transform>(out t);
        }
    }
}
