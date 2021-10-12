using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Platformer.Examples
{
    //Require component is a unity specific class attribute that is used to make sure components this script is dependent on
    //is attached to the same gameobject. This only happens when this script is initially added to the object
    [RequireComponent(typeof(BoxCollider2D))]

    //Serializable is a general class attribute that forces the data fields to be layed out the same way among multiple instances.
    //This is generally used when saving classes
    [Serializable]

    //AddComponentMenu is used to create a shortcut for adding this script in the add component menu
    [AddComponentMenu("Examples/")]

    //DisallowMultipleComponent is used to limit one instance of this script per gameobject
    [DisallowMultipleComponent]
    public class AttributeExamples : MonoBehaviour
    {
        //SerializeField is an attribute that forces the Unity editor to serialize a field
        //By default, Unity only serializes public fields. This is useful for showing private variables in the inspector
        [SerializeField]
        private int serializedPrivate = 0;
        private int unserializedPrivate = 0;
        public int publicValue = 0;

        //The range attribute is used to limit the range of values that the inspector will set this field to
        [Range(0, 20)]
        public int range0to20 = 0;

        //The min attribute is used to set a minimum value the unity inspector will accept for this field
        [Min(10)]
        public int min10 = 5;

        //HideInInspector is used to prevent a field from being shown in the inspector
        [HideInInspector]
        public int hiddenVariable;

        [Tooltip("The tooltip attribute is used to add a tooltip to a field")]
        public int tooltip = 0;

        //This creates an expanded textbox that allows for more functionality when inputting a string
        [TextArea]
        public string textArea;
        public string defaultString;
    }
}