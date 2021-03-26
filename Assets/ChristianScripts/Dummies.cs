using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummies : MonoBehaviour
{
    public GameObject confirmation;
    private GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
    }
    void OnTriggerEnter(Collider plyr)
    {
        if (plyr.gameObject.tag == "Player")
        {
            confirmation.SetActive(true);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Destroy(gameObject);
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
