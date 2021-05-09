using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public Image thrust;
    public Image fuel;
    public Image attitude;

    public DisplayVector diagonalV;
    public DisplayVector verticalV;
    public DisplayVector horizontalV;

    public DisplayVector diagonalA;
    public DisplayVector verticalA;
    public DisplayVector horizontalA;

    public float velMax = 60;
    public float accelMax = 25;
    public float maxOut = 100;


    public TMP_Text velocityXText;
    public TMP_Text velocityYText;
    public TMP_Text accelXText;
    public TMP_Text accelYText;

    public TMP_Text altitudeText;
    public TMP_Text contactText;
    public TMP_Text engineStatusText;

    public TMP_Text throttleText;
    public TMP_Text fuelText;

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

        diagonalV.MaxOut = maxOut;
        verticalV.MaxOut = maxOut;
        horizontalV.MaxOut = maxOut;

        diagonalA.MaxOut = maxOut;
        verticalA.MaxOut = maxOut;
        horizontalA.MaxOut = maxOut;
    }

    // Update is called once per frame
    void Update()
    {
        // Get updated physics data
        physicsData = landerController.GetPhysicsData();

        // UI Texts
        velocityXText.text = System.Math.Round(physicsData.velocity.x, 1).ToString() + "m/s";
        velocityYText.text = System.Math.Round(physicsData.velocity.y, 1).ToString() + "m/s";
        accelXText.text = System.Math.Round(physicsData.acceleration.x, 1).ToString() + "m/s/s";
        accelYText.text = System.Math.Round(physicsData.acceleration.y, 1).ToString() + "m/s/s";

        altitudeText.text = System.Math.Round(physicsData.altitude, 1).ToString() + "m";
        contactText.text = landerController.IsLanded() ? "Contact" : "In Flight";
        engineStatusText.text = landerController.thrusterEnabled ? "On" : "Off";

        throttleText.text = System.Math.Round(landerController.throttle * (float)landerController.thrust).ToString() + "N";
        fuelText.text = System.Math.Round(landerController.currentFuelMass, 1).ToString() + "kg";

        // Fillable bars
        thrust.fillAmount = landerController.throttle;
        if (landerController.thrusterEnabled) {
          thrust.color = new Color(0, 38, 255, 255);
        } else  {
          thrust.color = new Color(255, 0, 28, 255);
        }

        // Over max fuel mass in end, temporary number
        fuel.fillAmount = landerController.currentFuelMass / 816;


        // Attitude Indicator
        attitude.transform.rotation = Quaternion.Euler(0, 0, -1 * physicsData.degreesRotated + 90);

        diagonalV.displayVector = physicsData.velocity;
        horizontalV.displayVector = physicsData.velocity * Vector2.right;
        verticalV.displayVector = physicsData.velocity * Vector2.up;

        diagonalA.displayVector = physicsData.acceleration;
        horizontalA.displayVector = physicsData.acceleration * Vector2.right;
        verticalA.displayVector = physicsData.acceleration * Vector2.up;

        // Scalable vectors
        if (landerController.IsLanded())
        {
            diagonalV.displayVector = Vector2.zero;
            horizontalV.displayVector = Vector2.zero;
            verticalV.displayVector = Vector2.zero;

            diagonalA.displayVector = Vector2.zero;
            horizontalA.displayVector = Vector2.zero;
            verticalA.displayVector = Vector2.zero;
        }
    }
}
