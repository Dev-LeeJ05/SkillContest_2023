using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBox : MonoBehaviour
{
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI bestScore;

    public void setScore(int score,bool bestscore)
    {
        bestScore.text = GameManager.Instance.bestScore.ToString();
        if (bestscore)
            bestScore.text = score.ToString();
        currentScore.text = score.ToString();
    }
}
