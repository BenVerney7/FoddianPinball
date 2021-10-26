using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperScript : MonoBehaviour {
    public bool isRightFlipper;
    public int startPosition;
    public int endPosition;
    public float power;
    public float damper;



    HingeJoint hinge;

    public Transform anchor;
    JointSpring newSpring;
    int polarity;
    public bool isFlipping;
    Rigidbody rb;

    public List<GameObject> recentlyHitBalls = new List<GameObject>();


    public bool test;
    public float angle;

    void Start() {
        hinge = GetComponent<HingeJoint>();
        rb = GetComponent<Rigidbody>();

        polarity = isRightFlipper ? -1 : 1;
        test = false;

        //Create Spring
        hinge.useSpring = true;
        newSpring = new JointSpring();
        newSpring.spring = power;
        newSpring.damper = damper;
        newSpring.targetPosition = startPosition;
        hinge.spring = newSpring;

        //Set Limits
        hinge.useLimits = true;
        JointLimits limits = new JointLimits();
        limits.min = startPosition;
        limits.max = endPosition * polarity;

        hinge.limits = limits;

    }

    //The left flipper will be at 115-120 degrees with a swing of 55-60 degrees.



    void Update() {
        if (Input.GetButton("Submit")){
            test = test ? false : true;
        }

        angle = hinge.angle;

        if (Input.GetButton("LeftFlip") && !isRightFlipper) {
            newSpring.targetPosition = endPosition;
            if (!(Mathf.Abs(hinge.angle) > (Mathf.Abs(endPosition) - 3))) {
                isFlipping = true;
            } else {
                isFlipping = false;
            }
        } else if (Input.GetButton("RightFlip") && isRightFlipper) {
            newSpring.targetPosition = -endPosition;
            if (!(Mathf.Abs(hinge.angle) > (Mathf.Abs(endPosition) - 3))){ 
                isFlipping = true;
            } else {
                isFlipping = false;
            }
            
        } else if(Mathf.Abs(hinge.angle) > (Mathf.Abs(endPosition) - 3)){
            newSpring.targetPosition = startPosition;
            
            isFlipping = false;
        }

        hinge.spring = newSpring;

    }

   //MAY NEED TO CHANGE FLIPPER FRICTION
    
    //ADD ANGULAR VEL

    private void OnCollisionEnter(Collision collision) {
        FlipperPhysicsColision(collision);
    }
    
    private void OnCollisionStay(Collision collision) {
        FlipperPhysicsColision(collision);
    }



    IEnumerator RecentlyHitBallTimer(GameObject ball) {
        yield return new WaitForSeconds(.1f);
        recentlyHitBalls.Remove(ball);
    }



    void FlipperPhysicsColision(Collision collision) {
        if ((collision.gameObject.tag == "Ball" && isFlipping)) {
            if (recentlyHitBalls.Contains(collision.gameObject)) {
                return;
            }

            Vector3 flipperToBall = ((collision.transform.position - anchor.position).normalized) /* * 10f*/;
            

            //collision.rigidbody.AddForce(flipperToBall, ForceMode.Impulse);
            collision.rigidbody.velocity = (flipperToBall + collision.rigidbody.velocity.normalized).normalized * collision.rigidbody.velocity.magnitude;
            //ADD FLIPPER ANGULAR VEL HERE???


            recentlyHitBalls.Add(collision.gameObject);
            StartCoroutine(RecentlyHitBallTimer(collision.gameObject));
            Debug.DrawLine(anchor.position, collision.transform.position, Color.red, 3f);
            Debug.DrawRay(anchor.position, flipperToBall, Color.blue, 3.5f);
            
        }
    }




    //dumb shit trying to make physics better that didnt work at all
    /*
    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Ball" && isFlipping) {
            collision.gameObject.transform.SetParent(gameObject.transform);
            hitBalls.Add(collision.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Ball" && isFlipping) {
            collision.rigidbody.velocity += rb.velocity;
            collision.rigidbody.angularVelocity += rb.angularVelocity;
        }
    }
    private void OnCollisionEnter(Collision collision) {
        foreach (ContactPoint point in collision.contacts) {
            print("Force: " + -1 * point.normal * rb.velocity.magnitude);
            point.otherCollider.attachedRigidbody.AddForce(-1 * point.normal * rb.velocity.magnitude, ForceMode.Impulse);
        }
    }

    void RemoveParentedBalls() {
        foreach (GameObject ball in hitBalls.ToArray()) {
            ball.transform.SetParent(null);
            hitBalls.Remove(ball);
        }
    }
    */
}
