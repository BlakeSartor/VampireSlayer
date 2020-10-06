using UnityEngine;
using System.Collections.Generic;

[BoltGlobalBehaviour("Main2")]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
    void OnGUI()
    {
        int maxMessages = Mathf.Min(5, logMessages.Count);

        GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, Screen.height - 100, 400, 100), GUI.skin.box);

        for (int i = 0; i < maxMessages; i++)
        {
            GUILayout.Label(logMessages[i]);
        }

        GUILayout.EndArea();
    }


    List<string> logMessages = new List<string>();

    [System.Obsolete]
    public override void SceneLoadLocalDone(string scene)
    {
        var spawnPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(15f,30f), 0f);

        var player = BoltNetwork.Instantiate(BoltPrefabs.FirstPersonPlayer, spawnPosition, Quaternion.identity);
    
    }

    public override void OnEvent(PlayerJoinedEvent evnt)
    {
        logMessages.Insert(0, evnt.Message);
    }

    public override void OnEvent(TakeDamageEvent evnt)
    {
            if (evnt.Entity.IsOwner)
            {
                if (evnt.Entity.Source == null)
                {
                    BoltEntity entity = evnt.Entity;
                    PlayerHealth playerHealth = entity.GetComponent<PlayerHealth>();
                    playerHealth.TakeDamage(evnt.Damage);
                }
            }
            else
            {
                if (evnt.Entity.Source == null)
                {
                    BoltConsole.Write("empty soiurce");
                }
                else
                {
                    var newEvnt = TakeDamageEvent.Create(evnt.Entity.Source);
                    newEvnt.Damage = evnt.Damage;
                    newEvnt.Entity = evnt.Entity;
                    newEvnt.Send();
                }
            }
    }
}