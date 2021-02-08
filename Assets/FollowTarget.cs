using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowTarget : MonoBehaviour
{
    //[SerializeField] private Transform target; //Character
    private Transform target; //Character
    private Vector3 destination; //Where enemies should go
    private NavMeshAgent agent; //Enemy

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
}

//References
//1- https://docs.unity3d.com/2020.2/Documentation/ScriptReference/AI.NavMeshAgent-destination.html
//2- https://youtu.be/3EAS9OkvJIk?t=7084 (Game Engine I's class on NavMesh)
//3- https://gamedev.stackexchange.com/questions/166678/unity3d-ai-follow-player-script-for-prefab-enemy
//4- https://docs.unity3d.com/2020.2/Documentation/ScriptReference/AI.NavMeshAgent-isStopped.html