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
        crashPopup.HidePopup();
        offScreenPopup.HidePopup();
    }
    public void OnSoftLanding(float velocity)
    {
        softLandingPopup.DisplayPopup("Soft landing at " + velocity + " m/s");
    }
    public void OnHardLanding(float velocity)
    {
        hardLandingPopup.DisplayPopup("Hard landing at " + velocity + " m/s");
    }
    public void OnCrashLanding(float velocity)
    {
        crashPopup.DisplayPopup("Crash landing at " + velocity + " m/s");
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
