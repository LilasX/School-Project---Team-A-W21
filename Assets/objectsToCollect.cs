using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objectsToCollect : MonoBehaviour
{
    public static int objects = 0;
    // Start is called before the first frame update
    public Text flowers;

    void Awake()
    {
         
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            objects++;
            flowers.text = objects.ToString();
            gameObject.SetActive(false);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
