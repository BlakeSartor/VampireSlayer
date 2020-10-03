using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListener : Bolt.GlobalEventListener
{

    public float health = 3f;
    public override void OnEvent(TakeDamageEvent evnt)
    {
        BoltConsole.Write("EVENtttt HEARD");
        TakeDamage(evnt.Damage);
    }

    public void TakeDamage(float amount)
    {
        BoltConsole.Write("Healt: " + health);
        health -= amount;

        BoltConsole.Write("Healt after: " + health);


        if (health <= 0 )
        {
            Die();
        }
    }

    void Die()
    {
        BoltNetwork.Destroy(gameObject);
    }
}
