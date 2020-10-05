using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDestruct : Bolt.EntityBehaviour<IProjectileHitState>
{
    public float timeToDie = 10f;

    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(timeToDie);
        if (entity.IsOwner)
        {
            BoltNetwork.Destroy(gameObject);

        }
    }
}
