using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image thrust;
    public Image fuel;
    public Image altitude;
    public Image attitude;


    public DisplayVector diagonalV;
    public DisplayVector verticalV;
    public DisplayVector horizontalV;

    public DisplayVector diagonalA;
    public DisplayVector verticalA;
    public DisplayVector horizontalA;

    public float velMax = 60;
    public float accelMax = 25;


    public Text altitudeText;
    public Text velocityXText;
    public Text velocityYText;
    public Text accelXText;
    public Text accelYText;


    public LanderController landerController;

    private PhysicsData physicsData;

    public float vectorScale = 15f;

    public float maxVelo = 3f;
    public float maxAccel = 6f;

    // Start is called before the first frame update
    void Start()
    {
        diagonalV.MaxIn = velMax;
        verticalV.MaxIn = velMax;
        horizontalV.MaxIn = velMax;

        diagonalA.MaxIn = accelMax;
        verticalA.MaxIn = accelMax;
        horizontalA.MaxIn = accelMax;
    }

    // Update is called once per frame
    void Update()
    {
        // Get updated physics data
        physicsData = landerController.GetPhysicsData();

        // UI Texts
        //altitudeText.text = "Alt.: " + System.Math.Round(physicsData.altitude, 1).ToString() + "m";
        //velocityXText.text = "Velo X: " + System.Math.Round(physicsData.velocity.x, 1).ToString() + "m/s";
        //velocityYText.text = "Velo Y: " + System.Math.Round(physicsData.velocity.y, 1).ToString() + "m/s";
        //accelXText.text = "Accel. X: " + System.Math.Round(physicsData.acceleration.x, 1).ToString() + "m/s/s";
        //accelYText.text = "Accel. Y: " + System.Math.Round(physicsData.acceleration.y, 1).ToString() + "m/s/s";

        // Fillable bars
        thrust.fillAmount = landerController.throttle;
        if (landerController.thrusterEnabled) {
          thrust.color = new Color(0, 38, 255, 255);
        } else  {
          thrust.color = new Color(255, 0, 28, 255);
        }

        // Over max fuel mass in end, temporary number
        fuel.fillAmount = landerController.currentFuelMass / 816;

        //altitude.fillAmount = physicsData.altitude / 100;


        // Attitude Indicator
        attitude.transform.rotation = Quaternion.Euler(0, 0, -1 * physicsData.degreesRotated + 90);




        // Scalable vectors
        diagonalV.displayVector = physicsData.velocity;
        horizontalV.displayVector = physicsData.velocity * Vector2.right;
        verticalV.displayVector = physicsData.velocity * Vector2.up;

        diagonalA.displayVector = physicsData.acceleration;
        horizontalA.displayVector = physicsData.acceleration * Vector2.right;
        verticalA.displayVector = physicsData.acceleration * Vector2.up;
    }
}
