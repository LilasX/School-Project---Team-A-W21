using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.UI;
using Cinemachine;

public class EndGame : MonoBehaviour
{
    private string sceneToReload = "LilasSceneS"; // Load this scene when starting game
    public PlayableDirector director; //the director that plays the timeline
    [SerializeField] private GameObject buttonreplay; //button replay
    [SerializeField] private GameObject buttonquit; //button quit
    [SerializeField] private Text gameCompleted; //text displayed with button quit and replay
    [SerializeField] private GameObject canvas; //canvas used in timeline
    [SerializeField] private GameObject timeline; //the timeline playing in this scene

    public void ReloadGame()
    {
        SceneManager.LoadScene(sceneToReload); //Load the game scene when replay button is clicked
    }

    public void Quit()
    {
        Application.Quit();
    }

    void OnEnable()
    {
        director.stopped += OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            //Active when the director is done playing the timeline
            buttonreplay.SetActive(true);
            buttonquit.SetActive(true);
            gameCompleted.enabled = true;

            //Inactive when the director is done playing the timeline
            timeline.SetActive(false);
            canvas.SetActive(false);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    void OnDisable()
    {
        director.stopped -= OnPlayableDirectorStopped;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        buttonreplay.SetActive(false);
        buttonquit.SetActive(false);
        gameCompleted.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

//Reference
//https://docs.unity3d.com/2020.2/Documentation/ScriptReference/Playables.PlayableDirector-stopped.html //2021-03-17