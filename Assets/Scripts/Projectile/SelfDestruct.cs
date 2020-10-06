using System.Collections;
using UnityEngine;

public class SelfDestruct : Bolt.EntityBehaviour<IProjectileHitState>
{
    public float timeToDie = 10f;

    void Start()
    {
        StartCoroutine(Destruct());
    }

    IEnumerator Destruct()
    {
        yield return new WaitForSeconds(timeToDie);
        if (entity.IsOwner)
        {
            BoltNetwork.Destroy(gameObject);

        }
    }
}
