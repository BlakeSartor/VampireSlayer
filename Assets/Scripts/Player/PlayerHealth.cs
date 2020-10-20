using UnityEngine;

public class PlayerHealth : Bolt.EntityEventListener<ISlayerState>
{
    public float maxHealth = 100f;
    public HealthBar healthbar;
    private float health;
    public void Start()
    {
        health = maxHealth;
        healthbar.SetMaxHealth(health);
    }

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


    public void TakeDamage(float amount)
    {

        if (entity.IsOwner)
        {
            BoltConsole.Write("---HIT DETECTED---: " + amount + " damage taken");

            BoltConsole.Write("Health: " + health);
            health -= amount;

            healthbar.SetHealth(health);

            BoltConsole.Write("Health after: " + health);


            if (health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        BoltNetwork.Destroy(gameObject);

        var spawnPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(15f, 30f), 0f); 
        var player = BoltNetwork.Instantiate(BoltPrefabs.Vampire, spawnPosition, Quaternion.identity);
    }

}