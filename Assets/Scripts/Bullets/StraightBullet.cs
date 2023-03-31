using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : BulletBase
{
    public override void BulletMove()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }
}
