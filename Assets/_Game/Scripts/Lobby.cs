using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    public GameObject painelLogin;
    public GameObject painelLobby;
    public Text lobbyWaiting;
    public Text lobbyCountdown;
    public InputField playerInputField;
    public Text loginStatus;
    public string playerName;

    //public string lobbyCountdownText = "Start Game in 5...";
    
    private void Awake()
    {
        playerName = "Player" + Random.Range(1000, 10000);
        playerInputField.text = playerName;
    }

    void Start()
    {
        lobbyWaiting.gameObject.SetActive(true);
        lobbyCountdown.gameObject.SetActive(false);
        loginStatus.gameObject.SetActive(false);
        PanelLoginActive();
    }

    public void PanelLobbyActive()
    {
        painelLogin.gameObject.SetActive(false);
        painelLobby.gameObject.SetActive(true);
    }

    public void PanelLoginActive()
    {
        painelLogin.gameObject.SetActive(true);
        painelLobby.gameObject.SetActive(false);
    }
}
