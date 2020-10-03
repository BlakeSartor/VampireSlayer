using Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Bolt.EntityBehaviour<IProjectileState>
{
    public float speed;
    public float damage;
    public float fireRate;
    public float impactForce;

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
        }
    }
}
