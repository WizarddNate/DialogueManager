using UnityEngine;
using NatesLibrary.Dialogue;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class TestParser : MonoBehaviour
{
    //TEST ONLY SCRIPT. DELETE LATER !!!

    [SerializeField] private TextAsset file;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SendFileToParse();
    }

    // Update is called once per frame
    void SendFileToParse()
    {
        List<string> lines = TextFileManager.ReadTextAsset(file, false);

        foreach (string line in lines)
        {
            DialogueLine dl = DialogueParser.Parse(line);
        }
    }
}
