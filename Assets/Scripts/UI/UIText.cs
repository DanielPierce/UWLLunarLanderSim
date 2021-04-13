using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIText : MonoBehaviour
{

  public LanderController lander;
  public RectTransform myUI;

  private PhysicsData physicsData;

  private float _x;
  private float _y;

  private GUIStyle gui = new GUIStyle();
  void Start()
  {
    gui.fontSize = 24;
    gui.normal.textColor = Color.white;
  }

  void Update()
  {
    physicsData = lander.GetPhysicsData();

  }

  void OnGUI()
  {
    GUI.Label(new Rect(80, 65, 200, 20), "" + Mathf.Round(100 * lander.throttle), gui);
    GUI.Label(new Rect(220, 65, 200, 20), "" + Mathf.Round(lander.currentFuelMass), gui);
    GUI.Label(new Rect(115, 220, 200, 20), "X: " + Mathf.Round(physicsData.velocity.x) + "   Y:" + Mathf.Round(physicsData.velocity.y), gui);
    GUI.Label(new Rect(115, 380, 200, 20), "X: " + Mathf.Round(physicsData.acceleration.x) + "   Y:" + Mathf.Round(physicsData.acceleration.y), gui);
  }
}
