using System;
using Bolt.Matchmaking;
using UdpKit;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : Bolt.GlobalEventListener
{
    public GameObject serverListPanel;
    public Button joinGameButtonPrefab;
    public void startServer()
    {
        BoltLauncher.StartServer();
    }

    public void startClient()
    {
        BoltLauncher.StartClient();
    }

    public override void BoltStartDone()
    {
        if (BoltNetwork.IsServer)
        {
            string matchName = Guid.NewGuid().ToString();

            BoltMatchmaking.CreateSession(
                sessionID: matchName,
                sceneToLoad: "Main2"
            );
        }
    }

    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        Debug.LogFormat("Session list updated: {0} total sessions", sessionList.Count);

      
        foreach (var session in sessionList)
        {
            UdpSession photonSession = session.Value as UdpSession;

            Button joinGameButtonClone = Instantiate(joinGameButtonPrefab);
            joinGameButtonClone.transform.parent = serverListPanel.transform;
            joinGameButtonClone.transform.localPosition = new Vector3(0,0,0);
            joinGameButtonClone.gameObject.SetActive(true);

            joinGameButtonClone.onClick.AddListener(() => JoinGame(photonSession));

            /*
            if (photonSession.Source == UdpSessionSource.Photon)
            {
            }
            */
        }
        
    }

    private void JoinGame(UdpSession photonSession)
    {
        BoltMatchmaking.JoinSession(photonSession);

    }

    public void OnSetUserNameValueChanged(string username)
    {

    }
}
