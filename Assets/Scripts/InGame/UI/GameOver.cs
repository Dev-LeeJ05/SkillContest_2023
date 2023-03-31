using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI reasonText;

    public void EndGame(string reason)
    {
        reasonText.text = reason;
        gameObject.SetActive(true);
    }

    public void PressNextButton()
    {
        if (GameManager.Instance.ranks.Count < 5 || GameManager.Instance.ranks[GameManager.Instance.ranks.Count-1].Score < InGameManager.Instance.currentScore)
        {
            InGameManager.Instance.canvas.rankRegister.Active();
        }
        else
        {
            GameManager.Instance.GetComponent<SceneController>().SceneLoad("Rank");
        }
    }
}
