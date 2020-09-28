using UnityEngine;
using System.Collections;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
    public override void SceneLoadLocalDone(string scene)
    {
        // randomize a position
        var spawnPosition = new Vector3(Random.Range(-5, 5), 0, 0);

        // instantiate cube
        BoltNetwork.Instantiate(BoltPrefabs.Slayer, spawnPosition, Quaternion.identity);
    }
}