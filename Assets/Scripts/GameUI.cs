﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image thrust;
    public Image fuel;
    public LanderController landerController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        thrust.fillAmount = landerController.throttle;


        // Over max fuel mass in end, temporary number
        fuel.fillAmount = landerController.currentFuelMass / 816;
    }
}
