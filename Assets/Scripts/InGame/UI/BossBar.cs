using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    public TextMeshProUGUI bossBarText;
    public Slider bossBar;

    public void ActiveBossBar(bool active)
    {
        gameObject.SetActive(active);
    }

    public void UpdateBossBar(int hp, int maxHp, string bossName)
    {
        bossBarText.text = $"{bossName} º¸½º {hp} / {maxHp}";
        bossBar.maxValue = maxHp;
        bossBar.value = hp;
    }
}
