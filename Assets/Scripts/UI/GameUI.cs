using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
  public Image thrust;
  public Image fuel;

  public Image upVelocityVector;
  public Image downVelocityVector;

  public Image leftVelocityVector;

  public Image rightVelocityVector;
  public Image upAccelVector;
  public Image downAccelVector;

  public Image leftAccelVector;


  public Image rightAccelVector;

  public LanderController landerController;

  private PhysicsData physicsData;

  public float vectorScale = 15f;

  public float maxVelo = 3f;
  public float maxAccel = 6f;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    // Get updated physics data
    physicsData = landerController.GetPhysicsData();

    // Fillable bars
    thrust.fillAmount = landerController.throttle;

    // Over max fuel mass in end, temporary number
    fuel.fillAmount = landerController.currentFuelMass / 816;


    // Scalable vectors

    // Vertical velocity vectors
    if (physicsData.velocity.y > 0.1)
    {
      upVelocityVector.enabled = true;
      downVelocityVector.enabled = false;
      upVelocityVector.rectTransform.sizeDelta = new Vector2(vectorScale, physicsData.velocity.y * maxVelo);
    }
    else if (physicsData.velocity.y < -0.1)
    {
      downVelocityVector.enabled = true;
      upVelocityVector.enabled = false;
      downVelocityVector.rectTransform.sizeDelta = new Vector2(vectorScale, Mathf.Abs(physicsData.velocity.y * maxVelo));
    }
    else
    {
      downVelocityVector.enabled = false;
      upVelocityVector.enabled = false;
    }
    // Horizontal velocity vectors
    if (physicsData.velocity.x > 0.1)
    {
      rightVelocityVector.enabled = true;
      leftVelocityVector.enabled = false;
      rightVelocityVector.rectTransform.sizeDelta = new Vector2(physicsData.velocity.x * maxVelo, vectorScale);
    }
    else if (physicsData.velocity.x < -0.1)
    {
      leftVelocityVector.enabled = true;
      rightVelocityVector.enabled = false;
      leftVelocityVector.rectTransform.sizeDelta = new Vector2(Mathf.Abs(physicsData.velocity.x * maxVelo), vectorScale);
    }
    else
    {
      leftVelocityVector.enabled = false;
      rightVelocityVector.enabled = false;
    }


    // Vertical accel vectors
    if (physicsData.acceleration.y > 0.1)
    {
      upAccelVector.enabled = true;
      downAccelVector.enabled = false;
      upAccelVector.rectTransform.sizeDelta = new Vector2(vectorScale, physicsData.acceleration.y * maxAccel);
    }
    else if (physicsData.acceleration.y < -0.1)
    {
      downAccelVector.enabled = true;
      upAccelVector.enabled = false;
      downAccelVector.rectTransform.sizeDelta = new Vector2(vectorScale, Mathf.Abs(physicsData.acceleration.y * maxAccel));
    }
    else
    {
      downAccelVector.enabled = false;
      upAccelVector.enabled = false;
    }
    // Horizontal Accel vectors
    if (physicsData.acceleration.x > 0.1)
    {
      rightAccelVector.enabled = true;
      leftAccelVector.enabled = false;
      rightAccelVector.rectTransform.sizeDelta = new Vector2(physicsData.acceleration.x * maxAccel, vectorScale);
    }
    else if (physicsData.acceleration.x < -0.1)
    {
      leftAccelVector.enabled = true;
      rightAccelVector.enabled = false;
      leftAccelVector.rectTransform.sizeDelta = new Vector2(Mathf.Abs(physicsData.acceleration.x * maxAccel), vectorScale);
    }
    else
    {
      leftAccelVector.enabled = false;
      rightAccelVector.enabled = false;
    }
  }
}


