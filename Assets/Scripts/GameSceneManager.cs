﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    // Start is called before the first frame update

    public LanderController lander;

    public Image crashLanding;
    public enum Scenario { Moon, Mars };
    public enum SimulationMode { Arcade, FullPhys}

    public Scenario currentLocation = Scenario.Moon;
    public SimulationMode simMode = SimulationMode.Arcade;

    private PhysicsData landerData = null;


    private bool isPaused = false;

    void Start()
    {
        lander.body.gravityScale = 0;
        if(currentLocation == Scenario.Moon)
        {
            lander.thrust = 45040; // N
            lander.gravity = 1.62f; // m/s/s
            lander.body.drag = 0;
            lander.body.angularDrag = 0;
            lander.drymass = 6839; // kg
            lander.maxFuelMass = 8165; // kg
            lander.setFuelLevel = lander.maxFuelMass * 0.1f; // Start with 10% fuel remaining
            lander.currentFuelMass = lander.setFuelLevel;
            lander.burnRate = 14.75f; // kg/s
            lander.torque = 150;
            // Can add additional values here like fuel levels, dry mass, etc
        }
        else if(currentLocation == Scenario.Mars)
        {
            lander.gravity = 3.72f; // m/s/s
            // Will have to update these for martian conditions
        }
        //others scenarios?
        if(simMode == SimulationMode.Arcade)
        {
            lander.body.angularDrag = 100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        landerData = lander.GetPhysicsData();
        HandleLanderThrottle();
        // If the space key was pressed this frame
        if (Input.GetKeyDown(KeyCode.P))
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

                crashLanding.enabled = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            lander.ToggleThruster();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            lander.ResetLander();
            crashLanding.enabled = false;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 250, 20), "Net force:    " + landerData.netForce);
        GUI.Label(new Rect(20, 40, 250, 20), "Net torque:   " + landerData.netTorque);
        GUI.Label(new Rect(20, 60, 250, 20), "Angular vel:  " + landerData.angularVelocity);
        GUI.Label(new Rect(20, 80, 250, 20), "Angular acc:  " + landerData.angularAcceleration);
        GUI.Label(new Rect(20, 100, 250, 20), "Velocity:     " + landerData.velocity);
        GUI.Label(new Rect(20, 120, 250, 20), "Acceleration: " + landerData.acceleration);
        GUI.Label(new Rect(20, 140, 250, 20), "Altitude:     " + landerData.altitude);
        GUI.Label(new Rect(20, 160, 250, 20), "Rotation:     " + landerData.degreesRotated);
        GUI.Label(new Rect(20, 180, 250, 20), "Internal Rot: " + landerData.internalRotation);
        GUI.Label(new Rect(20, 200, 250, 20), "Thrust force: " + landerData.thrustForce);
        GUI.Label(new Rect(20, 240, 250, 20), "Current fuel: " + lander.currentFuelMass);
        GUI.Label(new Rect(20, 260, 250, 20), "Throttle:     " + lander.throttle);
    }

    void FixedUpdate()
    {
        HandleLanderRotation();
    }

    void HandleLanderThrottle()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            lander.IncreaseThrottle();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            lander.DecreaseThrottle();
        }
    }

    void HandleLanderRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            lander.RotateLeft();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            lander.RotateRight();
        }
    }
}
