﻿using UnityEngine;
using static GameSceneManager;

public class LanderController : MonoBehaviour
{

    public Rigidbody2D body;
    public int thrust;
    public float gravity;
    public float torque;

    public float setFuelLevel;
    public float maxFuelMass;
    public float currentFuelMass;
    public float burnRate;

    public float throttle;

    public float drymass;

    public Vector2 originalPos;

    private Vector2 previousVelocity;
    private float previousAngularVelocity;

    private PhysicsData record;

    private float throttleMax = 1f;
    private float throttleMin = 0f;
    private float throttleInc = 0.001f;

    private bool thrusterEnabled = true;

    private const float safeLandingMaxSpeed = 30.0f;
    private const float hardLandingMaxSpeed = 60.0f;

    private float internalRotation;

    public SimulationMode simMode;

    private const float arcadeModeRotationSpeed = 50f;

    public SpriteRenderer sprite;


    // Start is called before the first frame update
    void Start()
    {
        record = new PhysicsData();
        originalPos = new Vector2(body.position.x, body.position.y);
        thrusterEnabled = true;
    }

    // Fixed update is called every physics step
    void FixedUpdate()
    {
        initializeTimestepVariables();

        if (currentFuelMass <= 0)
        {
            throttle = 0;
            currentFuelMass = 0;
            thrusterEnabled = false;
        }
        if (thrusterEnabled)
        {
            // Transform the rotation to a Vector2
            Vector2 rotationVector = new Vector2(Mathf.Cos(Mathf.Deg2Rad * internalRotation) * -1, Mathf.Sin(Mathf.Deg2Rad * internalRotation));

            // Apply the thrust using the vector created
            Vector2 thrustVector = rotationVector * thrust * Time.deltaTime * throttle;
            body.AddForce(thrustVector, ForceMode2D.Impulse);

            // Ensure the thrust from the thruster is applied to the net force
            record.netForce += thrustVector;

            currentFuelMass -= burnRate * throttle * Time.deltaTime;
            currentFuelMass = Mathf.Max(currentFuelMass, 0);
            body.mass = drymass + currentFuelMass;
        }


        recordPostPhysicsVariables();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D targetObj)
    {
        // If we handle collisions like this, make sure to correctly assign tags to
        // the good/bad landing zone objects
        if (targetObj.gameObject.tag == "FlatTerrain")
        {
            float velocity = targetObj.relativeVelocity.magnitude;
            // Check if velocity/orientation are good
            if (velocity <= safeLandingMaxSpeed)
            {
                //soft landing
                Debug.Log("Soft landing @ speed: " + velocity);
            }
            else if (velocity < hardLandingMaxSpeed)
            {
                //Hard Landing
                Debug.Log("Hard landing @ speed: " + velocity);
            }
            else
            {
                // Crashed, reset position
                ResetLander();
            }

        }

        if (targetObj.gameObject.tag == "CrashTerrain")
        {
            ResetLander();
        }
    }

    public PhysicsData GetPhysicsData()
    {
        return record;
    }

    private void initializeTimestepVariables()
    {
        // Add affects of gravity
        Vector2 gravForce = Vector2.down * gravity * body.mass;
        body.AddForce(gravForce * Time.deltaTime, ForceMode2D.Impulse);

        // Set initial values for variables to display
        record.netForce = Vector3.down * gravity;
        record.netTorque = 0;
        record.velocity = body.velocity;
        record.angularVelocity = body.angularVelocity * Mathf.Rad2Deg;
        record.altitude = body.position.y - (body.transform.localScale.y / 2);


        // Calculate the number of degrees from vertical the sprite has rotated
        record.degreesRotated = -1 * (body.rotation - 90);

        internalRotation = Mathf.Round( (-1 * (body.rotation - 90)) / 5 ) * 5;
        record.internalRotation = internalRotation;

        sprite.transform.rotation = Quaternion.AngleAxis(Mathf.Round(body.rotation / 5) * 5, Vector3.forward);
        sprite.transform.position = this.transform.position;
    }

    private void recordPostPhysicsVariables()
    {

        // Calculate instantaneous acceleration, and store current velocity for next frame
        record.acceleration = (body.velocity - previousVelocity) / Time.deltaTime;
        previousVelocity = body.velocity;

        // Same for instantaneous angular acceleration
        float angularAccelVector = (body.angularVelocity * Mathf.Rad2Deg - previousAngularVelocity * Mathf.Rad2Deg) / Time.deltaTime;
        record.angularAcceleration = angularAccelVector;
        previousAngularVelocity = body.angularVelocity;

    }

    public void IncreaseThrottle()
    {
        if (throttle < throttleMax)
        {
            throttle += throttleInc;
        }
        if (throttle >= throttleMax)
        {
            throttle = throttleMax;
        }
    }
    public void DecreaseThrottle()
    {
        if (throttle > throttleMin)
        {
            throttle -= throttleInc;
        }
        if (throttle <= throttleMin)
        {
            throttle = throttleMin;
        }
    }

    public void RotateLeft()
    {
        // Rotate counterclockwise in the Z plane here
        if (simMode == SimulationMode.FullPhys)
        {
            record.netTorque = torque;
            body.AddTorque(record.netTorque, ForceMode2D.Impulse);
        }
        else // arcade mode
        {
            body.rotation += arcadeModeRotationSpeed * Time.deltaTime;
        }
    }

    public void RotateRight()
    {
        // Rotate clockwise in the Z plane here
        if (simMode == SimulationMode.FullPhys)
        {
            record.netTorque = torque * -1;
            body.AddTorque(record.netTorque, ForceMode2D.Impulse);
        }
        else // arcade mode
        {
            body.rotation -= arcadeModeRotationSpeed * Time.deltaTime;
        }
    }

    public void ToggleThruster()
    {
        thrusterEnabled = !thrusterEnabled;
    }

    public void ResetLander()
    {
        // Crashed, reset position
        body.position = originalPos;
        body.rotation = 0;
        body.velocity = Vector2.zero;
        body.angularVelocity = 0;
        currentFuelMass = setFuelLevel;
    }
}

public class PhysicsData
{
    public float degreesRotated;
    public float torque;

    public Vector2 velocity;
    public float angularVelocity;

    public Vector2 netForce;
    public float netTorque;
    public Vector2 acceleration;
    public float angularAcceleration;

    public float altitude;

    public float internalRotation;
}
