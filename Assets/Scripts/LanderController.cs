using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LanderController : MonoBehaviour
{

    public Rigidbody body;
    public int thrust;
    public float gravity;
    public Vector3 thrustVector;
    


    // Start is called before the first frame update
    void Start()
    {

    }

    // Fixed update is called every physics step
    void FixedUpdate()
    {
        // Add affects of gravity
        body.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        // If the up arrow is down, apply an impulse this timestep
        if(Input.GetKey(KeyCode.UpArrow))
        {
            // Thrust is newtons, multiply by time since last physics step to get newton-seconds
            //body.AddForce(Vector3.up * thrust * Time.deltaTime, ForceMode.Impulse);
            float degreesRotated = (body.transform.localRotation.z);
            thrustVector = new Vector3(Mathf.Sin(degreesRotated), Mathf.Cos(degreesRotated), 0);
            body.AddForce(thrustVector * thrust * Time.deltaTime, ForceMode.Impulse);

            //body.transform.LocalRotation.z
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Rotate left in the Z plane here
            body.transform.Rotate(0, 0, 2);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            // Rotate right in the Z plane here
            body.transform.Rotate(0, 0, -2);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision targetObj)
    {
        // If we handle collisions like this, make sure to correctly assign tags to
        // the good/bad landing zone objects

        if (targetObj.gameObject.tag == "Goal")
        {
            // Check if velocity/orientation are good
        }
        if (targetObj.gameObject.tag == "Hazard")
        {
            // Crashed
        }
    }
}
