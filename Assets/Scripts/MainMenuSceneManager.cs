using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSceneManager : MonoBehaviour
{
    public Image creditsPopup;

    private bool isCredits = false;
    
    public void changeCredits() {
        isCredits = !isCredits;

        if (isCredits) {
            creditsPopup.enabled = true;
        } else {
            creditsPopup.enabled = false;
        }
        
    }
}
