using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Rank : MonoBehaviour
{
    public TextMeshProUGUI ranking;
    public TextMeshProUGUI nickName;
    public TextMeshProUGUI score;

    public void SetRank(int ranking, string nickName, int score)
    {
        this.ranking.text = ranking.ToString();
        this.nickName.text = nickName;
        this.score.text = score.ToString();
    }
}
