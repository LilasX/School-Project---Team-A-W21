using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null; //Singleton;

    [SerializeField] private GameObject character; //Reference to our character
    [SerializeField] private GameObject respawningC; //Reference to where the character respawns after dying
    [SerializeField] private GameObject enemies; //Reference of our enemy prefab
    [SerializeField] private GameObject respawnELeft; //Reference to where the enemies will be spawning from
    [SerializeField] private GameObject respawnERight; //Reference to where the enemies will be spawning from
    public bool canMoveE = true; //if enemies move or not
    [SerializeField] private Camera cameraPlayer; //camera with character visible
    [SerializeField] private Camera cameraShoot; //first person camera for shooting
    [SerializeField] private Camera cameraMenu; //camera for the menu
    [SerializeField] private Image cursorshoot; //shooting image
    public Image effectshoot; //shooting image effect when shooting
    //private bool sCamPlayer = false;
    private bool sCamShoot = false;
    [SerializeField] private GameObject rigidchar; //reference to the rigidbody replacing character
    private string sceneToReload = "LilasSceneS"; // Load this screen when retrying game
    public bool gameIsOn = false; //when the game started officially excluding menu

    //Menu UI
    [SerializeField] private Text title; //Reference for the title
    [SerializeField] private GameObject menuBG; //plane used for menu background
    [SerializeField] private GameObject btnReplay; //Reference for button play
    [SerializeField] private GameObject btnQuit; //Reference for button play
    [SerializeField] private GameObject btnBegin; //Reference for button Begin
    [SerializeField] private Text txtRules; //text for rules
    [SerializeField] private Text txtRulesC; //text for rules
    [SerializeField] private Text pageRules; //page number for rules
    [SerializeField] private GameObject btnRulesC; //Reference for button rules continue
    [SerializeField] private GameObject btnRulesB; //Reference for button rules back
    [SerializeField] private Text txtAdditionalInfo; //text for AdditionalInfo
    [SerializeField] private Text txtAdditionalInfoC; //text for AdditionalInfo
    [SerializeField] private Text pageAdditionalInfo; //page number for AdditionalInfo
    [SerializeField] private GameObject btnAdditionalInfoC; //Reference for button AdditionalInfo continue
    [SerializeField] private GameObject btnAdditionalInfoB; //Reference for button AdditionalInfo back
    [SerializeField] private Text txtControls; //text for controls
    [SerializeField] private Text txtControlsC; //text for controls
    [SerializeField] private Text pageControls; //page number for controls
    [SerializeField] private GameObject btnControlsB; //Reference for button controls back
    [SerializeField] private GameObject btnplay; //Reference for button play
    [SerializeField] private GameObject settings; //Reference to the settings object
    [SerializeField] private GameObject settingsButton; //Reference to the button for settings in the menu

    //Life and stamina for shooting
    [SerializeField] private int lives = 5; //number of lives before game over
    private const string prelives = "Lives = "; //pretext before showing the number of lives
    [SerializeField] private Text txtlives; //text for lives
    [SerializeField] private Text diedOnce; //Reference for the message when player died once and gets respawned
    [SerializeField] private Text gameOver; //Reference for the game over txt when dead
    [SerializeField] private float stamina = 100; //number of stamina for shooting
    private const string prestamina = "Stamina = "; //pretext before showing the number of stamina
    [SerializeField] private Text txtstamina; //text for stamina
    [SerializeField] private Text txtnostamina; //text for no more stamina and cannot enter shooting mode
    [SerializeField] private Text txtnoshooting; //text for no shooting when recovering stamina
    private static float recovery = 5f; //recover stamina 
    private bool healed; //after healing
    private bool instantiateMode = false; //if we're using the method to instantiate enemies instead of InvokeRepeating
    [SerializeField] private float instantiateTimer = 20f; //the repeat rate of spawning enemies like InvokeRepeating
    [SerializeField] private GameObject aurapar; //Particle system played when interacting with healing spots (to recover stamina)

    //Secret Event related
    [SerializeField] private GameObject secretMessage; //the message that appears after interacting with game object with tag SecretB
    [SerializeField] private GameObject secretobject; //game object with tag SecretE
    [SerializeField] private GameObject secretAnswer; //UI for riddle
    [SerializeField] private GameObject secretBegin; //game object with tag SecretB to activate the other capsule with tag SecretE

    //buttons used for the riddle
    [SerializeField] private Button[] buttonsEvent;
    [SerializeField] private Button buttonH;
    [SerializeField] private Button buttonE;
    [SerializeField] private Button buttonL;
    [SerializeField] private Button buttonP;
    [SerializeField] private Button buttonS;
    [SerializeField] private Button buttonA;
    [SerializeField] private Button buttonT;
    [SerializeField] private Button buttonM;
    [SerializeField] private GameObject[] buttonsPositions;
    private Button buttonSpawn;

    //Text used for the riddle
    [SerializeField] private Text oneIMark;
    [SerializeField] private Text twoIMark;
    [SerializeField] private Text threeIMark;
    [SerializeField] private Text fourIMark;
    private const string h = "H";
    private const string e = "E";
    private const string l = "L";
    private const string p = "P";
    private bool eventActive = false;
    int countB = 0;
    int countCorrectB = 0;
    private bool shootboost = false;
    [SerializeField] private Image ty;
    [SerializeField] private Text tygrateful;
    [SerializeField] private Text boosts;
    [SerializeField] private Button np;

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

    public void SwitchCamShoot(InputAction.CallbackContext context)
    {
        sCamShoot = context.performed;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        gameIsOn = false;
        cameraPlayer.enabled = false;
        cameraShoot.enabled = false;
        cursorshoot.enabled = false;
        effectshoot.gameObject.SetActive(false);
        rigidchar.SetActive(false);
        character.SetActive(false);

        //Menu
        cameraMenu.enabled = true;
        btnBegin.SetActive(true); //the button Begin is available
        btnQuit.SetActive(true); //the button Quit is active
        btnReplay.SetActive(false); //inactive until dead or game completed
        menuBG.SetActive(true); //menu plane is active

        txtRules.enabled = false; //active at appropriate page of the menu
        txtRulesC.enabled = false; //active at appropriate page of the menu
        pageRules.enabled = false; //active at appropriate page of the menu
        btnRulesC.SetActive(false); //active at appropriate page of the menu
        btnRulesB.SetActive(false); //active at appropriate page of the menu
        txtAdditionalInfo.enabled = false; //active at appropriate page of the menu
        txtAdditionalInfoC.enabled = false; //active at appropriate page of the menu
        pageAdditionalInfo.enabled = false; //active at appropriate page of the menu
        btnAdditionalInfoC.SetActive(false); //active at appropriate page of the menu
        btnAdditionalInfoB.SetActive(false); //active at appropriate page of the menu
        txtControls.enabled = false; //active at appropriate page of the menu
        txtControlsC.enabled = false; //active at appropriate page of the menu
        pageControls.enabled = false; //active at appropriate page of the menu
        btnControlsB.SetActive(false); //active at appropriate page of the menu
        btnplay.SetActive(false); //not visible until last page
        settings.SetActive(false); //settings inactive
        settingsButton.SetActive(true); //settings button is active

        //UI in the game
        txtlives.enabled = false; //text hidden
        diedOnce.enabled = false; //when player died once
        gameOver.enabled = false; //inactive unless dead and game over
        txtstamina.enabled = false; //text hidden
        txtnostamina.enabled = false; //text hidden
        txtnoshooting.enabled = false; //text hidden

        aurapar.SetActive(false); //particle system for healing

        //secret event related
        secretMessage.SetActive(false); //until character collides with the secret event object
        secretobject.SetActive(false); //until the player closes the message of the secret event object
        secretAnswer.SetActive(false); //until the player collide with the corresponding objects

        //Secret event with riddle related
        ty.enabled = false;
        tygrateful.enabled = false;
        boosts.enabled = false;
        np.gameObject.SetActive(false);
    }

    //OnClick on the menu
    public void SettingsOn()
    {
        settings.SetActive(true);
    }

    public void TitlePage()
    {
        //Deactivate Pages Before and After
        txtRules.enabled = false; //active at appropriate page of the menu
        txtRulesC.enabled = false; //active at appropriate page of the menu
        pageRules.enabled = false; //active at appropriate page of the menu
        btnRulesC.SetActive(false); //active at appropriate page of the menu
        btnRulesB.SetActive(false); //active at appropriate page of the menu

        //Active in this page
        title.enabled = true; //title is visible
        btnQuit.SetActive(true); //quitting is an option
        btnBegin.SetActive(true); //the button Begin is available
        settingsButton.SetActive(true); //settings button is active
    }

    public void RulesPage()
    {
        //Deactivate Pages Before and After
        title.enabled = false; //title is invisible
        btnQuit.SetActive(false); //inactive
        btnBegin.SetActive(false); //inactive
        settingsButton.SetActive(false); //settings button is inactive
        txtAdditionalInfo.enabled = false; //active at appropriate page of the menu
        txtAdditionalInfoC.enabled = false; //active at appropriate page of the menu
        pageAdditionalInfo.enabled = false; //active at appropriate page of the menu
        btnAdditionalInfoC.SetActive(false); //active at appropriate page of the menu
        btnAdditionalInfoB.SetActive(false); //active at appropriate page of the menu

        //Active in this page
        txtRules.enabled = true; //active at appropriate page of the menu
        txtRulesC.enabled = true; //active at appropriate page of the menu
        pageRules.enabled = true; //active at appropriate page of the menu
        btnRulesC.SetActive(true); //active at appropriate page of the menu
        btnRulesB.SetActive(true); //active at appropriate page of the menu
    }

    public void AdditionalInfoPage()
    {
        //Deactivate Pages Before and After
        txtRules.enabled = false; //active at appropriate page of the menu
        txtRulesC.enabled = false; //active at appropriate page of the menu
        pageRules.enabled = false; //active at appropriate page of the menu
        btnRulesC.SetActive(false); //active at appropriate page of the menu
        btnRulesB.SetActive(false); //active at appropriate page of the menu
        txtControls.enabled = false; //active at appropriate page of the menu
        txtControlsC.enabled = false; //active at appropriate page of the menu
        pageControls.enabled = false; //active at appropriate page of the menu
        btnControlsB.SetActive(false); //active at appropriate page of the menu
        btnplay.SetActive(false); //not visible until last page

        //Active in this page
        txtAdditionalInfo.enabled = true; //active at appropriate page of the menu
        txtAdditionalInfoC.enabled = true; //active at appropriate page of the menu
        pageAdditionalInfo.enabled = true; //active at appropriate page of the menu
        btnAdditionalInfoC.SetActive(true); //active at appropriate page of the menu
        btnAdditionalInfoB.SetActive(true); //active at appropriate page of the menu
    }

    public void ControlsPage()
    {
        //Deactivate Page Before
        txtAdditionalInfo.enabled = false; //active at appropriate page of the menu
        txtAdditionalInfoC.enabled = false; //active at appropriate page of the menu
        pageAdditionalInfo.enabled = false; //active at appropriate page of the menu
        btnAdditionalInfoC.SetActive(false); //active at appropriate page of the menu
        btnAdditionalInfoB.SetActive(false); //active at appropriate page of the menu

        //Active in this page
        txtControls.enabled = true; //active at appropriate page of the menu
        txtControlsC.enabled = true; //active at appropriate page of the menu
        pageControls.enabled = true; //active at appropriate page of the menu
        btnControlsB.SetActive(true); //active at appropriate page of the menu
        btnplay.SetActive(true); //not visible until last page
    }

    public void GameStart()
    {
        gameIsOn = true;
        InvokeRepeating("SpawnEnemies", 0, 20);
        Cursor.lockState = CursorLockMode.Locked;
        cameraPlayer.enabled = true;
        character.SetActive(true);

        cameraMenu.enabled = false;
        menuBG.SetActive(false);
        txtControls.enabled = false; //active at appropriate page of the menu
        txtControlsC.enabled = false; //active at appropriate page of the menu
        pageControls.enabled = false; //active at appropriate page of the menu
        btnControlsB.SetActive(false); //active at appropriate page of the menu
        btnplay.SetActive(false); //not visible until last page

        txtlives.enabled = true; //activate text
        txtlives.text = prelives + lives.ToString("D1"); //show lives
        txtstamina.enabled = true; //activate text
        txtstamina.text = prestamina + Mathf.FloorToInt(stamina).ToString("D3"); //show stamina
    }

    public void Quit()
    {
        Application.Quit(); //Terminate application
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(sceneToReload); //Load the game scene
        
    }

    public void Dead()
    {
        lives--;
        diedOnce.enabled = true; //activate message
        Invoke("DiedOff", 3); //message disappears
        txtlives.text = prelives + lives.ToString("D1"); //show lives
        //character and rigidbody is resawned at character's respawning place
        character.transform.position = respawningC.transform.position;
        rigidchar.transform.position = respawningC.transform.position;
        healed = false;
        CancelInvoke("SpawnEnemies"); //Cancel current InvokeRepeating
        canMoveE = false; //enemies can't move
        instantiateMode = false; //enemies do not spawn with the other function to spawn enemies
        instantiateTimer = 20f; //reinitialize timer to 20f
        InvokeRepeating("SpawnEnemies", 30, 20); //Restart respawning enemies after 30 secs of player's death
        if(lives == 0)
        {
            gameIsOn = false;
            diedOnce.enabled = false; //if game over, do not show message
            Cursor.lockState = CursorLockMode.None;
            gameOver.enabled = true; //active
            btnReplay.SetActive(true); //inactive until dead or game completed
            btnQuit.SetActive(true); //the button Quit is active
            character.SetActive(false); //character disappears
        }
    }

    //Message telling player, they lost a life
    public void DiedOff()
    {
        diedOnce.enabled = false; //deactivate
    }

    //Spawning enemies with InvokeRepeating
    public void SpawnEnemies()
    {
        canMoveE = true;
        GameObject e = Instantiate(enemies, respawnELeft.transform.position, respawnELeft.transform.rotation);
        GameObject e2 = Instantiate(enemies, respawnERight.transform.position, respawnERight.transform.rotation);
    }

    //Spawning enemies without InvokeRepeating
    public void SpawnEnemiesWithoutInvoke()
    {
        //Reference 1
        instantiateTimer -= Time.deltaTime;
        if (Mathf.FloorToInt(instantiateTimer) == 0)
        {
            canMoveE = true;
            GameObject e = Instantiate(enemies, respawnELeft.transform.position, respawnELeft.transform.rotation);
            GameObject e2 = Instantiate(enemies, respawnERight.transform.position, respawnERight.transform.rotation);
            instantiateTimer = 20f;
        }
    }

    //Every time the player shoots, call this method
    public void LoseStamina()
    {
        if (!shootboost) //if they didn't succeed in the secret event
        {
            if (Mathf.FloorToInt(stamina) > 0)
            {
                stamina -= 5;
            }

            if (Mathf.FloorToInt(stamina) >= 0 && Mathf.FloorToInt(stamina) < 5)
            {
                txtnostamina.enabled = true; //inform player, they don't have enough stamina to shoot
                Invoke("NoMoreStamina", 3); //deactivate message informing player they don't have enough stamina to shoot
                sCamShoot = false; //cannot enter shooting mode
                character.transform.position = rigidchar.transform.position;
                character.SetActive(true);
                rigidchar.SetActive(false);
                cameraPlayer.enabled = true;
                cameraShoot.enabled = false;
                cursorshoot.enabled = false;
                effectshoot.gameObject.SetActive(false);
            }
        }
        else //if they did get the boost from secret event
        {
            if (Mathf.FloorToInt(stamina) > 0)
            {
                stamina -= 1;
            }

            if (Mathf.FloorToInt(stamina) >= 0 && Mathf.FloorToInt(stamina) < 1)
            {
                txtnostamina.enabled = true;
                Invoke("NoMoreStamina", 3);
                sCamShoot = false;
                character.transform.position = rigidchar.transform.position;
                character.SetActive(true);
                rigidchar.SetActive(false);
                cameraPlayer.enabled = true;
                cameraShoot.enabled = false;
                cursorshoot.enabled = false;
                effectshoot.gameObject.SetActive(false);
            }
        }
        
        txtstamina.text = prestamina + Mathf.FloorToInt(stamina).ToString(); //show stamina
    }

    //Deactivate message informing player they don't have enough stamina to shoot
    public void NoMoreStamina()
    {
        txtnostamina.enabled = false; //deactivate
    }

    //Activate message to remind the player they don't have enough stamina when they're trying to enter shooting mode
    public void ReminderNoStamina()
    {
        txtnostamina.enabled = true;
        Invoke("NoMoreStamina", 2);
    }
    
    //Deactivate message informing player they can't enter shooting mode when healing
    public void NoShooting()
    {
        txtnoshooting.enabled = false; //deactivate
    }

    //Activate message when player is trying to enter shooting mode when healing
    public void ReminderNoShooting()
    {
        txtnoshooting.enabled = true;
        Invoke("NoShooting", 2);
    }

    //when the player can shoot or not
    public bool NoShoot()
    {
        if (!shootboost)
        {
            if (Mathf.FloorToInt(stamina) >= 0 && Mathf.FloorToInt(stamina) < 5)
            {
                return true;
            }
        }
        else
        {
            if (Mathf.FloorToInt(stamina) >= 0 && Mathf.FloorToInt(stamina) < 1)
            {
                return true;
            }
        }

        if (Time.timeScale == 0)
        {
            return true;
        }
        
        return false;
    }

    //Activate coded message when colliding with capsule with tag SecretB
    public void SecretEventActivate()
    {
        secretMessage.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //Close coded message and activate capsule with tag SecretE
    public void ConfirmSecretMessage()
    {
        secretMessage.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        secretobject.SetActive(true);
    }

    //Activate UI riddle
    public void SecretEventAnswer()
    {
        eventActive = true;
        Time.timeScale = 0;
        secretAnswer.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        for (int i = 0; i < buttonsEvent.Length; i++)
        {
            buttonSpawn = buttonsEvent[i];
            buttonSpawn.gameObject.SetActive(true);
        }

        oneIMark.text = "?";
        twoIMark.text = "?";
        threeIMark.text = "?";
        fourIMark.text = "?";
    }

    //OnClick for Secret Event
    public void SorryButton() //close UI riddle 
    {
        countB = 0;
        countCorrectB = 0;
        eventActive = false;
        Time.timeScale = 1;
        secretAnswer.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //Buttons used for riddle
    public void HButton()
    {
        oneIMark.text = h;
        buttonH.gameObject.SetActive(false);
        countB++;
        countCorrectB++;
    }

    public void EButton()
    {
        twoIMark.text = e;
        buttonE.gameObject.SetActive(false);
        countB++;
        countCorrectB++;
    }

    public void LButton()
    {
        threeIMark.text = l;
        buttonL.gameObject.SetActive(false);
        countB++;
        countCorrectB++;
    }

    public void PButton()
    {
        fourIMark.text = p;
        buttonP.gameObject.SetActive(false);
        countB++;
        countCorrectB++;
    }

    public void AButton()
    {
        buttonA.gameObject.SetActive(false);
        countB++;
    }

    public void SButton()
    {
        buttonS.gameObject.SetActive(false);
        countB++;
    }

    public void TButton()
    {
        buttonT.gameObject.SetActive(false);
        countB++;
    }

    public void MButton()
    {
        buttonM.gameObject.SetActive(false);
        countB++;
    }

    //Activate when player succeed completing the riddle
    public void EventSuccess()
    {
        ty.enabled = true;
        tygrateful.enabled = true;
        boosts.enabled = true;
        np.gameObject.SetActive(true);
    }

    //After maths of the success of the riddle and boost acquired
    public void EventSuccessConfirm()
    {
        ty.enabled = false;
        tygrateful.enabled = false;
        boosts.enabled = false;
        np.gameObject.SetActive(false);
        

        shootboost = true;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        character.GetComponent<LocomotionCharacterController>().speed = 10f; //increase speed to 10f
        foreach (GameObject currentenemies in GameObject.FindGameObjectsWithTag("Enemy")) //destroy current spawned enemies
        {
            Destroy(currentenemies);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsOn) //after passing the menu
        {
            if (!shootboost)
            {
                if (Mathf.FloorToInt(stamina) >= 5)
                {
                    if (sCamShoot)
                    {
                        rigidchar.transform.position = character.transform.position;
                        //sCamPlayer = false;
                        character.SetActive(false);
                        rigidchar.SetActive(true);
                        cameraShoot.enabled = true;
                        cameraPlayer.enabled = false;
                        cursorshoot.enabled = true;
                    }
                    else
                    {
                        sCamShoot = false;
                        character.SetActive(true);
                        rigidchar.SetActive(false);
                        cameraPlayer.enabled = true;
                        cameraShoot.enabled = false;
                        cursorshoot.enabled = false;
                        effectshoot.gameObject.SetActive(false);
                    }
                }
                else if (Mathf.FloorToInt(stamina) >= 0 && Mathf.FloorToInt(stamina) < 5)
                {
                    if (sCamShoot)
                    {
                        ReminderNoStamina();
                        sCamShoot = false;
                        rigidchar.SetActive(false);
                        cameraPlayer.enabled = true;
                        cameraShoot.enabled = false;
                        cursorshoot.enabled = false;
                        effectshoot.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                if (Mathf.FloorToInt(stamina) >= 1)
                {
                    if (sCamShoot)
                    {
                        rigidchar.transform.position = character.transform.position;
                        //sCamPlayer = false;
                        character.SetActive(false);
                        rigidchar.SetActive(true);
                        cameraShoot.enabled = true;
                        cameraPlayer.enabled = false;
                        cursorshoot.enabled = true;
                    }
                    else
                    {
                        sCamShoot = false;
                        character.SetActive(true);
                        rigidchar.SetActive(false);
                        cameraPlayer.enabled = true;
                        cameraShoot.enabled = false;
                        cursorshoot.enabled = false;
                        effectshoot.gameObject.SetActive(false);
                    }
                }
                else if (Mathf.FloorToInt(stamina) >= 0 && Mathf.FloorToInt(stamina) < 1)
                {
                    if (sCamShoot)
                    {
                        ReminderNoStamina();
                        sCamShoot = false;
                        rigidchar.SetActive(false);
                        cameraPlayer.enabled = true;
                        cameraShoot.enabled = false;
                        cursorshoot.enabled = false;
                        effectshoot.gameObject.SetActive(false);
                    }
                }
            }

            if (!rigidchar.activeInHierarchy) //for the image to be inactive after no stamina to shoot
            {
                effectshoot.gameObject.SetActive(false);
            }

            RaycastHit hit;
            if (Physics.Raycast(character.transform.position, Vector3.down, out hit, 5))
            {
                if (hit.collider.tag == "Heal") //Heal object
                {
                    aurapar.transform.position = character.transform.position;
                    aurapar.SetActive(true);
                    CancelInvoke("SpawnEnemies");
                    instantiateMode = false;
                    instantiateTimer = 20f;
                    canMoveE = false;
                    healed = true;
                    if (Mathf.FloorToInt(stamina) < 100)
                    {
                        stamina += recovery * Time.deltaTime;
                    }

                    txtstamina.text = prestamina + Mathf.FloorToInt(stamina).ToString(); //show stamina

                    if (sCamShoot)
                    {
                        ReminderNoShooting();
                        sCamShoot = false;
                        character.transform.position = rigidchar.transform.position;
                        character.SetActive(true);
                        rigidchar.SetActive(false);
                        cameraPlayer.enabled = true;
                        cameraShoot.enabled = false;
                        cursorshoot.enabled = false;
                        effectshoot.gameObject.SetActive(false);
                    }
                }
                else if (!canMoveE && healed && !instantiateMode)
                {
                    healed = false;
                    instantiateMode = true;
                    canMoveE = true;
                    aurapar.SetActive(false);
                }
            }

            //Cannot call InvokeRepeating in Update so we have to make one manually to spawn enemies at certain rate
            if (instantiateMode)
            {
                SpawnEnemiesWithoutInvoke();
                CancelInvoke("SpawnEnemies");
            }

            //Secret event
            if (eventActive)
            {
                if(countB == 4)
                {
                    if(countCorrectB == 4)
                    {
                        EventSuccess();
                        secretAnswer.SetActive(false);
                        secretBegin.SetActive(false);
                        secretobject.SetActive(false);
                        eventActive = false;
                    }
                    else
                    {
                        SorryButton();
                    }
                    countB = 0;
                    countCorrectB = 0;
                }
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            sCamShoot = false;
            cameraShoot.enabled = false;
            cursorshoot.enabled = false;
            effectshoot.gameObject.SetActive(false);
        }
    }
}

//Reference
//1- https://answers.unity.com/questions/637597/instantiate-at-intervals.html (for instantiating prefabs at a certain interval to replace InvokeRepeating)