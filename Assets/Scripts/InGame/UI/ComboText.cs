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
        comboTxt.text = $"연속 처치 {combo}";
        scoreTxt.text = $"+ {score}";
    }
}
