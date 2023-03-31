using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameClear : MonoBehaviour
{
    public TextMeshProUGUI FinalScore;

    public void Clear()
    {
        FinalScore.text = $"최종 점수 : {InGameManager.Instance.currentScore}";
        gameObject.SetActive(true);
    }

    public void NextButton()
    {
        if (GameManager.Instance.ranks.Count < 5 || GameManager.Instance.ranks[GameManager.Instance.ranks.Count - 1].Score < InGameManager.Instance.currentScore)
        {
            InGameManager.Instance.canvas.rankRegister.Active();
        }
        else
        {
            GameManager.Instance.GetComponent<SceneController>().SceneLoad("Rank");
        }
    }
}
