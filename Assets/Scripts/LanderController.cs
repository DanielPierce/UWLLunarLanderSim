using UnityEngine;

public class LanderController : MonoBehaviour
{

    public Rigidbody body;
    public int thrust;
    public float gravity;
    public Vector2 rotationVector;
    public float degreesRotated;
    public float torque;

    public Vector2 netForce;
    public float netTorque;
    public Vector2 acceleration;
    public float angularAcceleration;

    private Vector3 previousVelocity;
    private Vector3 previousAngularVelocity;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Fixed update is called every physics step
    void FixedUpdate()
    {
        // Add affects of gravity
        body.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        // Set initial values for variables to display
        netForce = Vector3.down * gravity;
        netTorque = 0;
        // Velocity vector can be read from body.velocity
        // Angular velocity can be read from body.angularVelocity.z (though this is radians/s)
        // Altitude can be read from body.position.y
        
        // Calculate the number of degrees from vertical the sprite has rotated
        degreesRotated = -1 * (body.rotation.eulerAngles.z - 90);
        // Transform the rotation to a Vector2
        rotationVector = new Vector2(Mathf.Cos(Mathf.Deg2Rad * degreesRotated) * -1, Mathf.Sin(Mathf.Deg2Rad * degreesRotated));

        // If the up arrow is down, apply an impulse this timestep
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // Apply the thrust using the vector created
            Vector2 thrustVector = rotationVector * thrust * Time.deltaTime;
            body.AddForce(thrustVector, ForceMode.Impulse);

            // Ensure the thrust from the thruster is applied to the net force
            netForce += thrustVector;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Rotate counterclockwise in the Z plane here
            netTorque = torque;
            body.AddTorque(Vector3.forward * netTorque, ForceMode.Impulse);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            // Rotate clockwise in the Z plane here
            netTorque = torque * -1;
            body.AddTorque(Vector3.forward * netTorque, ForceMode.Impulse);
        }

        // Calculate instantaneous acceleration, and store current velocity for next frame
        acceleration = (body.velocity - previousVelocity) / Time.deltaTime;
        previousVelocity = body.velocity;

        // Same for instantaneous angular acceleration
        Vector3 angularAccelVector = (body.angularVelocity - previousAngularVelocity) / Time.deltaTime;
        angularAcceleration = angularAccelVector.z;
        previousAngularVelocity = body.angularVelocity;
        // Since angularVelocity is rads/s, angularAcceleration will be rads/s/s

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
