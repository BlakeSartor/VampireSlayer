using Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerListControls : GlobalEventListener
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            BoltLauncher.Shutdown();
            SceneManager.LoadSceneAsync("Menu");
        }       
    }
}
