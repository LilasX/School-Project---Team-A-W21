using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogues
{
    public Image character; //for the character png
    public Image background; //for the dialogues space

    [TextArea(3,10)] //min 3 lines and max 10 lines
    public string[] sentences; //an array of strings for the dialogues if necessary
}

//Reference
//https://youtu.be/_nRzoTzeyxU