using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
  public void changeSceneMoonArcade()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }

  public void changeSceneMoonRealPhysics()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
  }

  public void changeSceneMain() {
    SceneManager.LoadScene(0);
  }
}
