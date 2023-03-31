using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Info : MonoBehaviour
{
    [Header("Text")]
    public TextMeshProUGUI currentStage;
    public TextMeshProUGUI currentTimer;

    private void Update()
    {
        currentTimer.text = InGameManager.Instance.gameTimer.ToString("0.0");
    }

    public void SetStage(int stage)
    {
        currentStage.text = $"스테이지 {stage}";
    }
}
