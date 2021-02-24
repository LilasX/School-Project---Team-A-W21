using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objectsCount : MonoBehaviour
{

    public Transform Spawnpoint;
    public GameObject Prefab;
    GameObject objUI;
    // Use this for initialization
    void Start()
    {
        objUI = GameObject.Find("ObjectNum");
    }
    // Update is called once per frame
    void Update()
    {
        objUI.GetComponent<Text>().text = objectsToCollect.objects.ToString();
        if (objectsToCollect.objects == 0)
        {
            Instantiate(Prefab, Spawnpoint.position, Spawnpoint.rotation);
            objUI.GetComponent<Text>().text = "All objects collected.";
            Destroy(objUI,4f);
        }

    }
}
