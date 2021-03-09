using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LanderController : MonoBehaviour
{

    public Rigidbody body;
    public int thrust;
    public float gravity;
    


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
            body.AddForce(Vector3.up * thrust * Time.deltaTime, ForceMode.Impulse);
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
