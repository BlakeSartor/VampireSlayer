using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerListener : Bolt.GlobalEventListener
{

    public HealthBar healthbar;

    public float maxHealth = 10f;
    float health;
    public override void OnEvent(TakeDamageEvent evnt)
    {
        BoltConsole.Write("EVENtttt HEARD");
        TakeDamage(evnt.Damage);
    }

    public void Start()
    {
        health = maxHealth;
        healthbar.SetMaxHealth(health);
    }

    public void TakeDamage(float amount)
    {
        BoltConsole.Write("Healt: " + health);
        health -= amount;

        healthbar.SetHealth(health);

        BoltConsole.Write("Healt after: " + health);


        if (health <= 0 )
        {
            Die();
        }
    }

    void Die()
    {
        BoltNetwork.Destroy(gameObject);
        var spawnPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(15f, 30f), 0f);

        var player = BoltNetwork.Instantiate(BoltPrefabs.FirstPersonPlayer, spawnPosition, Quaternion.identity);

        health = maxHealth;
        healthbar.SetHealth(maxHealth);

    }
}
