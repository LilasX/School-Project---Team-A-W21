using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objectsToCollect : MonoBehaviour
{
    public static int objects = 0;
    public GameObject MessagePanel;
    // Start is called before the first frame update

    void Awake()
    {
        objects++;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            OpenMessagePanel("");

            
        }

       
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("IsPressing");
                objects--;
                gameObject.SetActive(false);
            }

        }
       
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            CloseMessagePanel("");
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMessagePanel(string text)
    {
        MessagePanel.SetActive(true);
    }
    public void CloseMessagePanel(string text)
    {
        MessagePanel.SetActive(false);
    }
}
