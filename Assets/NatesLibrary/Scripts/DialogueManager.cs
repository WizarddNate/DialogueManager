using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private Queue<string> dialogueQueue; //YESSSS THE DATA STRUCTURES CLASS IS BEING PUT TO USE WOO WOOOOOOOOO

    string filePath, fileName;

    void Start()
    {
        dialogueQueue = new Queue<string>();
        fileName = "SampleText";
        filePath = Application.dataPath + "/TextFiles/" + fileName + ".txt";

        ReadFromFile();
    }


    public void StartDialogue (Dialogue dialogue)
    {
        Debug.Log("Starting conversation with " + dialogue.name);

        nameText.text = dialogue.name;

        dialogueQueue.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            dialogueQueue.Enqueue(sentence);
            DisplayNextLine();
        }
    }

    public void DisplayNextLine()
    {
        //check if queue is empty
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = dialogueQueue.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation.");
    }

    public void ReadFromFile()
    {
        //read text file line by line
        //dialogueArray = File.ReadAllLines(filePath);
        foreach(string line in dialogueQueue)
        {
            Debug.Log(line);
        }
    }

}
