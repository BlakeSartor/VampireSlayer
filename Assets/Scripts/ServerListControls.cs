using Bolt;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerListControls : GlobalEventListener
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BoltLauncher.Shutdown();
            SceneManager.LoadSceneAsync("Menu");
        }       
    }
}
