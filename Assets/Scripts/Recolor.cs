using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recolor : MonoBehaviour
{

    public Color color;
    public bool custom = false;
    // Start is called before the first frame update
    void Start()
    {
        if (custom) {
            gameObject.GetComponent<Renderer>().material.color = color;
        } else {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
