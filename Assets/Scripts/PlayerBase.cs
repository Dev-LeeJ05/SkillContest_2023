using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject bullet;

    [Header("Status")]
    public int maxHp;
    public int maxFuel;
    public float moveSpeed = 3.5f;
    public float maxDuration = 0.15f;
    public bool isgod = false;
    public float maxGodDuration = 5f;

    [Header("Values")]
    public int hp;
    public int fuel;
    public float curDelay;
    public float curGodDelay;
    public int weaponLevel;

    [Header("Variables")]
    public bool isIntro = true;
    public bool isDamaged = false;
    public Transform target;

    [Header("Key")]
    public KeyCode MoveFront = KeyCode.UpArrow;
    public KeyCode MoveBack = KeyCode.DownArrow;
    public KeyCode MoveLeft = KeyCode.LeftArrow;
    public KeyCode MoveRight = KeyCode.RightArrow;
    public KeyCode AttackKey = KeyCode.Z;

    [Header("Animator")]
    public Animator anim;

    protected virtual void Start()
    {
        hp = maxHp;
        fuel = maxFuel;
        isgod = false;

    }

    void Update()
    {
        InGameManager.Instance.canvas.hpContainer.SetHP(Mathf.Clamp(hp,0,maxHp), maxHp);
        InGameManager.Instance.canvas.fuelContainer.setFuel(Mathf.Clamp(fuel,0,maxFuel), maxFuel);
        if (isIntro) Intro();
        if (!InGameManager.Instance.isGameStarted) return;

        hp = Mathf.Clamp(hp, 0, maxHp);
        fuel = Mathf.Clamp(fuel, 0, maxFuel);

        MoveFunc();
        AttackFunc();
        GodFunc();
    }

    void Intro()
    {
        Vector3 direction = (target.position - transform.position).normalized;

        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            //end
            isIntro = false;
            InGameManager.Instance.isGameStarted = true;
            InGameManager.Instance.stage.NextWave();
            FuelManager();
        }
        else
        {
            float distance = 3.5f * Time.deltaTime;

            if (Vector3.Distance(transform.position, target.position) < distance)
            {
                distance = Vector3.Distance(transform.position, target.position);
            }

            transform.Translate(direction * distance, Space.World);
        }
    }

    public void GodFunc()
    {
        if (isgod)
        {
            // anim parameter bool = true
            anim.SetBool("god", true);
            curGodDelay += Time.deltaTime;
            if (curGodDelay >= maxGodDuration)
            {
                isgod = false;
                // anim parameter bool = false
            anim.SetBool("god", false);
            }
        }
    }

    void MoveFunc()
    {
        int DirX = 0;
        int DirY = 0;

        if (Input.GetKey(MoveFront)) DirY = 1;
        if (Input.GetKey(MoveBack)) DirY = -1;
        if (Input.GetKey(MoveLeft)) DirX = -1;
        if (Input.GetKey(MoveRight)) DirX = 1;

        Vector3 DirVec = new Vector3(DirX * Time.deltaTime, DirY * Time.deltaTime) * moveSpeed;

        // clamp
        transform.position += DirVec;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -4f, 4f), Mathf.Clamp(transform.position.y, -4.6f, 3.5f));
    }

    void AttackFunc()
    {
        if (Input.GetKey(AttackKey) && curDelay >= maxDuration)
        {
            BulletShoot();
            curDelay = 0f;
        }
        curDelay += Time.deltaTime;
    }

    public virtual void WeaponLevelup()
    {
        weaponLevel++;
    }

    public virtual void HpRecover(int value = 10)
    {
        hp += value;
        if (hp >= maxHp) hp = maxHp;
    }

    public virtual void FuelRecover(int value = 10)
    {
        fuel += value;
        if (fuel >= maxFuel) fuel = maxFuel;
    }

    public void FuelManager()
    {
        if (!InGameManager.Instance.isGameStarted) return;
        fuel -= 1;
        if(fuel <0)
        {
            InGameManager.Instance.PlayerDie("연료 소진");
        }
        Invoke("FuelManager", 1.5f);
    }

    public void OnDamage(int value)
    {
        if (isDamaged || isgod || !InGameManager.Instance.isGameStarted) return;
        StartCoroutine(GetDamaged());
        hp -= value;
        if (hp < 0)
        {
            // die
            InGameManager.Instance.PlayerDie("체력 부족");
        }
    }

    IEnumerator GetDamaged()
    {

        isDamaged = true;
        // anim
        yield return new WaitForSeconds(0.7f);
        isDamaged = false;
    }

    protected abstract void BulletShoot();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("EnemyBullet"))
        {
            OnDamage(collision.GetComponent<BulletBase>().damage);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Enemy"))
        {
            OnDamage(5);
        }
    }
}
