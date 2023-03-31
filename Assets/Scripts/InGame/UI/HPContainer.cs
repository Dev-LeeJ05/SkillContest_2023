using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPContainer : MonoBehaviour
{
    public Slider hpSlider;
    public TextMeshProUGUI hpTxt;

    public void SetHP(int hp, float maxHp)
    {
        hpTxt.text = hp.ToString();

        hpSlider.maxValue = maxHp;
        hpSlider.value = hp;

        float calc = hp / maxHp;
        if (calc >= 0.8f)
            hpTxt.color = Color.green;
        else if (calc >= 0.3f)
            hpTxt.color = Color.yellow;
        else
            hpTxt.color = Color.red;
    }
}
