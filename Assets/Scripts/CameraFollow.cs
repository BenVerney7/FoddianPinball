using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform ball;

    public float offset;
    public float cameraBoundary;
    public float smoothTime;

    public float vel = 1f;

    public float camPosPlusOffset;
    public float ballPos;
    public float distanceFromCameraToTarget;

    Vector3 startPos;

    float lowerLimit = 4f;
    float upperLimit = -5f;

    private void Start() {
        startPos = transform.position;
    }


    void LateUpdate() {

        camPosPlusOffset = transform.position.z + offset;
        ballPos = ball.position.z; 
        distanceFromCameraToTarget =camPosPlusOffset - ballPos;
        
        
        //>4.5 need to tightly follow down
        //<-2 need to slow go up
        //<-4.5 tightly go up


        if(distanceFromCameraToTarget < lowerLimit && distanceFromCameraToTarget > -2) {
            //vel = 0;
            SlowFollow();
        } else if(distanceFromCameraToTarget > lowerLimit) {
            CloseFollowDown();
        } else if(distanceFromCameraToTarget < upperLimit) {
            CloseFollowUp(); //can get stuck here
        } else {
            SmoothFollow();
        }

    } 




    void SmoothFollow() {
        float zPos = Mathf.SmoothDamp(transform.position.z, ball.position.z - offset, ref vel, smoothTime);

        if (zPos <= startPos.z) {
            vel = 0f;
            return; //dont move camera below start z
        }

        //float zPos = Mathf.Lerp(transform.position.z , ball.position.z + offset, .3f * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
    }

    void SlowFollow() {
        float zPos = Mathf.SmoothDamp(transform.position.z, ball.position.z - offset + lowerLimit, ref vel, 4f);

     
        transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
    }

    void CloseFollowUp() {
        
        float zPos = Mathf.Lerp(transform.position.z, ball.position.z - offset + upperLimit, 10f * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
    }
    //IF BALL IS MOVING FAST THEN INCREASE THE CAMERA DIST > 4.5
    void CloseFollowDown() {
        float zPos = Mathf.Lerp(transform.position.z, ball.position.z - offset + lowerLimit, 10f * Time.deltaTime);

        if (zPos <= startPos.z) {
            return; //dont move camera below start z
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
    }
}
