using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankManager : MonoBehaviour
{
    private static RankManager _instance;
    public static RankManager Instance => _instance;

    [Header("Prefabs")]
    public GameObject rankPrefab;

    [Header("Rank")]
    public Transform rankTransform;

    private void Start()
    {
        SetRank();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            SetRank();
    }

    public void SetRank()
    {
        GameManager.Instance.SortRank();
        foreach(Transform child in rankTransform)
        {
            Destroy(child.gameObject);
        }
        for(int i = 0; i < GameManager.Instance.ranks.Count; i++)
        {
            Instantiate(rankPrefab, rankTransform).TryGetComponent<Rank>(out Rank rank);
            rank.SetRank(i + 1, GameManager.Instance.ranks[i].Name, GameManager.Instance.ranks[i].Score);
        }
    }

    // return button
    public void PressBackButton()
    {
        GameManager.Instance.GetComponent<SceneController>().SceneLoad("Menu");
    }
}