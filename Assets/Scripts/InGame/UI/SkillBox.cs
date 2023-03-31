using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillBox : MonoBehaviour
{
    public Image coolDownImg;
    public TextMeshProUGUI skillCoolDownTxt;
    public TextMeshProUGUI skillCountTxt;
    public GameObject Press;

    public void ActionSkill(float curDelay,float maxDuration, int count)
    {
        float coolDown = 1 - curDelay / maxDuration;
        //Debug.Log($"Heal : {coolDown}");
        coolDownImg.fillAmount = coolDown;

        float remainTime = maxDuration - curDelay;
        if (coolDown > 0)
            skillCoolDownTxt.text = remainTime.ToString("0.0");
        else skillCoolDownTxt.text = "";

        skillCountTxt.text = count.ToString();
    }
}
