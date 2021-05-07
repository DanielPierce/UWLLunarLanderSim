using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
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

    protected Vector2 previousVelocity;
    protected float previousAngularVelocity;

    protected PhysicsData record;

    protected float throttleMax = 1f;
    protected float throttleMin = 0f;
    protected float throttleInc = 0.003f;

    public bool thrusterEnabled = true;

    protected const float safeLandingMaxSpeed = 3.0f;
    protected const float hardLandingMaxSpeed = 6.0f;
    protected const float safeLandingAngle = 15.0f;
    protected const float hardLandingAngle = 45.0f;

    protected float internalRotation;

    public SpriteRenderer sprite;
    public Animator animator;

    public GameSceneManager gameSceneManager;

    public Collider2D landerCollider;
    private Collider2D mostRecentCollision;

    private bool isRotationApplied;

    public float sectimer = 5.0f;

    public UnityEvent CamZone1Event;
    public UnityEvent CamZone2Event;
    public UnityEvent CamZone3Event;
    public UnityEvent LeaveCamZoneEvent;

    // Start is called before the first frame update
    public virtual void Start()
    {
        record = new PhysicsData();
        originalPos = new Vector2(body.position.x, body.position.y);
        thrusterEnabled = true;
    }

    // Fixed update is called every physics step
    public virtual void FixedUpdate()
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
            record.thrustForce = rotationVector * thrust * throttle;
            record.netForce += record.thrustForce;

            currentFuelMass -= burnRate * throttle * Time.deltaTime;
            currentFuelMass = Mathf.Max(currentFuelMass, 0);
            body.mass = drymass + currentFuelMass;

            animator.SetBool("isThrusting", true);
        }
        else
        {
            animator.SetBool("isThrusting", false);
        }

        if (throttle == 0)
        {
            animator.SetBool("isThrusting", false);
        }

        //animator.SetFloat("thrustAmount", throttle);

        recordPostPhysicsVariables();

        OnPhysicsUpdate();

        isRotationApplied = false;

        if (IsLanded() == false)
        {
            sectimer = sectimer - Time.deltaTime;
        }
        else
        {
            sectimer = 5.0f;
        }
    }

    public virtual void OnPhysicsUpdate()
    {
        internalRotation = record.degreesRotated;
        record.internalRotation = internalRotation;

        sprite.transform.rotation = transform.rotation;
        sprite.transform.position = transform.position;

        if (body.angularVelocity != 0 && !isRotationApplied)
        {
            if (body.angularVelocity > 0.0001)
            {
                record.netTorque = torque * -1;
                body.AddTorque(record.netTorque, ForceMode2D.Impulse);
            }
            if (body.angularVelocity < -0.0001)
            {
                record.netTorque = torque;
                body.AddTorque(record.netTorque, ForceMode2D.Impulse);
            }
        }
    }

    public bool IsLanded()
    {
        return mostRecentCollision == null ? false : landerCollider.IsTouching(mostRecentCollision);
    }

    void OnCollisionEnter2D(Collision2D targetObj)
    {
        mostRecentCollision = targetObj.collider;

        if (targetObj.gameObject.tag == "FlatTerrain")
        {
            float velocity = targetObj.relativeVelocity.magnitude;
            float landingAngle = Mathf.Abs(body.rotation) % 360;


            if (sectimer <= 0)
            {
                sectimer = 5.0f;
                if (velocity <= safeLandingMaxSpeed && ((landingAngle >= 0 && landingAngle <= 15) || (landingAngle >= 345 && landingAngle <= 360)))
                {
                    //soft landing
                    Debug.Log("Soft landing @ speed: " + velocity + ", Angle: " + landingAngle);

                    //Add UI popup for soft landing here
                    gameSceneManager.popups.OnSoftLanding(velocity);
                    //Toggle off thrusters
                    gameSceneManager.changePause();
                }
                else if (velocity <= hardLandingMaxSpeed && ((landingAngle >= 0 && landingAngle <= 15) || (landingAngle >= 345 && landingAngle <= 360)))
                {
                    //Hard Landing due to speed
                    Debug.Log("Hard landing (speed) @ speed: " + velocity + ", Angle: " + landingAngle);

                    //Add UI popup for hard landing here
                    gameSceneManager.popups.OnHardLanding(velocity);
                    //Add any damages here(20% fuel loss for now)
                    currentFuelMass = currentFuelMass * 0.8f;

                    //Toggle off thrusters

                    gameSceneManager.changePause();
                }
                else if (velocity <= safeLandingMaxSpeed && ((landingAngle >= 0 && landingAngle <= 45) || (landingAngle >= 315 && landingAngle <= 360)))
                {
                    //Hard Landing due to angle
                    Debug.Log("Hard landing (angle) @ speed: " + velocity + ", Angle: " + landingAngle);

                    //Add UI popup for hard landing here
                    gameSceneManager.popups.OnHardLanding(velocity);
                    //Add any damages here(20% fuel loss for now)
                    currentFuelMass = currentFuelMass * 0.8f;

                    //Toggle off thrusters

                    gameSceneManager.changePause();
                }
                else if (velocity <= hardLandingMaxSpeed && ((landingAngle >= 0 && landingAngle <= 45) || (landingAngle >= 315 && landingAngle <= 360)))
                {
                    //Hard Landing due to both
                    Debug.Log("Hard landing (speed & angle) @ speed: " + velocity + ", Angle: " + landingAngle);

                    //Add UI popup for hard landing here
                    gameSceneManager.popups.OnHardLanding(velocity);
                    //Add any damages here(20% fuel loss for now)
                    currentFuelMass = currentFuelMass * 0.8f;

                    //Toggle off thrusters

                    gameSceneManager.changePause();
                }
                else
                {
                    Debug.Log("Crashed @ speed: " + velocity + ", Angle: " + landingAngle);
                    //Crashed

                    //Add UI popup for crashing here
                    gameSceneManager.popups.OnCrashLanding(velocity);
                    //Drain remaining fuel (for now) to remove lander's ability to fly
                    throttle = 0;
                    currentFuelMass = 0;
                }
            }
        }

        if (targetObj.gameObject.tag == "CrashTerrain")
        {
            float velocity = targetObj.relativeVelocity.magnitude;
            throttle = 0;
            currentFuelMass = 0;
            gameSceneManager.popups.OnCrashLanding(velocity);
            Debug.Log("hit crash terrain");
        }

        thrusterEnabled = false;
        animator.SetBool("isThrusting", false);
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "MapBorder")
        {
            throttle = 0;
            currentFuelMass = 0;

            Debug.Log("flew off of the screen");

            //Add UI popup for flying off screen here
            gameSceneManager.popups.offScreenPopup.DisplayPopup();
        }
        else if (obj.gameObject.tag == "ZoneCameraCollider")
        {
            Debug.Log("Collider " + obj.name.ToCharArray()[4]);
            switch (obj.name.ToCharArray()[4])
            {
                case '1':
                    CamZone1Event.Invoke();
                    break;
                case '2':
                    CamZone2Event.Invoke();
                    break;
                case '3':
                    CamZone3Event.Invoke();
                    break;
            }
        }
    }
    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.tag == "ZoneCameraCollider")
        {
            LeaveCamZoneEvent.Invoke();
        }
    }

    public PhysicsData GetPhysicsData()
    {
        return record;
    }

    protected virtual void initializeTimestepVariables()
    {

        // Add affects of gravity
        Vector2 gravForce = Vector2.down * gravity * body.mass;
        body.AddForce(gravForce * Time.deltaTime, ForceMode2D.Impulse);

        // Set initial values for variables to display
        record.netForce = gravForce;
        record.netTorque = 0;
        record.velocity = body.velocity;
        record.angularVelocity = body.angularVelocity * Mathf.Rad2Deg;
        record.altitude = body.position.y - (body.transform.localScale.y / 2);


        // Calculate the number of degrees from vertical the sprite has rotated
        record.degreesRotated = -1 * (body.rotation - 90);
    }

    protected void recordPostPhysicsVariables()
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

    public virtual void RotateLeft()
    {
        // Rotate counterclockwise in the Z plane here
        record.netTorque = torque;
        body.AddTorque(record.netTorque, ForceMode2D.Impulse);
        isRotationApplied = true;
    }

    public virtual void RotateRight()
    {
        // Rotate clockwise in the Z plane here
        record.netTorque = torque * -1;
        body.AddTorque(record.netTorque, ForceMode2D.Impulse);
        isRotationApplied = true;
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
    public Vector2 thrustForce;
    public float netTorque;
    public Vector2 acceleration;
    public float angularAcceleration;

    public float altitude;

    public float internalRotation;
}
