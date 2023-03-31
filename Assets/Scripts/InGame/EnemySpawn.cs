using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject[] Enemies;

    [HideInInspector]
    public List<GameObject> enlist;

    private void Start()
    {
        enlist.Clear();
    }

    public void BasicSpawn(int posNum)
    {
        SpawnEnemy(0, 7f, 4f);
        switch (posNum)
        {
            case 0:
                enlist[0].GetComponent<EnemyBase>().targetPosition = new Vector2(-4, 2);
                enlist[1].GetComponent<EnemyBase>().targetPosition = new Vector2(4, 2);
                break;
            case 1:
                enlist[0].GetComponent<EnemyBase>().targetPosition = new Vector2(-2.5f, 1);
                enlist[1].GetComponent<EnemyBase>().targetPosition = new Vector2(2.5f, 1);
                break;
            case 2:
                enlist[0].GetComponent<EnemyBase>().targetPosition = new Vector2(-1, 0);
                enlist[1].GetComponent<EnemyBase>().targetPosition = new Vector2(1, 0);
                break;
            default:
                break;
        }
        enlist.Clear();
    }

    public void TargetSpawn(int posNum)
    {
        SpawnEnemy(1, 7f, 4f);
        switch (posNum)
        {
            case 0:
                enlist[0].GetComponent<EnemyBase>().targetPosition = new Vector2(-3, 2.5f);
                enlist[1].GetComponent<EnemyBase>().targetPosition = new Vector2(3, 2.5f);
                break;
            case 1:
                enlist[0].GetComponent<EnemyBase>().targetPosition = new Vector2(-1.5f, 1);
                enlist[1].GetComponent<EnemyBase>().targetPosition = new Vector2(1.5f, 1);
                break;
            default:
                break;
        }
        enlist.Clear();
    }

    public void TripleSpawn(int posNum)
    {
        SpawnEnemy(2, 6f, 0f);
        switch (posNum)
        {
            case 0:
                enlist[0].GetComponent<EnemyBase>().targetPosition = new Vector2(-1, 3);
                enlist[1].GetComponent<EnemyBase>().targetPosition = new Vector2(1, 3);
                break;
            case 1:
                enlist[0].GetComponent<EnemyBase>().targetPosition = new Vector2(-3, 1.5f);
                enlist[1].GetComponent<EnemyBase>().targetPosition = new Vector2(3, 1.5f);
                break;
            default:
                break;
        }
        enlist.Clear();
    }

    void SpawnEnemy(int enemy,float spawnX,float spawnY)
    {
        for (int i = 0; i < 2; i++)
        {
            enlist.Add(Instantiate(Enemies[enemy], InGameManager.Instance.enemySpawnTrans));
            if (i < 1)
                enlist[i].transform.position = new Vector3(-spawnX, spawnY, 0);
            else
                enlist[i].transform.position = new Vector3(spawnX, spawnY, 0);
        }
    }
}
