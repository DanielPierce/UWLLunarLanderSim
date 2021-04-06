using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustController : MonoBehaviour
{
  public bool isThrusting = true;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (
    }

  void ToggleThruster()
  {
    if (isThrusting)
    {
      isThrusting = false;
    }
    else
    {
      isThrusting = true;
    }
  }
}
