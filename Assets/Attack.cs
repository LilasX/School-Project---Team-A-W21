using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : MonoBehaviour
{
    //[SerializeField] private Transform target; //Character
    private Transform target; //Character
    private Vector3 destination; //Where enemies should go
    private NavMeshAgent agent; //Enemy

    public Animator anim;
    private GameManager manager; //Reference to our Game Manager

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance; //Cache of our game manager
        agent = GetComponent<NavMeshAgent>(); //Cache enemy
        target = GameObject.FindWithTag("Player").transform; //Respawned enemies can find the target with tag Player (Reference 3)
        destination = target.position; //We want the enemy to go toward the player's position
        agent.destination = destination; //Start and calculate the enemy's course

        
    }

    // Update is called once per frame
    void Update()
    {
        //Update the enemy's destination, if the destination is moving
        if (Vector3.Distance(target.position, destination) > 1.0f)
        {
            destination = target.position; //The enemy's destination is the player's position
            agent.destination = destination; //Calculate the enemy's course
           
        }

        //Reference 4
        if (!manager.canMoveE)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
            

        }



    }

    private void LateUpdate()
    {
        transform.LookAt(target);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.name == "CharacterMCFBX")
        {
            anim.Play("Zombie Attack");
        }
    }


}
