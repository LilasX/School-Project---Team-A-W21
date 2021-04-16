using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguesManager : MonoBehaviour
{
    public Text dialoguesText; //the dialogues 
    public Image characterImage; //character png
    public Image backgroundImage; //text space for dialogues
    public Button continueButton; //to move to the next sentence or close UI dialogue

    public static DialoguesManager instance = null; //Singleton;

    private Queue<string> sentences; //make a queue for dialogues

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>(); //make a new queue for dialogues (FIFO)
        
        //Inactive when starting game
        characterImage.enabled = false;
        backgroundImage.enabled = false;
        dialoguesText.enabled = false;
        continueButton.gameObject.SetActive(false);
    }

    public void StartDialogues(Dialogues dialogues) //Activate dialogues UI
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //Active when meeting the conditions or calling the method to trigger dialogues
        characterImage.enabled = true;
        backgroundImage.enabled = true;
        dialoguesText.enabled = true;
        continueButton.gameObject.SetActive(true);

        //get the components
        characterImage = dialogues.character;
        backgroundImage = dialogues.background;

        sentences.Clear(); 

        //Play all sentences in the queue
        foreach(string sentence in dialogues.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence(); //display the next sentences with continue button
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue(); //call this method when there are no more sentences to display
            return;
        }

        //display the sentences in the order put in the queue
        string sentence = sentences.Dequeue();
        dialoguesText.text = sentence;
    }

    void EndDialogue()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //deactivate dialogues UI
        characterImage.enabled = false;
        backgroundImage.enabled = false;
        dialoguesText.enabled = false;
        continueButton.gameObject.SetActive(false);
    }
}

//Reference
//https://youtu.be/_nRzoTzeyxU