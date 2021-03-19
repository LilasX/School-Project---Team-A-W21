using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMarker : MonoBehaviour
{
    public Transform player;
    public RectTransform icon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        icon.anchoredPosition = player.position;
    }
}
