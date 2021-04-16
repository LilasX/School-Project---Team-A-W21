using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    private GameManager manager; //Reference to our Game Manager
    [SerializeField] private float fireRange = 100f; //shooting distance
    private bool firing; //Input system
    public LayerMask layerE;
    private Monster monster; // monster spawning after portal is unlocked
   



    public void OnFire(InputAction.CallbackContext context)
    {
        firing = context.performed;
    }

    // Start is called before the first frame update
    void Start()
    {
        monster = new Monster(); //Christian
        manager = GameManager.instance; //Cache of our game manager
    }

    // Update is called once per frame
    void Update()
    {
        if (!manager.NoShoot())
        {
            if (firing)
            {
                firing = false; //to not shoot constantly all at once when pressing and releasing
                manager.LoseStamina();
                manager.effectshoot.gameObject.SetActive(true); //image activates every time the player shoots
                
                
                RaycastHit hit; //object properties that is touching the ray
                if (Physics.Raycast(transform.position, transform.forward, out hit, fireRange, layerE))
                {
                    //Draw a line if the ray is colliding with an object
                    Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);

                   

                    if (hit.collider.tag == "Enemy")
                    {
                        Destroy(hit.collider.gameObject); //destroy enemy game object
                    }

                    //Christian Begin
                    if (hit.collider.tag == "Monster")
                    {
                        monster.maxlife -= 25;


                        if (monster.maxlife <= 0)
                        {
                            
                            monster.speed = 0;
                            monster.Deathanim();
                            
                            Destroy(hit.collider.gameObject,3f); //destroy enemy game object
                        }
                    }
                    //Christian End

                }
                else
                {
                    Debug.DrawRay(transform.position, transform.forward * fireRange, Color.red);
                }
            }
            else
            {
                manager.effectshoot.gameObject.SetActive(false); //image is active only when shooting
            }
        }
    }
}

//Reference
//1- https://youtu.be/K_svD1T9XH0?t=11126 (Game Engine I's class FPS project)
//2- https://youtu.be/0wguEh1UQ48?t=2033 (Game Engine I's class FPS project suite)