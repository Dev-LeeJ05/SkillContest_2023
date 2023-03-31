using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance => instance;

    [Header("Score")]
    public List<PlayerRank> ranks;
    public int bestScore;

    [Header("InGame")]
    public GameObject[] playerPrefabs;
    public GameObject selectedPlayer;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (ranks.Count> 0)
        {
            SortRank();
            bestScore = ranks[0].Score;
        }
    }

    public void SortRank()
    {
        ranks.Sort((a, b) => b.Score.CompareTo(a.Score));
    }

}

[System.Serializable]
public class PlayerRank
{
    public string Name;
    public int Score;
}