using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageStart : MonoBehaviour
{
    public TextMeshProUGUI stageName;
    public TextMeshProUGUI currentScore;

    public void StageClear()
    {
        stageName.text = $"�������� {InGameManager.Instance.currentStage}";
        currentScore.text = $"���� ���� : {InGameManager.Instance.currentScore}";
        gameObject.SetActive(true);
    }

    public void InActive()
    {
        gameObject.SetActive(false);
    }
}
