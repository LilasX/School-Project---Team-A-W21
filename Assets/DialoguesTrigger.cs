using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguesTrigger : MonoBehaviour
{
    public Dialogues dialogues;
    private DialoguesManager managerd;

    // Start is called before the first frame update
    void Start()
    {
        managerd = DialoguesManager.instance;
    }

    public void TriggerDialogues()
    {
        managerd.StartDialogues(dialogues);
    }
}

//Reference
//https://youtu.be/_nRzoTzeyxU