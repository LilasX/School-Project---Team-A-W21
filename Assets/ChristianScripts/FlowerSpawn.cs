using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpawn : MonoBehaviour
{
    public GameObject[] spawns;
    public GameObject objects;
    public List<GameObject> flowers = new List<GameObject>();
    [Range(0, 20)]
    public int flowerstochangetag = 10;


    //public List<GameObject> flowersDummies = new List<GameObject>();
    private void Awake()
    {
        SpawnObjects();
        //flowersDummies = GetRandomFlowersFromList(flowers, 10);
        ChangeTagOfFlowers();
        //Debug.Log(GetNumberOfFlowers());
    }


    private void SpawnObjects()
    {
        for(int i = 0; i < spawns.Length;i++)
        {
            flowers.Add(Instantiate(objects, spawns[i].transform.position, Quaternion.identity));
        }
        
    }

    private void ChangeTagOfFlowers()
    {
        var randomObjects = new GameObject[flowerstochangetag];

        for (int i = 0; i < flowerstochangetag; i++)
        {
            // Take only from the latter part of the list - ignore the first i items.
            int take = Random.Range(i, flowers.Count);
            randomObjects[i] = flowers[take];

            // Swap our random choice to the beginning of the array,
            // so we don't choose it again on subsequent iterations.
            flowers[take] = flowers[i];
            flowers[i] = randomObjects[i];
            flowers[i].transform.transform.gameObject.tag = "Dummy";
        }
    }

   


}
