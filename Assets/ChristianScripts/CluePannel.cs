using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CluePannel : MonoBehaviour
{
    ParseXML parse;
    public GameObject messagepannel;
    public Text cluemessage;
    private int check = 0;

    // Start is called before the first frame update
    void Start()
    {
        parse = gameObject.AddComponent<ParseXML>();
        messagepannel.SetActive(false);
    }

    void OnTriggerEnter(Collider plyr)
    {
        if (plyr.gameObject.tag == "Flower")
        {
            //Lilas begin
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //Lilas End

            if (check == 0)
            {
                messagepannel.SetActive(true);
                List<Dictionary<string, string>> allTextDic = parse.parseFile();
                Dictionary<string, string> dic = allTextDic[0];
                cluemessage.text = dic["firstclue"];
            }

            if (check == 1)
            {
                messagepannel.SetActive(true);
                List<Dictionary<string, string>> allTextDic = parse.parseFile();
                Dictionary<string, string> dic = allTextDic[0];
                cluemessage.text = dic["secondclue"];
            }

            if (check == 2)
            {
                messagepannel.SetActive(true);
                List<Dictionary<string, string>> allTextDic = parse.parseFile();
                Dictionary<string, string> dic = allTextDic[1];
                cluemessage.text = dic["firstclue"];
            }

            if (check == 3)
            {
                messagepannel.SetActive(true);
                List<Dictionary<string, string>> allTextDic = parse.parseFile();
                Dictionary<string, string> dic = allTextDic[1];
                cluemessage.text = dic["secondclue"];
            }

            if (check == 4)
            {
                messagepannel.SetActive(true);
                List<Dictionary<string, string>> allTextDic = parse.parseFile();
                Dictionary<string, string> dic = allTextDic[2];
                cluemessage.text = dic["firstclue"];
            }

            if (check == 5)
            {
                messagepannel.SetActive(true);
                List<Dictionary<string, string>> allTextDic = parse.parseFile();
                Dictionary<string, string> dic = allTextDic[2];
                cluemessage.text = dic["secondclue"];
            }

            if (check == 6)
            {
                messagepannel.SetActive(true);
                List<Dictionary<string, string>> allTextDic = parse.parseFile();
                Dictionary<string, string> dic = allTextDic[3];
                cluemessage.text = dic["firstclue"];
            }

            if (check == 7)
            {
                messagepannel.SetActive(true);
                List<Dictionary<string, string>> allTextDic = parse.parseFile();
                Dictionary<string, string> dic = allTextDic[3];
                cluemessage.text = dic["secondclue"];
            }

            if (check == 8)
            {
                messagepannel.SetActive(true);
                List<Dictionary<string, string>> allTextDic = parse.parseFile();
                Dictionary<string, string> dic = allTextDic[4];
                cluemessage.text = dic["firstclue"];
            }

            if (check == 9)
            {
                messagepannel.SetActive(true);
                List<Dictionary<string, string>> allTextDic = parse.parseFile();
                Dictionary<string, string> dic = allTextDic[4];
                cluemessage.text = dic["secondclue"];
            }

            check++;
            
        }

    }

    //OnClick
    public void CloseMessageBox() //Lilas
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        messagepannel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
