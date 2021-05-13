using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public GameObject display;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void DisplayPopup()
    {
        display.SetActive(true);
    }

    public virtual void HidePopup()
    {
        display.SetActive(false);
    }
}