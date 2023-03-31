using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboText : MonoBehaviour
{
    public TextMeshProUGUI comboTxt;
    public TextMeshProUGUI scoreTxt;

    public void SetText(int combo, int score)
    {
        comboTxt.text = $"���� óġ {combo}";
        scoreTxt.text = $"+ {score}";
    }
}
