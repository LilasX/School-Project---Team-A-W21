using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objectsToCollect : MonoBehaviour
{
    public static int objects = 0;
    // Use this for initialization
    void Awake()
    {
        objects = 0;
        objects++;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider plyr)
    {
        if (plyr.gameObject.tag == "Player")
            objects--;
        gameObject.SetActive(false);
    }




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
