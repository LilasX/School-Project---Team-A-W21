using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogues
{
    public Image character;
    public Image background;

    [TextArea(3,10)]
    public string[] sentences;
}

//Reference
//https://youtu.be/_nRzoTzeyxU