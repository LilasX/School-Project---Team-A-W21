using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguesManager : MonoBehaviour
{
    public Text dialoguesText;
    public Image characterImage;
    public Image backgroundImage;
    public Button continueButton;

    public static DialoguesManager instance = null; //Singleton;

    private Queue<string> sentences;

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
        sentences = new Queue<string>();
        characterImage.enabled = false;
        backgroundImage.enabled = false;
        dialoguesText.enabled = false;
        continueButton.gameObject.SetActive(false);
    }

    public void StartDialogues(Dialogues dialogues)
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        characterImage.enabled = true;
        backgroundImage.enabled = true;
        dialoguesText.enabled = true;
        continueButton.gameObject.SetActive(true);

        characterImage = dialogues.character;
        backgroundImage = dialogues.background;

        sentences.Clear();

        foreach(string sentence in dialogues.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialoguesText.text = sentence;
    }

    void EndDialogue()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        characterImage.enabled = false;
        backgroundImage.enabled = false;
        dialoguesText.enabled = false;
        continueButton.gameObject.SetActive(false);
    }
}

//Reference
//https://youtu.be/_nRzoTzeyxU