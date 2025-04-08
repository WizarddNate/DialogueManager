using UnityEngine;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    string[] dialogueArray;

    string filePath, fileName;

    void Start()
    {
        fileName = "SampleText";
        filePath = Application.dataPath + "/TextFiles/" + fileName + ".txt";

        ReadFromFile();
    }

    public void ReadFromFile()
    {
        //read text file line by line
        dialogueArray = File.ReadAllLines(filePath);
        foreach(string line in dialogueArray)
        {
            Debug.Log(line);
        }
    }

}
