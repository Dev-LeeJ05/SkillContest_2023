using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float moveSpeed;

    [Header("Stat")]
    public int maxHp= 1000;
    public int hp;

    private void Start()
    {
        hp = maxHp;
        Destroy(gameObject, 4.5f);
    }

    void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * moveSpeed;
    }

    public void GetDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            InGameManager.Instance.EnemeyDie();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            GetDamage(collision.GetComponent<PlayerBullet>().damage);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
    }
}
