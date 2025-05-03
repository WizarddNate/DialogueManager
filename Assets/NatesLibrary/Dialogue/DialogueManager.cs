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
        private ConversationManager conversationManager;
        private TextArchitect architect;

        public static DialogueManager instance { get; private set; }

        public delegate void DialogueManagerEvent();
        public event DialogueManagerEvent onUserPrompt_Next;

        public bool isRunningConversation => conversationManager.isRunning;

        public bool isFinished = false;

        private void Awake()
        {
            //makes sure that theres only one singleton in the scene
            if (instance == null)
            {
                instance = this;
                Initialize();
            }
            else
                DestroyImmediate(gameObject);

        }

        bool _initialized = false;
        private void Initialize()
        {
            if (_initialized)
                return;

            architect = new TextArchitect(dialogueContainer.dialogueText);
            conversationManager = new ConversationManager(architect);
        }

        public void OnUserPrompt_Next()
        {
            onUserPrompt_Next?.Invoke();
        }

        public void ShowSpeakerName(string speakerName = "")
        {
            //jankiest possible way to close out dialogue. Im sorry Eric :pensive emoji:
            if (speakerName.ToLower() == "end")
            {
                conversationManager.StopConversation();
                isFinished = true;
    }
            //hides the name narrartor's name tag.
            else if (speakerName.ToLower() != "narrator")
            {
                dialogueContainer.nameContainter.Show(speakerName);
            }
            else
            {
                HideSpeakerName();
            }
        }

        public void HideSpeakerName() => dialogueContainer.nameContainter.Hide();

        public void Say(string speaker, string dialogue)
        {
            List<string> conversation = new List<string>() { $"{speaker} \"{dialogue}\"" };
            Say(conversation);
        }

        public void Say(List<string> conversation)
        {
            conversationManager.StartConversation(conversation);
        }
    }
}
