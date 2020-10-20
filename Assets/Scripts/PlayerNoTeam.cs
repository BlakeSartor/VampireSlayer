using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNoTeam : Bolt.EntityEventListener<ISlayerState>
{
    public Camera cam;



    public override void Attached()
    {
        state.SetTransforms(state.SlayerTransform, transform);

        if (entity.IsOwner)
        {
            cam.gameObject.SetActive(true);

  
        }
    }
}
