using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Status")]
    public int maxHp;
    public float moveSpeed;
    public float maxDuration = 0.15f;

    [Header("Values")]
    public int hp;
    public float curDelay;

    [Header("Variables")]
    public Vector3 targetPosition;
    public float arrivalDistance = 0.1f;
    public bool isMoving;
    public Transform FirePos;

    void Start()
    {
        hp = maxHp;
    }

    protected virtual void Update()
    {
        if (isMoving)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;

            if (Vector3.Distance(transform.position, targetPosition) <= arrivalDistance)
            {
                isMoving = false;
            }
            else
            {
                float distance = moveSpeed * Time.deltaTime;

                if (Vector3.Distance(transform.position, targetPosition) < distance)
                {
                    distance = Vector3.Distance(transform.position, targetPosition);
                }

                transform.Translate(direction * distance, Space.World);
            }
        }
        if (!InGameManager.Instance.isGameStarted) return;
        AttackFunc();
    }

    protected abstract void AttackFunc();

    public void GetDamage(int value)
    {
        hp -= value;
        if(hp <= 0)
        {
            InGameManager.Instance.EnemeyDie();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            GetDamage(collision.GetComponent<PlayerBullet>().damage);
            Destroy(collision.gameObject);
        }
    }
}
