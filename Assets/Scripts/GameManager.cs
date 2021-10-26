using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool paused = false;
    void Update() {

        if (Input.GetButtonDown("Pause")) {
            Pause();
        }

    }

    void Pause() {
        if (!paused) {
            paused = true;
            Time.timeScale = 0;
            
        } else {
            paused = false;
            Time.timeScale = 1;
            
        }
    }
}
