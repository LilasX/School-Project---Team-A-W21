using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aura : MonoBehaviour
{
    public GameObject aurapar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider plyr)
    {
        if (plyr.gameObject.tag == "Player")
        {
            aurapar.SetActive(true);
        }
    }
    void OnTriggerExit(Collider plyr)
    {
        if (plyr.gameObject.tag == "Player")
        {
            aurapar.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
