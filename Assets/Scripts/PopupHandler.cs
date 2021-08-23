using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupHandler : MonoBehaviour
{
    public Popup helpPopup;
    public Popup offScreenPopup;
    public Popup pausePopup;
    public TextPopup softLandingPopup;
    public TextPopup hardLandingPopup;
    public TextPopup crashPopup;


    public void OnResetPressed()
    {
        HideAll();
    }
    public void OnSoftLanding(float velocity)
    {
        HideAll();
        string velocityAsString = string.Format("{0:0.###}", velocity);
        softLandingPopup.DisplayPopup(string.Format("<b>Soft Landing!</b>\nTouchdown at " + velocityAsString + " m/s\nPress 'P' to continue!"));
    }
    public void OnHardLanding(float velocity)
    {
        HideAll();
        string velocityAsString = string.Format("{0:0.###}", velocity);
        hardLandingPopup.DisplayPopup("<b>Hard Landing!</b>\nTouchdown at " + velocityAsString + " m/s\nPress 'P' to continue!");
    }
    public void OnCrashLanding(float velocity)
    {
        HideAll();
        string velocityAsString = string.Format("{0:0.###}", velocity);
        crashPopup.DisplayPopup("<b>Crash!</b>\nTouchdown at " + velocityAsString + " m/s\nPress 'P' to continue!");
        Debug.Log("on crash landing");
    }


    public void HideAll()
    {
        helpPopup.HidePopup();
        offScreenPopup.HidePopup();
        pausePopup.HidePopup();
        softLandingPopup.HidePopup();
        hardLandingPopup.HidePopup();
        crashPopup.HidePopup();
    }
}
