using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Stage : MonoBehaviour
{
    public GameObject Boss;

    public UnityEvent nextWave;

    public int currentWave;

    void Start()
    {
        nextWave.AddListener(NextWave);
    }

    void Update()
    {

    }

    public virtual void NextWave()
    {
        Debug.Log($"{InGameManager.Instance.currentStage}Wave++");
        currentWave++;
    }

    public virtual void WaveBoss()
    {
        InGameManager.Instance.canvas.bossBar.ActiveBossBar(true);
        Instantiate(Boss, new Vector2(0, 10), Quaternion.identity).TryGetComponent<BossBase>(out BossBase boss);
        boss.GetComponent<BossBase>().target = InGameManager.Instance.bossTarget;
        InGameManager.Instance.curBoss = boss;
    }
}
