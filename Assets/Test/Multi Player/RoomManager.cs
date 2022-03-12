using System.Collections.Generic;
using System.IO;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    public GameObject[] itemList;

    // public float beat = (60 / 130) * 2;
    public float beat = (60 / 130) * 2;

    private float timer;

    void Awake()
    {
        if (Instance)
        {
            Destroy (gameObject);
            return;
        }
        DontDestroyOnLoad (gameObject);
        Instance = this;
		    //     PhotonNetwork
            // .Instantiate(Path.Combine("PhotonPrefabs", "Map"),
            // Vector3.zero,
            // Quaternion.identity);
    }

    void Update()
    {
        // only run on the master client
        if (!PhotonNetwork.IsMasterClient) return;
        // ItemSpawner();
        // UpdateColor();
    }

    public override void OnEnable()
    {
        base.OnEnable();

        // SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        // SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    // {
    // 	if(scene.buildIndex == 1) // We're in the game scene
    // 	{
    // 		PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
    // 	}
    // }
    private void UpdateColor()
    {
        // PhotonView PV = PhotonView.Get(this);
        // PV.RPC("SpawnManager", RpcTarget.All, Color.black);
    }

    private void ItemSpawner()
    {
        if (timer > beat)
        {
            // Debug.Log("ItemSpawner Called");
            PhotonNetwork
                .Instantiate(Path.Combine("PhotonPrefabs", "Item"),
                Vector3.zero,
                Quaternion.identity);
            timer -= beat;
        }
        timer += Time.deltaTime;
    }
	
}
