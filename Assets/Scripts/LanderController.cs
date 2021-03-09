using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LanderController : MonoBehaviour
{

    public Rigidbody lander;
    [Header("Thrust in newtons")]
    public int thrust;
    [Header("Experienced gravity in m/s/s")]
    public float gravity;
    
    private bool isPaused = false;


    // Start is called before the first frame update
    void Start()
    {
        lander.drag = 0;
    }

    // Fixed update is called every physics step
    void FixedUpdate()
    {
        // Add affects of gravity
        lander.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        // If the up arrow is down, apply an impulse this timestep
        if(Input.GetKey(KeyCode.UpArrow))
        {
            // Thrust is newtons, multiply by time since last physics step to get newton-seconds
            lander.AddForce(Vector3.up * thrust * Time.deltaTime, ForceMode.Impulse);
        }


    }

    // Update is called once per frame
    void Update()
    {
        // If the space key was pressed this frame
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // Pause or unpause
            isPaused = !isPaused;
            if(isPaused)
            {
                Time.timeScale = 0;
            }
            else
            {
                //Can probably put fudge factor here, set time to like 0.8f instead of 1
                Time.timeScale = 1;
            }
        }
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
