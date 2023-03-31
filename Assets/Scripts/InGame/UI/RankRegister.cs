using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankRegister : MonoBehaviour
{
    [Header("Text")]
    public TMP_InputField nameInput;

    public void Active()
    {
        gameObject.SetActive(true);
    }

    public void PressButton()
    {
        if (nameInput.text.Length < 1 || nameInput.text.Length > 3)
            return;
        while (GameManager.Instance.ranks.Count >= 5)
        {
            GameManager.Instance.ranks.RemoveAt(GameManager.Instance.ranks.Count - 1);
        }
        PlayerRank rank = new PlayerRank();
        rank.Name = nameInput.text;
        rank.Score = InGameManager.Instance.currentScore;
        GameManager.Instance.ranks.Add(rank);
        GameManager.Instance.GetComponent<SceneController>().SceneLoad("Rank");
    }
}
