using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Bolt.EntityEventListener<ISlayerState>
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

    public void fun()
    {
        BoltConsole.Write("in playerhealth");
    }

    //public override on
    public void Die()
    {
        Debug.Log("trying");
        BoltNetwork.Destroy(gameObject);
    }

}