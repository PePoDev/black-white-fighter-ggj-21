using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class NetworkManagerTest : MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks, IOnEventCallback
{
    [SerializeField]
    string m_RoomName = "Default";

    [SerializeField]
    byte m_MaxPlayers = 10;

    // private readonly LoadBalancingClient client ;
    [SerializeField]
    TMP_Text errorText;

    [SerializeField]
    TMP_Text roomNameText;

    [SerializeField]
    Transform playerListContent;

    private bool quit;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    void IConnectionCallbacks.OnConnected()
    {
        Debug.Log("Connected.");
    }

    void IConnectionCallbacks.OnConnectedToMaster()
    {
        Debug.Log("Connected to master.");

        var roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = m_MaxPlayers;

        // roomOptions.MaxPlayers = m_MaxPlayers;
        PhotonNetwork .JoinOrCreateRoom(m_RoomName, roomOptions, TypedLobby.Default);
    }

    void IMatchmakingCallbacks.OnCreatedRoom()
    {
        Debug.Log("Room created.");
    }

    public void OnConnectedToMaster()
    {
        Console.WriteLine("OnConnectedToMaster Server:");
    }

    public void OnConnected()
    {
    }

    public void OnDisconnected(DisconnectCause cause)
    {
    }

    public void OnRegionListReceived(RegionHandler regionHandler)
    {
    }

    public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
    }

    public void OnCustomAuthenticationFailed(string debugMessage)
    {
    }

    void IMatchmakingCallbacks.OnFriendListUpdate(List<FriendInfo> friendList)
    {
        throw new NotImplementedException();
    }

    void IMatchmakingCallbacks.OnCreateRoomFailed(
        short returnCode,
        string message
    )
    {
        throw new NotImplementedException();
    }

    void IMatchmakingCallbacks.OnJoinedRoom()
    {
        Debug.Log("[PUN] joined room " + PhotonNetwork.CurrentRoom);
        Debug
            .Log("[PUN] CurrentRoom room player count :" +
            PhotonNetwork.CurrentRoom.PlayerCount);

        byte eventCode = PhotonNetwork.CurrentRoom.PlayerCount;
        RaiseEventOptions raiseEventOptions =
            new RaiseEventOptions { Receivers = ReceiverGroup.All }; // 	Everyone in the current room (including this peer) will get this event.
        PhotonNetwork
            .RaiseEvent(eventCode,
            "hello",
            raiseEventOptions,
            SendOptions.SendReliable);
        // roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        // Player[] players = PhotonNetwork.PlayerList;
    }

    void IMatchmakingCallbacks.OnJoinRoomFailed(
        short returnCode,
        string message
    )
    {
        if (returnCode == ErrorCode.NoRandomMatchFound)
        {
            Debug.Log("Failed to join room.");
        }
    }

    void IMatchmakingCallbacks.OnJoinRandomFailed(
        short returnCode,
        string message
    )
    {
        throw new NotImplementedException();
    }

    void IMatchmakingCallbacks.OnLeftRoom()
    {
        throw new NotImplementedException();
    }

    // call back form raise event
    void IOnEventCallback.OnEvent(EventData photonEvent)
    {
        switch (photonEvent.Code)
        {
            case 1:
                Debug.Log("Got called from " + photonEvent.CustomData);
                break;
        }
    }
}
