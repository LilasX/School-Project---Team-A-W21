using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objectsToCollect : MonoBehaviour
{
    List<GameObject> flowers = new List<GameObject>();
    public Transform Spawnpoint;
    public GameObject Prefab;
    public GameObject monster;
    GameObject objUI;
    
    private string pretext = "Collectable = ";  //Lilas
    bool portalhasnotspawned = true;
    Vector3 offset = new Vector3(0, 0, 5);
    private int numberofflowertocollect;
    // Use this for initialization
    void Start()
    {
        objUI = GameObject.Find("Collectable");
        objUI.gameObject.SetActive(false);
        

        foreach (GameObject flower in GameObject.FindGameObjectsWithTag("Flower"))
        {
            flowers.Add(flower);
        }
        numberofflowertocollect = flowers.Count;
    }

    //Onclick
    public void Settrue() //Lilas
    {
        objUI.SetActive(true);
    }

    void OnTriggerEnter(Collider plyr)
    {
        if (plyr.gameObject.tag == "Flower")
            numberofflowertocollect--;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (numberofflowertocollect > 0)
        {
            objUI.GetComponent<Text>().text = pretext + numberofflowertocollect.ToString();
        }


        else if (numberofflowertocollect == 0 && portalhasnotspawned == true)
        {
            Instantiate(Prefab, Spawnpoint.position, Spawnpoint.rotation);
            Instantiate(monster, Spawnpoint.position + offset, Spawnpoint.rotation);
            objUI.GetComponent<Text>().text = pretext + "Complete"; //Lilas
            portalhasnotspawned = false;
        }
        else
        {
            return;
        }

    }
}
