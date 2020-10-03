using Bolt;
using Bolt.Utils;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class PlayerWeapon : Bolt.EntityEventListener<ISlayerState>
{
    public int muzzleIndex;
    public GameObject muzzlePrefab;
    public GameObject[] WeaponObjects;
    public GameObject[] firePoints;
    public Camera cam;
    public float bulletSpeed;
    public int activeWeapon;
    public float gunRange = 100f;
    public float gunDamage = 1f;
    public GameObject projectileHitPrefab;

    public override void Attached()
    {
        state.SetTransforms(state.SlayerTransform, transform);

        state.OnWeaponShoot = Shoot;

        if (entity.IsOwner)
        {

            for (int i = 0; i < state.WeaponArray.Length; ++i)
            {
                state.WeaponArray[i].WeaponId = i;
                state.WeaponArray[i].WeaponAmmo = Random.Range(50, 100);
            }

            state.WeaponActiveIndex = -1;
        }

        state.AddCallback("WeaponActiveIndex", WeaponActiveIndexChanged);
    }

    void WeaponActiveIndexChanged()
    {

        for (int i = 0; i < WeaponObjects.Length; ++i)
        {
            WeaponObjects[i].SetActive(false);
        }

        if (state.WeaponActiveIndex >= 0)
        {
            {
                int objectId = state.WeaponArray[state.WeaponActiveIndex].WeaponId;
                WeaponObjects[objectId].SetActive(true);
                activeWeapon = state.WeaponActiveIndex;
            }
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && entity.IsOwner) state.WeaponActiveIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2) && entity.IsOwner) state.WeaponActiveIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3) && entity.IsOwner) state.WeaponActiveIndex = 2;
        if (Input.GetKeyDown(KeyCode.Alpha0) && entity.IsOwner) state.WeaponActiveIndex = -1;

        if (Input.GetKeyDown(KeyCode.Mouse0) && entity.IsOwner)
        {
            if (state.WeaponActiveIndex >= 0)
            {
                state.WeaponShoot();
            }
        }


    }

    public void Shoot()
    {
        Vector3 aimPoint;
        RaycastHit rayHit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out rayHit, gunRange))
        {
            aimPoint = rayHit.point;
            var targetEntity = rayHit.collider.gameObject.GetComponent<BoltEntity>();
            if (entity.IsOwner)
            {
                BoltConsole.Write("HIT DETECTED  on OBJECT : " + rayHit.collider.gameObject.name);
                if (targetEntity != null)
                {
                    if (entity.IsAttached)
                    {
                        BoltConsole.Write("CALLING EVENT");
                        var evnt = TakeDamageEvent.Create(targetEntity.Source);
                        evnt.Damage = gunDamage;
                        evnt.Send();
                    }
                }
                if (projectileHitPrefab != null)
                {
                    Quaternion rot = Quaternion.FromToRotation(Vector3.up, aimPoint);
                    var hitfx = Instantiate(projectileHitPrefab, aimPoint, rot);
                }
            }
        }
        else
        {
            aimPoint = cam.transform.position + cam.transform.forward * gunRange;
        }
        if (entity.IsOwner)
        {

            muzzleIndex = state.WeaponActiveIndex;
            var muz = firePoints[muzzleIndex];

            var bullet = BoltNetwork.Instantiate(BoltPrefabs.Projectile, muz.transform.position, Quaternion.identity);

            bullet.transform.LookAt(aimPoint);

             if (muzzlePrefab != null)
                {
                    var muzzleVfx = BoltNetwork.Instantiate(muzzlePrefab, muz.transform.position, Quaternion.identity);
                    muzzleVfx.transform.forward = bullet.transform.forward;
                }
        }
    }

     public int getActiveWeapon()
    {
        return state.WeaponActiveIndex;
    }
}
