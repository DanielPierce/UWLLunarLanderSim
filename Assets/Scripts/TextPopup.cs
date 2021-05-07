using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopup : Popup
{
    public TMP_Text value;

    public void DisplayPopup(string toDisplay)
    {
        base.DisplayPopup();
        value.enabled = true;
        value.text = toDisplay;
    }

    public override void HidePopup()
    {
        base.HidePopup();
        value.enabled = false;
    }
}
