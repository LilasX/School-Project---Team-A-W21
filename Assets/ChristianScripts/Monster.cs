using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    private GameObject player; //Character
    public int maxlife = 100;
    public float speed = 0.1f;
    Rigidbody rig;
    static  Animator anim;
    private GameManager manager; //Reference to our Game Manager
    Vector3 newPos;
    

    // Start is called before the first frame update
    void Start()
    {
       
        manager = GameManager.instance; //Cache of our game manager
        
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
            newPos = Vector3.MoveTowards(transform.position, player.transform.position, speed);
            rig.MovePosition(newPos);
    }

    private void LateUpdate()
    {
        transform.LookAt(player.transform);
    }

   
   
    private void OnCollisionEnter(Collision collision)
    {
        speed = 0.05f;
        if (collision.collider.tag == "Player")
        {
            speed = 0f;
            anim.SetBool("isattacking", true);
        }
    }

    public void Deathanim()
    {
        
        anim.SetBool("iswalking", false);
        anim.SetBool("isattacking", false);
        anim.SetBool("IsDying", true);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            speed = 0.05f;
            Debug.Log("no more collision");
            anim.SetBool("isattacking", false);
            anim.SetBool("iswalking", true);
        }
    }


}
