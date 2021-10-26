using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    public Transform respawnPoint;

    float resetTimer = 0f;

    public GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Restart")) {
            
            resetTimer += Time.deltaTime;
            if(resetTimer >= 3f) {
                Respawn();
                resetTimer = 0f;
            }
        } else {
            resetTimer = 0f;
        }


    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Ball") {
            other.attachedRigidbody.velocity = Vector3.zero;
        other.attachedRigidbody.angularVelocity = Vector3.zero;

        other.gameObject.transform.position = respawnPoint.position;
        }
    }

    void Respawn() {
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;

        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        ball.transform.position = respawnPoint.position;
    }
}
