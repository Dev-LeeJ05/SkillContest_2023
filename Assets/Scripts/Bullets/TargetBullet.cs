using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBullet : BulletBase
{
    public Vector3 direction;

    public override void BulletMove()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    protected override void Start()
    {
        base.Start();
        GetDirection();
    }

    void GetDirection()
    {
        direction = (InGameManager.Instance.curPlayer.transform.position - transform.position).normalized;
    }
}
