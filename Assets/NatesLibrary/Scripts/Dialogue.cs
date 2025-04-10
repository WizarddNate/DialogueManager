using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class Dialogue
{
    //name of person talking
    public string name;

    //the minimum and maximum area of the text box in the inspector menu
    [TextArea(3, 10)]

    //the sentences displayed in the dialogue
    public string[] sentences;

}
