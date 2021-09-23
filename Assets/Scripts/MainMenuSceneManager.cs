using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSceneManager : MonoBehaviour
{
    public GameObject creditsPopup;

    private bool isCredits = false;

    public void Start()
    {
        creditsPopup.SetActive(isCredits);
    }
    
    public void changeCredits() {
        isCredits = !isCredits;
        creditsPopup.SetActive(isCredits);
    }
}
