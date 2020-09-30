using Bolt;
using UnityEngine;

public class PlayerWeapon : Bolt.EntityEventListener<ISlayerState>
{
    public GameObject[] WeaponObjects;

    public Rigidbody bulletPrefab;
    public float bulletSpeed;
    public GameObject muzzle;
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
            int objectId = state.WeaponArray[state.WeaponActiveIndex].WeaponId;
            WeaponObjects[objectId].SetActive(true);
            activeWeapon = state.WeaponActiveIndex;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) state.WeaponActiveIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) state.WeaponActiveIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) state.WeaponActiveIndex = 2;
        if (Input.GetKeyDown(KeyCode.Alpha0)) state.WeaponActiveIndex = -1;

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

        Rigidbody bulletObj = Instantiate(bulletPrefab, muzzle.transform.position, this.transform.rotation);

        bulletObj.velocity = transform.TransformDirection(new Vector3(0, 0, bulletSpeed));
    }

     public int getActiveWeapon()
    {
        return state.WeaponActiveIndex;
    }
}
