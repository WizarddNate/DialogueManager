using NatesLibrary.Dialogue;
using System.Collections.Generic;
using UnityEngine;

public class TestConvo : MonoBehaviour
{
    //TEST ONLY SCRIPT. DELETE LATER !!!

    [SerializeField] private TextAsset file;
    [SerializeField] GameObject DialoguePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartConversation();
    }

    // Update is called once per frame
    private void Update()
    {
        if (DialogueManager.instance.isFinished == true)
        {
            EndConversation();
        }
    }
    void StartConversation()
    {
        DialoguePrefab.SetActive(true);

        List<string> lines = TextFileManager.ReadTextAsset(file, false);

        DialogueManager.instance.Say(lines);

    }

    void EndConversation()
    {
        DialoguePrefab.SetActive(false);
    }
}