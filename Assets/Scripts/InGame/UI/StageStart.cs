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
        stageName.text = $"스테이지 {InGameManager.Instance.currentStage}";
        currentScore.text = $"현재 점수 : {InGameManager.Instance.currentScore}";
        gameObject.SetActive(true);
    }

    public void InActive()
    {
        gameObject.SetActive(false);
    }
}
