using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image thrust;
    public Image fuel;

    public Image attitudeIndicator;
    public LanderController landerController;

    public PhysicsData data;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        data = landerController.GetPhysicsData();
        thrust.fillAmount = landerController.throttle;


        // Over max fuel mass in end, temporary number
        fuel.fillAmount = landerController.currentFuelMass / 816;

        attitudeIndicator.transform.rotation = Quaternion.Euler(0, 0, -1 * data.degreesRotated + 90);
    }
}
