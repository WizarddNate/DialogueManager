using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

namespace NatesLibrary.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        public DialogueContainer dialogueContainer = new DialogueContainer();

        public static DialogueManager instance;

        private void Awake()
        {
            //makes sure that theres only one singleton in the scene
            if (instance == null)
                instance = this;
            else
                DestroyImmediate(gameObject);
            return;
        }



    }
}
