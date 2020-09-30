using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Bolt.EntityBehaviour<ISlayerState>
{
    public float health = 3f;

    public override void Attached()
    {
        if (entity.IsOwner)
        {
            state.SlayerHealth = health;
        }

        state.AddCallback("SlayerHealth", HealthCallback);

    }

    private void HealthCallback()
    {
        health = state.SlayerHealth;

        if (health <= 0) 
        {
            Die();
        }

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (entity.IsOwner)
            {
               // state.SlayerHealth -= 1;
            }
        }
    }

    public void Die()
    {
        BoltNetwork.Destroy(gameObject);
    }

}
