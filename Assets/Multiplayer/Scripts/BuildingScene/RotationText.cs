using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Mirror;

public class RotationText : NetworkBehaviour
{
    public TextMeshProUGUI rotationAmountText;
    public Slider slider;

    void Update()
    {
        rotationAmountText.text = "Rotation"  + slider.value.ToString();  ;
    }
}
