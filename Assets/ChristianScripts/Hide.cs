using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public GameObject hide;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Hidepanel()
    {
        hide.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
