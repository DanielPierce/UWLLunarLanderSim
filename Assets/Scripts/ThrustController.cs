using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustController : MonoBehaviour
{
  public bool isThrusting = true;
  public Animator animator;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
  }

  public void ToggleThruster()
  {
    if (animator.GetBool("isThrusting"))
    {
      animator.SetBool("isThrusting", false);
    }
    else
    {
      animator.SetBool("isThrusting", true);
    }
  }
}
