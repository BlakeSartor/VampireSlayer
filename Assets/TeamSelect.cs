using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamSelect : Bolt.EntityEventListener<ISlayerState>
{

    public void JoinSlayers()
    {
        Debug.Log("Selecting team slayer");

        if (!PlayerPrefs.GetString("team").Equals("slayer"))
        {
            var spawnPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(15f, 30f), 0f);
            BoltConsole.Write("Selecting team slayer");
            PlayerPrefs.SetString("team", "slayer");
            BoltNetwork.Destroy(gameObject);
            var playerCam = BoltNetwork.Instantiate(BoltPrefabs.Slayer, spawnPosition, Quaternion.identity);

        }
        else
        {
            BoltConsole.Write("already team slayer");
        }
    }

    public void JoinVampires()
    {
        Debug.Log("Selecting team vamp");

        if (!PlayerPrefs.GetString("team").Equals("vampire"))
        {
            var spawnPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(15f, 30f), 0f);
            BoltConsole.Write("Selecting team vampire");
            PlayerPrefs.SetString("team", "vampire");
            BoltNetwork.Destroy(gameObject);
            var playerCam = BoltNetwork.Instantiate(BoltPrefabs.Vampire, spawnPosition, Quaternion.identity);
        }
        else
        {
            BoltConsole.Write("already team vampire");
        }
    }
}
