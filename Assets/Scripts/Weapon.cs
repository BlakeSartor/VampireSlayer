using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectileHitPrefab;
    public GameObject projectilPrefab;
    public GameObject firePoint;

    public float damage;
    public float range;
    public float fireRate;
    public float projectileSpeed;

    public bool isProjectileShooter;

    public float GetDamage()
    {
        return damage;
    }

    public float GetRange()
    {
        return range;
    }

    public float GetFireRate()
    {
        return fireRate;
    }

    public float GetProjectileSpeed()
    {
        return projectileSpeed;
    }

    public bool GetIsProjectileShooter()
    {
        return isProjectileShooter;
    }

    public GameObject getProjectileHitPrefab()
    {
        return projectileHitPrefab;
    }
}
