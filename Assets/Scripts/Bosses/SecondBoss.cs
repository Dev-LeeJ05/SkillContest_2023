using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBoss : BossBase
{
    public float targetX;

    protected override void Update()
    {
        base.Update();
        Move();
    }

    public override IEnumerator WaveManager()
    {
        while (isWaving)
        {
            wavingFunc = true;
            isWaving = false;
            phase = Random.Range(0, 3);
            PhaseFunc(phase);
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            isWaving = true;
            wavingFunc = false;
            yield return new WaitForSeconds(1.2f);
        }
        yield return null;
    }

    public void Move()
    {
        Vector3 dir;
        if (transform.position.x > targetX)
            dir = Vector3.left;
        else dir = Vector3.right;

        if (Vector3.Distance(transform.position, new Vector3(targetX, transform.position.y, 0)) > 0.2)
            transform.position += dir * Time.deltaTime * moveSpeed;
    }

    public override IEnumerator GetTarget()
    {
        base.GetTarget();
        while (true)
        {
            if (!wavingFunc && isWaving)
                StartCoroutine(WaveManager());
            switch (Random.Range(0, 4))
            {
                case 0:
                    targetX = -2.7f;
                    break;
                case 1:
                    targetX = -0.9f;
                    break;
                case 2:
                    targetX = 0.9f;
                    break;
                case 3:
                    targetX = 2.7f;
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(Random.Range(1.2f, 4.2f));
        }
    }
}
