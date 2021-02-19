using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null; //Singleton;

    [SerializeField] private GameObject character; //Reference to our character
    [SerializeField] private GameObject respawningC; //Reference to where the character respawns after dying
    [SerializeField] private GameObject enemies; //Reference of our enemy prefab
    [SerializeField] private GameObject respawnE; //Reference to where the enemies will be spawning from
    //private float oldTime = 0f; //Old time (0 by default) which allows us to toggle (switch) between a pause and the time scale of the game
    public bool canMoveE = true; //if enemies move or not
    [SerializeField] private Camera cameraPlayer; //camera with character visible
    [SerializeField] private Camera cameraShoot; //first person camera for shooting
    [SerializeField] private Image cursorshoot; //shooting image
    private bool sCamPlayer = false;
    private bool sCamShoot = false;
    [SerializeField] private GameObject rigidchar; //reference to the rigidbody replacing character

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

    public void SwitchCamPlayer(InputAction.CallbackContext context)
    {
        sCamPlayer = context.performed;
    }

    public void SwitchCamShoot(InputAction.CallbackContext context)
    {
        sCamShoot = context.performed;
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemies", 0, 10);
        Cursor.lockState = CursorLockMode.Locked;
        cameraPlayer.enabled = true;
        cameraShoot.enabled = false;
        cursorshoot.enabled = false;
        rigidchar.SetActive(false);
    }

    public void Dead()
    {
        Debug.Log("Player died.");
        character.transform.position = respawningC.transform.position;
        rigidchar.transform.position = respawningC.transform.position;
        CancelInvoke("SpawnEnemies");
        canMoveE = false;
        InvokeRepeating("SpawnEnemies", 30, 10);
    }

    public void SpawnEnemies()
    {
        canMoveE = true;
        GameObject e = Instantiate(enemies, respawnE.transform.position, respawnE.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (sCamPlayer)
        {
            character.transform.position = rigidchar.transform.position;
            sCamShoot = false;
            character.SetActive(true);
            rigidchar.SetActive(false);
            cameraPlayer.enabled = true;
            cameraShoot.enabled = false;
            cursorshoot.enabled = false;
        }
        else if (sCamShoot)
        {
            rigidchar.transform.position = character.transform.position;
            sCamPlayer = false;
            character.SetActive(false);
            rigidchar.SetActive(true);
            cameraShoot.enabled = true;
            cameraPlayer.enabled = false;
            cursorshoot.enabled = true;
        }

        //if (Input.GetButtonDown("Cancel")) //Pausing the game with button Escape
        //{
        //    //Reference 1
        //    float prevTime = oldTime; //Hide the saved timeScaled
        //    oldTime = Time.timeScale; //Permute (alter) the timeScale so we can come back to it
        //    Time.timeScale = prevTime; //Change timeScale for the hidden value
        //}
    }
}

//References
//1- Script for pausing the game in Game Engine I class project Rebonds : https://youtu.be/4fmy_ymj6jE?t=3332