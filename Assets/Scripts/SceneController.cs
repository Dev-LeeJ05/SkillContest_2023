using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private void Start()
    {
        SceneLoad("Menu");
    }

    public void SceneLoad(string name)
    {
        SceneManager.LoadScene(name);
    }
}
