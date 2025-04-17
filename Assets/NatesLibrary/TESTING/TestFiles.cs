using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NatesLibrary.Dialogue;


public class TestFiles : MonoBehaviour
{
    [SerializeField] private TextAsset fileName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        List<string> lines = TextFileManager.ReadTextAsset(fileName, false);

        foreach (string line in lines)
            Debug.Log(line);

        yield return null;
    }
}
