using System.Collections.Generic;
using System.IO;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField]
    TMP_InputField playerName;

    [SerializeField]
    TMP_Text status;

    void Start()
    {
        Debug.Log("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        // MenuManager.Instance.OpenMenu("title");
        Debug.Log("Joined Lobby");

        //TODO Move to ui flow
        var roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 10;

        // roomOptions.MaxPlayers = m_MaxPlayers;
        PhotonNetwork.JoinOrCreateRoom("test", roomOptions, TypedLobby.Default);
        // if (PhotonNetwork.IsMasterClient) {
        // PhotonNetwork
        //     .InstantiateRoomObject(Path.Combine("PhotonPrefabs", "Map"),
        //     Vector3.zero,
        //     Quaternion.identity);
        // }
    }

    public override void OnCreatedRoom()
    {
        // PhotonNetwork
        //     .InstantiateRoomObject(Path.Combine("PhotonPrefabs", "Map"),
        //     Vector3.zero,
        //     Quaternion.identity);
        PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);

        base.OnCreatedRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("[PUN] joined room " + PhotonNetwork.CurrentRoom);
        Debug
            .Log("[PUN] CurrentRoom room player count :" +
            PhotonNetwork.CurrentRoom.PlayerCount);

        // // MenuManager.Instance.OpenMenu("room");
        // roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        // Player[] players = PhotonNetwork.PlayerList;
        // foreach(Transform child in playerListContent)
        // {
        // 	Destroy(child.gameObject);
        // }
        // for(int i = 0; i < players.Count(); i++)
        // {
        // 	Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        // }
        // startGameButton.SetActive(PhotonNetwork.IsMasterClient);
        //    OnPlayerEnteredRoom(null);
            //     PhotonNetwork
            // .Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"),
            // Vector3.zero,
            // Quaternion.identity);
        PhotonNetwork
            .Instantiate(Path.Combine("PhotonPrefabs", "Player Variant"),
            Vector3.zero,
            Quaternion.identity);
        // RequsetSyncMap();
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log ("OnMasterClientSwitched "+newMasterClient);
        // photonView.TransferOwnership(newMasterClient);
        PhotonNetwork.SetMasterClient (newMasterClient);
        // if (!PhotonNetwork.IsMasterClient) return;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
       
        Debug.LogError("Room Creation Failed: " + message);
        // MenuManager.Instance.OpenMenu("error");
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Called OnLeftRoom");
    }

    // TODO : Logic to check & update current player count
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // foreach(Transform trans in roomListContent)
        // {
        // 	Destroy(trans.gameObject);
        // }
        // for(int i = 0; i < roomList.Count; i++)
        // {
        // 	if(roomList[i].RemovedFromList)
        // 		continue;
        // 	Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        // }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // Debug.Log("Called OnPlayerEnteredRoom");
        // PhotonNetwork
        //     .Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"),
        //     Vector3.zero,
        //     Quaternion.identity);
        // Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
}
