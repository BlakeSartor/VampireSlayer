using Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Bolt.EntityBehaviour<IProjectileState>
{
    private float speed;
    private bool isProjectileShooter;
    private float damage;
    private GameObject projectileHitPrefab;

    public override void Attached()
    {
        state.SetTransforms(state.ProjectileTransform, transform);

    }

    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(15f);
        if (entity.IsOwner)
        {
            BoltNetwork.Destroy(gameObject);

        }
    }

    public override void SimulateOwner()
    {
        if (speed != 0 )
        {
            transform.position += transform.forward * (speed * BoltNetwork.FrameDeltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (entity.IsAttached)
        {
            speed = 0;
            Destroy(gameObject);

            if (isProjectileShooter)
            {
                var targetEntity = collision.gameObject.GetComponent<BoltEntity>();
                if (targetEntity != null)
                {
                    BoltConsole.Write("CALLING EVENT");
                    var evnt = TakeDamageEvent.Create(targetEntity.Source);
                    evnt.Damage = damage;
                    evnt.Send();
                }
                if (projectileHitPrefab != null)
                {
                    ContactPoint contact = collision.contacts[0];
                    Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
                    var hitfx = Instantiate(projectileHitPrefab, contact.point, rot);
                }

            }

        }
    }
    
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetIsProjectileShooter(bool isProjectileShooter)
    {
        this.isProjectileShooter = isProjectileShooter;
    }

    public void SetProjectileDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetProjectileHitPrefab(GameObject projectileHitPrefab)
    {
        this.projectileHitPrefab = projectileHitPrefab;
    }
}
