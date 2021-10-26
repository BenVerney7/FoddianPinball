using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public AudioSource winSound;
    
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Ball") {
            winSound.Play(); 
            Time.timeScale = 0;
        }
    }
}
