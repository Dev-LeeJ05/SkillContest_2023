using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEnemy : EnemyBase
{
    protected override void AttackFunc()
    {
        curDelay += Time.deltaTime;
        if (curDelay > maxDuration)
        {
            InGameManager.Instance.bulletManager.Target(FirePos.position);
            curDelay = 0f;
        }   
    }
}
