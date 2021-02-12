using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objectsCount : MonoBehaviour
{
    public Text numberofflowers;
    int numberOfflowers;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] flowers = GameObject.FindGameObjectsWithTag("Flower");
        int numberOfflowers = flowers.Length;
    }

    // Update is called once per frame
    void Update()
    {
        numberofflowers.text = numberOfflowers.ToString();
    }
}
