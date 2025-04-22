using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NatesLibrary.Dialogue
{
    public class ConversationManager 
    {
        private DialogueManager dialogueManager => DialogueManager.instance;

        private Coroutine process = null;
        public bool isRunning => process != null;

        private TextArchitect architect = null;
        private bool userPrompt = false;
        public ConversationManager(TextArchitect architect)
        {
            this.architect = architect;
            dialogueManager.onUserPrompt_Next += OnUserPrompt_Next;
        }
        
        private void OnUserPrompt_Next()
        {
            userPrompt = true;
        }

        public void StartConversation(List<string> conversation)
        {
            StopConversation();

            process = dialogueManager.StartCoroutine(RunningConversation(conversation));
        }

        public void StopConversation()
        {
            if (!isRunning)
                return;

            dialogueManager.StopCoroutine(process);
            process = null;
        }

        IEnumerator RunningConversation(List<string> conversation)
        {
            for(int i = 0; i < conversation.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(conversation[i])) 
                    continue;

                //get full dialogue line and then parse it
                DialogueLine line = DialogueParser.Parse(conversation[i]);

                //run dialogue
                if (line.hasDialogue)
                    yield return Line_RunDialogue(line);

                //run any line commands
                if (line.hasCommands)
                    yield return Line_RunCommands(line);

            }
        }
        IEnumerator Line_RunDialogue(DialogueLine line)
        {
            //Show or hide the speaker name
            if (line.hasSpeaker)
                dialogueManager.ShowSpeakerName(line.speaker);
            else
                dialogueManager.HideSpeakerName();

            //Build dialogue
            yield return BuildDialogue(line.dialogue);

            //wait for user input
            yield return WaitForUserInput();
        }

        IEnumerator Line_RunCommands(DialogueLine line)
        {
            Debug.Log(line.commands);
            yield return null;
        }

        IEnumerator BuildDialogue(string dialogue)
        {
            architect.Build(dialogue);

            while (architect.isBuilding)
            {
                if (userPrompt)
                {
                    if (!architect.textSpeedIncrease)
                        architect.textSpeedIncrease = true;
                    else
                        architect.ForceComplete();

                    userPrompt = false;
                }
                yield return null;
            }
        }

        IEnumerator WaitForUserInput()
        {
            while(!userPrompt)
                yield return null;
            userPrompt = false;
        }

    }
}

