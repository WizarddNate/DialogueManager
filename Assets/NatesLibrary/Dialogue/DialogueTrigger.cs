using UnityEngine;
using System.IO;
using JetBrains.Annotations;
using NatesLibrary.Dialogue;
using System.Linq.Expressions;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    DialogueManager dm;
    TextArchitect architect;

    //temporary array of strings of text to test system
    string[] lines = new string[3]
    {
        "This is a line of test dialogue.",
        "If you are reading this, than everything is a success!",
        "At least I hope it is!"
    };



    private void Start()
    {
        dm = DialogueManager.instance;
        architect = new TextArchitect(dm.dialogueContainer.dialogueText);

        //set the text build method to typewriter
        architect.buildMethod = TextArchitect.BuildMethod.typewriter;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(architect.isBuilding)
            {
                if (!architect.textSpeedIncrease)
                    //speed up text
                    architect.textSpeedIncrease = true;
                else
                    //force complete text
                    architect.ForceComplete();
            }
            else
                architect.Build(lines[0]);
        }
    }
    /*public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(dialogue);
        
    }
    */
}
