using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerListener : Bolt.GlobalEventListener
{
    public PlayerHealth playerHealth;

    public override void OnEvent(TakeDamageEvent evnt)
    {
        BoltConsole.Write("EVENtttt HEARD");
        playerHealth.TakeDamage(evnt.Damage);
    }
}
