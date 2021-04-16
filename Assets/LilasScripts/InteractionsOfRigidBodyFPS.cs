using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsOfRigidBodyFPS : MonoBehaviour
{
    private GameManager manager; //Reference to our Game Manager
    [SerializeField] private GameObject charFPS; //Reference to the rigidbody replacing the character



    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance; //Cache of our game manager
        charFPS = GetComponent<GameObject>(); //this object cache
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy") //Enemies
        {
            manager.Dead(); //when character collides with deadly objects, call this method
        }

        if (collision.gameObject.tag == "Monster") //Monster
        {
            manager.Dead(); //when character collides with deadly objects, call this method
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            if (hit.collider.tag == "Death") //Lava
            {
                manager.Dead(); //when character collides with deadly objects, call this method
            }
        }
    }
}
