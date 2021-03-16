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
    public float degreesRotated;
    


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
            // Pull the rotation of the lander and store it is degreesRotated
            degreesRotated = (body.transform.localRotation.z);
            //Create a vector to represent thrust based on the lander's rotation
            thrustVector = new Vector3(Mathf.Sin(degreesRotated) * -1, Mathf.Cos(degreesRotated), 0);
            //Apply the thrust using the vector created
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
