using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class LoadGameScene : MonoBehaviour
{
    private string sceneToReload = "LilasScene"; // Load this scene when starting game
    public PlayableDirector director; //director playing the timeline in this scene

    public void LoadGame()
    {
        SceneManager.LoadScene(sceneToReload); //Load the game scene
    }

    void OnEnable()
    {
        director.stopped += OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            //Load the game scene when cutscene is over
            SceneManager.LoadScene(sceneToReload); //Load the game scene
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

//Reference
//https://docs.unity3d.com/2020.2/Documentation/ScriptReference/Playables.PlayableDirector-stopped.html //2021-03-17