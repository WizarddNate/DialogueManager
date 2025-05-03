using UnityEngine;
using System.IO;
using NatesLibrary.Dialogue;
using System.Collections;
using System.Collections.Generic;

public class DialogueTrigger : MonoBehaviour
{
    //text file
    [SerializeField] private TextAsset file;

    //access dialogue within the scene
    [SerializeField] GameObject DialoguePrefab;

    //Access player inputs (Important for enabling and disabling movement while the dialogue is running
    public PlayerInputManager pIM;

    //dialogue trigger
    public Collider2D col2D;

    private void Update()
    {
        //end dialogue once the finished variable is triggered true
        if (DialogueManager.instance.isFinished == true)
        {
            EndConversation();
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
            pIM.DisablePlayerMovement();
            StartConversation();
    }

    void StartConversation()
    {
        DialoguePrefab.SetActive(true);

        List<string> lines = TextFileManager.ReadTextAsset(file, false);

        DialogueManager.instance.Say(lines);

    }

    void EndConversation()
    {
        //close dialogue box and reset finish trigger
        pIM.EnablePlayerMovement();

        DialoguePrefab.SetActive(false);

        DialogueManager.instance.isFinished = false;

        //make sure that the textbox doesnt reenable
        col2D.enabled = false;
    }
}
