using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Panel")]
    public GameObject Start;
    public GameObject Help;

    public void GameStart()
    {
        Start.SetActive(true);
    }

    public void GameHelp()
    {
        Help.SetActive(true);
    }

    public void GameRank()
    {
        GameManager.Instance.gameObject.GetComponent<SceneController>().SceneLoad("Rank");
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void GameStartButton()
    {
        GameManager.Instance.GetComponent<SceneController>().SceneLoad("InGame");
    }
}
