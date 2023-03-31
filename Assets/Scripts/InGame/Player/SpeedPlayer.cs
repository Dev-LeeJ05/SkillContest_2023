using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPlayer : PlayerBase
{
    protected override void BulletShoot()
    {
        if (weaponLevel >= 3)
        {
            Instantiate(bullet, transform.position + new Vector3(-0.75f, -0.25f), Quaternion.Euler(0, 0, 10));
            Instantiate(bullet, transform.position + new Vector3(0.75f, -0.25f), Quaternion.Euler(0, 0, -10));
        }
        if (weaponLevel >= 2)
        {
            Instantiate(bullet, transform.position + new Vector3(-0.5f, 0f), Quaternion.Euler(0, 0, 4.5f));
            Instantiate(bullet, transform.position + new Vector3(0.5f, 0f), Quaternion.Euler(0, 0, -4.5f));
        }
        if (weaponLevel >= 1)
        {
            Instantiate(bullet, transform.position + new Vector3(-0.15f, 0.5f), Quaternion.Euler(0, 0, 0));
            Instantiate(bullet, transform.position + new Vector3(0.15f, 0.5f), Quaternion.Euler(0, 0, 0));
        }
        else
        {
            Instantiate(bullet, transform.position + new Vector3(0f, 0.5f), Quaternion.Euler(0, 0, 0));
        }
    }
}
