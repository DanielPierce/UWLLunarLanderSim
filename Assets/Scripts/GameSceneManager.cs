using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    // Start is called before the first frame update

    public LanderController lander;
    public enum Scenario { Moon, Mars };

    public Scenario currentLocation = Scenario.Moon;

    private PhysicsData landerData = null;


    private bool isPaused = false;

    void Start()
    {
        lander.body.useGravity = false;
        if(currentLocation == Scenario.Moon)
        {
            lander.thrust = 45040; // N
            lander.gravity = 1.62f; // m/s/s
            lander.body.drag = 0;
            lander.body.angularDrag = 0;
            lander.body.mass = 15200; // kg
            lander.torque = 50;
            // Can add additional values here like fuel levels, dry mass, etc
        }
        else if(currentLocation == Scenario.Mars)
        {
            lander.thrust = 45040; // N
            lander.gravity = 3.72f; // m/s/s
            lander.body.drag = 0;
            lander.body.angularDrag = 0;
            lander.body.mass = 15200; // kg
            lander.torque = 50;
            // Will have to update these for martian conditions
        }
        //others scenarios?
    }

    // Update is called once per frame
    void Update()
    {
        landerData = lander.GetPhysicsData();
        // If the space key was pressed this frame
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Pause or unpause
            isPaused = !isPaused;
            if (isPaused)
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
}
