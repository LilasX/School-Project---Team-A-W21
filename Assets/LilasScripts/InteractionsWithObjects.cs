using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionsWithObjects : MonoBehaviour
{
    private GameManager manager; //Reference to our Game Manager
    [SerializeField] private CharacterController controller; //Reference to the controller of the character

    [SerializeField] private ParticleSystem healingcircle; //one of the particle system in the prefab
    [SerializeField] private ParticleSystem healingstar; //one of the particle system in the prefab

    private string sceneToReload = "LilasSceneS2"; // Load this scene when game completed

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance; //Cache of our game manager
        controller = GetComponent<CharacterController>(); //Controller cache
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy") //Enemies
        {
            controller.enabled = false; //we need to deactivate the controller or it won't respawn (Start of reference 1)
            manager.Dead(); //when character collides with deadly objects, call this method
            controller.enabled = true; //reactivate the controller after respawning (End of reference 1)
        }

        if (collision.gameObject.tag == "Monster") //Monster
        {
            controller.enabled = false; //we need to deactivate the controller or it won't respawn (Start of reference 1)
            manager.Dead(); //when character collides with deadly objects, call this method
            controller.enabled = true; //reactivate the controller after respawning (End of reference 1)
        }

        if (collision.gameObject.tag == "SecretB") //Capsule close to character's respawning point
        {
            controller.enabled = false; //we need to deactivate the controller or it won't respawn (Start of reference 1)
            manager.SecretEventActivate(); //activate UI with coded message
            controller.enabled = true; //reactivate the controller after respawning (End of reference 1)
        }

        if (collision.gameObject.tag == "SecretE") //capsule active after colliding with the capsule close to character's respawning point
        {
            controller.enabled = false; //we need to deactivate the controller or it won't respawn (Start of reference 1)
            manager.SecretEventAnswer(); //activate UI with the riddle
            controller.enabled = true; //reactivate the controller after respawning (End of reference 1)
        }

        if(collision.gameObject.tag == "Portal")
        {
            SceneManager.LoadScene(sceneToReload); //Load the game scene
        }
    }


    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            if (hit.collider.tag == "Death") //Lava
            {
                controller.enabled = false; //we need to deactivate the controller or it won't respawn (Start of reference 1)
                manager.Dead(); //when character collides with deadly objects, call this method
                controller.enabled = true; //reactivate the controller after respawning (End of reference 1)
            }
        }
    }
}

//References
//1- https://www.reddit.com/r/Unity3D/comments/b8p8tz/can_someone_help_me_figure_out_why_my_simple/