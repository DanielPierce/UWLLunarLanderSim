using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera MainCamera;
    public Camera Zone1Cam;
    public Camera Zone2Cam;
    public Camera Zone3Cam;

    public Canvas UI;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCam1()
    {
        UI.worldCamera = Zone1Cam;
        MainCamera.enabled = false;
        Zone1Cam.enabled = true;
        Zone2Cam.enabled = false;
        Zone3Cam.enabled = false;
    }
    public void OnCam2()
    {
        UI.worldCamera = Zone1Cam;
        MainCamera.enabled = false;
        Zone1Cam.enabled = false;
        Zone2Cam.enabled = true;
        Zone3Cam.enabled = false;
    }
    public void OnCam3()
    {
        UI.worldCamera = Zone1Cam;
        MainCamera.enabled = false;
        Zone1Cam.enabled = false;
        Zone2Cam.enabled = false;
        Zone3Cam.enabled = true;
    }
    public void OnMainCam()
    {
        UI.worldCamera = Zone1Cam;
        MainCamera.enabled = true;
        Zone1Cam.enabled = false;
        Zone2Cam.enabled = false;
        Zone3Cam.enabled = false;
    }
}
