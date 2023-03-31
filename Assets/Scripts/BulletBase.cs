using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    [Header("Status")]
    public int damage;
    public float moveSpeed;

    protected virtual void Start()
    {
        Destroy(gameObject, 2.5f);
    }

    private void Update()
    {
        BulletMove();
    }

    public abstract void BulletMove();
}
