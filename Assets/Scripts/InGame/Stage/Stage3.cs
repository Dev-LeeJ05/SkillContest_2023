using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3 : Stage
{
    public override void NextWave()
    {
        base.NextWave();
        switch (currentWave)
        {
            case 1:
                InGameManager.Instance.enemySpawn.BasicSpawn(0);
                InGameManager.Instance.enemySpawn.BasicSpawn(1);
                InGameManager.Instance.enemySpawn.BasicSpawn(2);
                break;
            case 2:
                InGameManager.Instance.enemySpawn.TargetSpawn(0);
                InGameManager.Instance.enemySpawn.TargetSpawn(1);
                break;
            case 3:
                InGameManager.Instance.enemySpawn.BasicSpawn(2);
                InGameManager.Instance.enemySpawn.BasicSpawn(0);
                InGameManager.Instance.enemySpawn.TargetSpawn(0);
                break;
            case 4:
                InGameManager.Instance.enemySpawn.BasicSpawn(0);
                InGameManager.Instance.enemySpawn.BasicSpawn(1);
                InGameManager.Instance.enemySpawn.BasicSpawn(2);
                InGameManager.Instance.enemySpawn.TargetSpawn(0);
                InGameManager.Instance.enemySpawn.TargetSpawn(1);
                break;
            case 5:
                StartCoroutine(InGameManager.Instance.MeteorGenerator());
                WaveBoss();
                break;
            default:
                break;
        }
        InGameManager.Instance.isWaving = true;
    }
}
