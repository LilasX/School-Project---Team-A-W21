using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummies : MonoBehaviour
{
    public GameObject confirmation;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider plyr)
    {
        if (plyr.gameObject.tag == "Dummy")
        {
            confirmation.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    //OnClick
    public void HideConfirmation() //Lilas
    {
        confirmation.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
