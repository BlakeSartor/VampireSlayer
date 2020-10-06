using System;
using System.Collections.Generic;
using Bolt.Matchmaking;
using UdpKit;
using UnityEngine;
using UnityEngine.UI;

public class Menu : Bolt.GlobalEventListener
{
    public GameObject userNameMenuButton;
    public GameObject menuPanel;
    public GameObject setUserNamePanel;
    public GameObject serverListPanel;
    public Button joinGameButtonPrefab;
    public float buttonSpacing;

    public List<Button> joinServerButtonList = new List<Button>();

    public void Start()
    {
        Debug.Log("hmmmm:");
        if (PlayerPrefs.GetString("userName") == null)
        {
            setUserNamePanel.SetActive(true);
            menuPanel.SetActive(false);
        } 
        else
        {
            setUserNamePanel.SetActive(false);
            menuPanel.SetActive(true);
            userNameMenuButton.GetComponentInChildren<Text>().text = PlayerPrefs.GetString("userName");
        }
    }

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
            string matchName = PlayerPrefs.GetString("userName");

            BoltMatchmaking.CreateSession(
                sessionID: matchName,
                sceneToLoad: "Main2"
            );
        }
    }

    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        Debug.LogFormat("Session list updated: {0} total sessions", sessionList.Count);

        ClearSessions();
      
        foreach (var session in sessionList)
        {
            UdpSession photonSession = session.Value as UdpSession;

            Button joinGameButtonClone = Instantiate(joinGameButtonPrefab);
            joinGameButtonClone.transform.SetParent(serverListPanel.transform, false);
            joinGameButtonClone.transform.localPosition = new Vector3(0, buttonSpacing * joinServerButtonList.Count, 0);
            joinGameButtonClone.GetComponentInChildren<Text>().text = session.Value.HostName;
            joinGameButtonClone.gameObject.SetActive(true);

            joinGameButtonClone.onClick.AddListener(() => JoinGame(photonSession));

            joinServerButtonList.Add(joinGameButtonClone);

        }
        
    }

    private void JoinGame(UdpSession photonSession)
    {
        BoltMatchmaking.JoinSession(photonSession);
    }

    private void ClearSessions()
    {
        foreach(Button button in joinServerButtonList)
        {
            Destroy(button.gameObject);
        }
        joinServerButtonList.Clear();
    }

    public void OnSetUserNameValueChanged(string username)
    {
        PlayerPrefs.SetString("userName", username);
        userNameMenuButton.GetComponentInChildren<Text>().text = PlayerPrefs.GetString("userName");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
