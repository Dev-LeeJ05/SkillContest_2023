using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossBase : MonoBehaviour
{
    [Header("Status")]
    public int maxHp;
    public string bossName;

    [Header("Values")]
    public int hp;
    public int phase;

    [Header("Variables")]
    public float moveSpeed;
    public bool isMoving;
    public bool endIntro;
    public bool isWaving;
    public bool wavingFunc;
    public Transform target;

    [Header("Bullet")]
    public Transform FirePos;

    void Start()
    {
        hp = maxHp;
        endIntro = false;
        isMoving = true;
    }

    protected virtual void Update()
    {
        IntroMove();
    }

    public void IntroMove()
    {
        if (isMoving && !endIntro)
        {
            Vector3 direction = (target.position - transform.position).normalized;

            if (Vector3.Distance(transform.position, target.position) <= 0.1f)
            {
                isMoving = false;
                endIntro = true;
                isWaving = true;
                StartCoroutine(WaveManager());
                StartCoroutine(GetTarget());
            }
            else
            {
                float distance = moveSpeed * Time.deltaTime;

                if (Vector3.Distance(transform.position, target.position) < distance)
                {
                    distance = Vector3.Distance(transform.position, target.position);
                }

                transform.Translate(direction * distance, Space.World);
            }
        }
        else return;
    }

    public void GetDamage(int damage)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, maxHp);
        if (hp <= 0)
        {
            InGameManager.Instance.BossDie();
        }
    }

    public abstract IEnumerator WaveManager();

    public void PhaseFunc(int phasenum)
    {   
        switch (phasenum + 1)
        {
            case 1:
                Phase1();
                break;
            case 2:
                Phase2();
                break;
            case 3:
                Phase3();
                break;
            case 4:
                Phase4();
                break;
        }
    }

    public void Phase1() // 스테이지 1
    {
        if (isWaving) return;

        InGameManager.Instance.bulletManager.TripleShot(FirePos.position);
        InGameManager.Instance.bulletManager.Target(FirePos.position);

        Invoke("Phase1", Random.Range(0.3f,0.7f));
    }

    public void Phase2() // 스테이지 1
    {
        if (isWaving) return;

        // 탄막 뿌리기
        StopCoroutine(InGameManager.Instance.bulletManager.CircularSector(FirePos.position));
        StartCoroutine(InGameManager.Instance.bulletManager.CircularSector(FirePos.position));

    }

    public void Phase3() // 스테이지 2
    {
        if (isWaving) return;

        InGameManager.Instance.bulletManager.Circle(FirePos.position, 24);
        InGameManager.Instance.bulletManager.Target(FirePos.position);

        Invoke("Phase3", Random.Range(0.7f,1.7f));
    }

    public void Phase4() // 스테이지 3
    {
        if (isWaving) return;

        int dustcount = Random.Range(1, 30 + 1);
        StopCoroutine(InGameManager.Instance.bulletManager.Dust(FirePos.position, dustcount));
        StartCoroutine(InGameManager.Instance.bulletManager.Dust(FirePos.position, dustcount));
    }

    public virtual IEnumerator GetTarget()
    {
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            GetDamage(collision.GetComponent<PlayerBullet>().damage);
            Destroy(collision.gameObject);
        }
    }
}
