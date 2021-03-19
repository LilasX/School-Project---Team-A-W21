using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class LoadGameScene : MonoBehaviour
{
    private string sceneToReload = "LilasScene"; // Load this screen when starting game
    public PlayableDirector director;

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