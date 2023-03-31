using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FuelContainer : MonoBehaviour
{
    public Slider fuelSlider;
    public TextMeshProUGUI fuelTxt;

    public void setFuel(int fuel, float maxfuel)
    {
        fuelTxt.text = fuel.ToString();
        fuelSlider.value = fuel;

        float calc = fuel / maxfuel;

        if (calc >= 0.8f)
            fuelTxt.color = Color.green;
        else if (calc >= 0.3f)
            fuelTxt.color = Color.yellow;
        else
            fuelTxt.color = Color.red;
    }
}
