using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objectsCount : MonoBehaviour
{

    public Transform Spawnpoint;
    public GameObject Prefab;
    public GameObject monster;
    GameObject objUI;
    [SerializeField] private GameObject noticeAll;
    private string pretext = "Collectable = ";
    bool portalhasnotspawned = true;
    Vector3 offset = new Vector3(0, 0, 5);
    // Use this for initialization
    void Start()
    {
        objUI = GameObject.Find("Collectable");
        objUI.gameObject.SetActive(false);
        noticeAll.gameObject.SetActive(false);
    }

    public void Settrue()
    {
        objUI.SetActive(true);
    }

    void Setfalse()
    {
        objUI.SetActive(false);
    }

    void NoticeOff()
    {
        noticeAll.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (objectsToCollect.objects > 0)
        {
            objUI.GetComponent<Text>().text = pretext + objectsToCollect.objects.ToString();
        }
       
       
        else if (objectsToCollect.objects == 0 && portalhasnotspawned == true)
        {
            Instantiate(Prefab, Spawnpoint.position, Spawnpoint.rotation);
            Instantiate(monster, Spawnpoint.position + offset, Spawnpoint.rotation);
            objUI.GetComponent<Text>().text = pretext + "Complete";
            //Invoke("Setfalse", 4f);
            portalhasnotspawned = false;

            noticeAll.SetActive(true);
            Invoke("NoticeOff", 5f);
        }
        else
        {
            return;
        }

    }
}
