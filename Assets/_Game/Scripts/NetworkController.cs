using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

using Hastable = ExitGames.Client.Photon.Hashtable;
using ExitGames.Client.Photon;

public class NetworkController : MonoBehaviourPunCallbacks
{
    public Lobby lobbyScript;
    public byte maxPlayers = 2;

    public override void OnEnable()
    {
        base.OnEnable();

        CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimeIsExpired;
    }

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        CountdownTimer.OnCountdownTimerHasExpired -= OnCountdownTimeIsExpired;
    }

    void OnCountdownTimeIsExpired()
    {
        StartGame();
    }

    public override void OnConnected()
    {
        Debug.Log("OnConnected");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        lobbyScript.PanelLobbyActive();
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");

        string roomName = "Room" + Random.Range(1000, 10000);
        RoomOptions roomOptions = new RoomOptions()
        {
            IsOpen = true,
            IsVisible = true,
            MaxPlayers = maxPlayers
        };

        PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom");

        if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayers)
        {
            foreach (var item in PhotonNetwork.PlayerList)
            {
               if (item.IsMasterClient)
               {
                    Hastable props = new Hastable {
                        {CountdownTimer.CountdownStartTime, (float) PhotonNetwork.Time}
                    };

                    PhotonNetwork.CurrentRoom.SetCustomProperties(props);
               }
            }
        }
    }

    public override void OnRoomPropertiesUpdate(Hastable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey(CountdownTimer.CountdownStartTime))
        {
            lobbyScript.lobbyCountdown.gameObject.SetActive(true);
            lobbyScript.lobbyWaiting.gameObject.SetActive(false);
        }
    }

    void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected: " + cause.ToString());
        lobbyScript.PanelLoginActive();
    }


    public void ButtonCancel()
    {
        lobbyScript.loginStatus.gameObject.SetActive(false);
        PhotonNetwork.Disconnect();
    }

    public void ButtonPlay()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = lobbyScript.playerInputField.text;
        lobbyScript.loginStatus.gameObject.SetActive(true);
    }
}
