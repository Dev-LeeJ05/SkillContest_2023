using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyBase
{
    protected override void AttackFunc()
    {
        curDelay += Time.deltaTime;
        if(curDelay > maxDuration)
        {
            InGameManager.Instance.bulletManager.Straight(FirePos.position, Quaternion.identity);
            curDelay = 0f;
        }
    }
}
