using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleEnemy : EnemyBase
{
    protected override void AttackFunc()
    {
        curDelay += Time.deltaTime;
        if (curDelay > maxDuration)
        {
            InGameManager.Instance.bulletManager.TripleShot(FirePos.position);
            curDelay = 0f; 
        }
    }
}
