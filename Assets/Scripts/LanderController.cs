using UnityEngine;

public class LanderController : MonoBehaviour
{

    public Rigidbody body;
    public int thrust;
    public float gravity;
    public float torque;

    public float maxFuelMass;
    public float currentFuelMass;
    public float burnRate;

    public float throttle;

    public float drymass;

    private Vector3 previousVelocity;
    private Vector3 previousAngularVelocity;

    private PhysicsData record;

    // Start is called before the first frame update
    void Start()
    {
        record = new PhysicsData();
    }

    // Fixed update is called every physics step
    void FixedUpdate()
    {
        initializeTimestepVariables();

        // Transform the rotation to a Vector2
        Vector2 rotationVector = new Vector2(Mathf.Cos(Mathf.Deg2Rad * record.degreesRotated) * -1, Mathf.Sin(Mathf.Deg2Rad * record.degreesRotated));

        throttle = 0;
        // If the up arrow is down, apply an impulse this timestep
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if(currentFuelMass > 0)
            {
                throttle = 1;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Rotate counterclockwise in the Z plane here
            record.netTorque = torque;
            body.AddTorque(Vector3.forward * record.netTorque, ForceMode.Impulse);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            // Rotate clockwise in the Z plane here
            record.netTorque = torque * -1;
            body.AddTorque(Vector3.forward * record.netTorque, ForceMode.Impulse);
        }

        // Apply the thrust using the vector created
        Vector2 thrustVector = rotationVector * thrust * Time.deltaTime * throttle;
        body.AddForce(thrustVector, ForceMode.Impulse);
        // Ensure the thrust from the thruster is applied to the net force
        record.netForce += thrustVector;

        currentFuelMass -= burnRate * throttle * Time.deltaTime;
        currentFuelMass = Mathf.Max(currentFuelMass, 0);
        body.mass = drymass + currentFuelMass;

        recordPostPhysicsVariables();
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

    public PhysicsData GetPhysicsData()
    {
        return record;
    }

    private void initializeTimestepVariables()
    {
        // Add affects of gravity
        body.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        // Set initial values for variables to display
        record.netForce = Vector3.down * gravity;
        record.netTorque = 0;
        record.velocity = body.velocity;
        record.angularVelocity = body.angularVelocity.z * Mathf.Rad2Deg;
        record.altitude = body.position.y;


        // Calculate the number of degrees from vertical the sprite has rotated
        record.degreesRotated = -1 * (body.rotation.eulerAngles.z - 90);
    }

    private void recordPostPhysicsVariables()
    {

        // Calculate instantaneous acceleration, and store current velocity for next frame
        record.acceleration = (body.velocity - previousVelocity) / Time.deltaTime;
        previousVelocity = body.velocity;

        // Same for instantaneous angular acceleration
        Vector3 angularAccelVector = (body.angularVelocity * Mathf.Rad2Deg - previousAngularVelocity * Mathf.Rad2Deg) / Time.deltaTime;
        record.angularAcceleration = angularAccelVector.z;
        previousAngularVelocity = body.angularVelocity;

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
}
