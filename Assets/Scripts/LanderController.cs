using UnityEngine;

public class LanderController : MonoBehaviour
{

    public Rigidbody2D body;
    public int thrust;
    public float gravity;
    public float torque;

    public float maxFuelMass;
    public float currentFuelMass;
    public float burnRate;

    public float throttle;

    public float drymass;

    public Vector3 originalPos;

    private Vector2 previousVelocity;
    private float previousAngularVelocity;

    private PhysicsData record;

    // Start is called before the first frame update
    void Start()
    {
        record = new PhysicsData();
        originalPos = new Vector3(body.transform.position.x, body.transform.position.y, body.transform.position.z);
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
            body.AddTorque(record.netTorque, ForceMode2D.Impulse);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            // Rotate clockwise in the Z plane here
            record.netTorque = torque * -1;
            body.AddTorque(record.netTorque, ForceMode2D.Impulse);
        }

        // Apply the thrust using the vector created
        Vector2 thrustVector = rotationVector * thrust * Time.deltaTime * throttle;
        body.AddForce(thrustVector, ForceMode2D.Impulse);
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

    void OnCollisionEnter2D(Collision2D targetObj)
    {
        // If we handle collisions like this, make sure to correctly assign tags to
        // the good/bad landing zone objects
        if (targetObj.gameObject.tag == "FlatTerrain")
        {
            // Check if velocity/orientation are good
            if (body.velocity.y < 3.0)
            {
                //soft landing
                Debug.Log("Soft Landing detected");
            }
            else if (body.velocity.y < 6.0)
            {
                //Hard Landing
                Debug.Log("Hard Landing detected");
            }
            else
            {
                // Crashed, reset position
                body.transform.position = originalPos;
                Debug.Log("Crash detected");
            }

        }
        if (targetObj.gameObject.tag == "CrashTerrain")
        {
            // Crashed, reset position
            body.transform.position = originalPos;
            Debug.Log("Crash detected");
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
        record.altitude = body.position.y;


        // Calculate the number of degrees from vertical the sprite has rotated
        record.degreesRotated = -1 * (body.rotation - 90);
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
