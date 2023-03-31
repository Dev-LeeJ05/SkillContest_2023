using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [Header("Bullet Prefabs")]
    public GameObject StraightBullet;
    public GameObject TargetBullet;

    public void Straight(Vector2 pos, Quaternion rot)
    {
        GameObject bullet = Instantiate(StraightBullet, InGameManager.Instance.bulletSpawn);
        bullet.transform.position = pos;
        bullet.transform.rotation = rot;
    }

    public void Target(Vector2 pos)
    {
        GameObject bullet = Instantiate(TargetBullet, InGameManager.Instance.bulletSpawn);
        bullet.transform.position = pos;
    }

    public void TripleShot(Vector2 pos)
    {
        for (int i = 0; i<3; i++)
        {
            Straight(pos, Quaternion.Euler(0,0, -15 + 15 * i));
        }
    }

    public IEnumerator CircularSector(Vector2 pos)
    {
        if (Random.Range(0, 2) < 1)
            for (int i = 0; i < 9; i++)
            {
                Straight(pos, Quaternion.Euler(0, 0, -60 + 15 * i));
                yield return new WaitForSeconds(0.07f);
            }
        else
            for (int i = 0; i < 9; i++)
            {
                Straight(pos, Quaternion.Euler(0, 0, 60 - 15 * i));
                yield return new WaitForSeconds(0.07f);
            }
    }

    public void Circle(Vector2 pos, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Straight(pos, Quaternion.Euler(0, 0, (360 / count) * i));
        }
    }

    public IEnumerator Dust(Vector2 pos,int count)
    {
        for (int i = 0; i < count; i++)
        {
            Straight(pos, Quaternion.Euler(0, 0, -60 + Random.Range(0,120)));
            yield return new WaitForSeconds(0.07f);
        }
    }
}
