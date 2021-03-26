using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    //[SerializeField] private Transform target; //Character
    private Transform target; //Character
    public int life = 100;
    private NavMeshAgent agent; //Enemy

    static  Animator anim;
    private GameManager manager; //Reference to our Game Manager
    public float MobdistanceRun = 4.0f;
    Vector3 newPos;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance; //Cache of our game manager
        agent = GetComponent<NavMeshAgent>(); //Cache enemy
        target = GameObject.FindWithTag("Player").transform; 
       
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if(distance < MobdistanceRun)
        {
            Vector3 dirtoPlayer = transform.position - target.transform.position;
            newPos = transform.position - dirtoPlayer;
            agent.SetDestination(newPos);
        }

        /*//Update the enemy's destination, if the destination is moving
        if (Vector3.Distance(target.position, destination) > 1.0f)
        {
            destination = target.position; //The enemy's destination is the player's position
            agent.destination = destination; //Calculate the enemy's course
            

        }*/



        

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
        if(collision.collider.tag == "Player")
        {
            anim.SetBool("isattacking", true);
        }
    }

    public void Deathanim()
    {
        //newPos = transform.position;
        anim.SetBool("iswalking", false);
        anim.SetBool("isattacking", false);
        anim.SetBool("IsDying", true);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("no more collision");
            anim.SetBool("isattacking", false);
            anim.SetBool("iswalking", true);
        }
    }

}
