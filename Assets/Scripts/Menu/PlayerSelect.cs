using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour
{
    [Header("PlayerSelected")]
    public Sprite[] player;
    public Image selectedplayer;
    public Slider hp;
    public Slider atk;
    public Slider speed;
    public Slider bulletSpeed;

    public void SetPlayerSelected(int i)
    {
        selectedplayer.sprite = player[i];
        GameManager.Instance.selectedPlayer = GameManager.Instance.playerPrefabs[i];
        switch (i)
        {
            case 0: // basic
                hp.value = 6;
                atk.value = 5;
                speed.value = 5;
                bulletSpeed.value = 6;
                break;
            case 1: // power
                hp.value = 9;
                atk.value = 7;
                speed.value = 3;
                bulletSpeed.value = 4;
                break;
            case 2: // speed
                hp.value = 4;
                atk.value = 3;
                speed.value = 7;
                bulletSpeed.value = 8;
                break;
            default:
                break;
        }
    }

    private void OnEnable()
    {
        SetPlayerSelected(0);
    }

    public void CloseTab()
    {
        gameObject.SetActive(false);
    }
}
