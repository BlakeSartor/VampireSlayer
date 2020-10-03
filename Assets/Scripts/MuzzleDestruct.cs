using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;

public class MuzzleDestruct : Bolt.EntityBehaviour<IMuzzleFlashState>
{


    void Start()
    {
        StartCoroutine(SelfDestruct());    
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(1f);
        if (entity.IsOwner)
        {
            BoltNetwork.Destroy(gameObject);

        }
    }
}
