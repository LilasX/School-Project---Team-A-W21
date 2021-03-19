using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverOnCollision : MonoBehaviour
{
    private Winner winner;
    
    // Start is called before the first frame update

    
    void Start()
    {
        winner = Winner.instance;
    }

    void OnTriggerEnter(Collider plyr)
    {
        if (plyr.gameObject.tag == "Player")
        {
            winner.CompletedGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
