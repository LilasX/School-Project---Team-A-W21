using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomobjectSpawn : MonoBehaviour
{
    public Transform spawns;
    public GameObject objects;
    private int spawnIndex;
 

    void Start()
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {

        spawnIndex = Random.Range(4,6);

        Instantiate(objects, spawns.position, Quaternion.identity);
       
    }
}
