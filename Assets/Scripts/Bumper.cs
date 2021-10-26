using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float power;


    private void OnCollisionEnter(Collision collision) {
        
        foreach (ContactPoint contact in collision.contacts) {
            contact.otherCollider.attachedRigidbody.AddForce(-1 * contact.normal * power, ForceMode.Impulse);
        }
    }
}
