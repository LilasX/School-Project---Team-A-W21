using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsWithObjects : MonoBehaviour
{
    private GameManager manager; //Reference to our Game Manager
    [SerializeField] private CharacterController controller; //Reference to the controller of the character

    [SerializeField] private ParticleSystem healingcircle; //one of the particle system in the prefab
    [SerializeField] private ParticleSystem healingstar; //one of the particle system in the prefab


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