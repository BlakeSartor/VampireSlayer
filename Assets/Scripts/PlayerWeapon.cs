using Bolt;
using Bolt.Utils;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class PlayerWeapon : Bolt.EntityEventListener<ISlayerState>
{
    public GameObject pauseMenu;
    public GameObject muzzlePrefab;
    public GameObject[] WeaponObjects;
    public Camera cam;
    public int activeWeapon;

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
        PauseMenu pause = pauseMenu.GetComponent<PauseMenu>();
        if (!pause.getIsPaused())
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

    }

    public void Shoot()
    {

        var currentWeapon = WeaponObjects[state.WeaponActiveIndex];
        Weapon weapon = currentWeapon.GetComponent<Weapon>();

        float gunRange = weapon.GetRange();
        float gunDamage = weapon.GetDamage();
        float projectileSpeed = weapon.GetProjectileSpeed();
        bool isProjectileShooter = weapon.GetIsProjectileShooter();

        GameObject projectilePrefab = weapon.projectilPrefab;
        GameObject projectilHitPrefab = weapon.projectileHitPrefab;
        GameObject firePoint = weapon.firePoint;

        Vector3 aimPoint;
        RaycastHit rayHit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out rayHit, gunRange))
        {
            aimPoint = rayHit.point;
            var targetEntity = rayHit.collider.gameObject.GetComponent<BoltEntity>();
            if (entity.IsOwner)
            {

                if (!isProjectileShooter)
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
                    if (projectilHitPrefab != null)
                    {
                        Quaternion rot = Quaternion.FromToRotation(Vector3.up, aimPoint);
                        var hitfx = Instantiate(projectilHitPrefab, aimPoint, rot);
                    }
                }


            }
        }
        else
        {
            aimPoint = cam.transform.position + cam.transform.forward * gunRange;
        }
        if (entity.IsOwner)
        {

            var bullet = BoltNetwork.Instantiate(projectilePrefab, firePoint.transform.position, Quaternion.identity);

            var projectileScript = bullet.GetComponent<Projectile>();
            projectileScript.SetSpeed(projectileSpeed);
            projectileScript.SetIsProjectileShooter(isProjectileShooter);
            projectileScript.SetProjectileDamage(gunDamage);
            projectileScript.SetProjectileHitPrefab(projectilHitPrefab);

            bullet.transform.LookAt(aimPoint);

             if (muzzlePrefab != null)
                {
                    var muzzleVfx = BoltNetwork.Instantiate(muzzlePrefab, firePoint.transform.position, Quaternion.identity);
                    muzzleVfx.transform.forward = bullet.transform.forward;
                }
        }
    }

     public int getActiveWeapon()
    {
        return state.WeaponActiveIndex;
    }
}
