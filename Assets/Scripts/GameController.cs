using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using TMPro;
using System.Linq;

public class GameController : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI rank, activity, hp;

    public Transform[] spawnLocation;

    public bool MasterMode = false;
    public GameObject MasterCamera;

    private float currentSurviveTime = 0f;
    private Dictionary<string, float> score = new Dictionary<string, float>();
    private List<string> activitys = new List<string>();

    private PhotonView _pv;
    private static GameController instance;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
        _pv = GetComponent<PhotonView>();
        instance = this;

        if (MasterMode)
        {
            MasterCamera.SetActive(true);
            hp.gameObject.SetActive(false);
            PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
            return;
        }

        Random.InitState(System.DateTime.Now.Millisecond);
        if(Random.Range(0, 2) == 1){
   PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerBlack"), spawnLocation[Random.Range(0, spawnLocation.Length)].position, Quaternion.identity);
        }
     else{
  PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerWhite"), spawnLocation[Random.Range(0, spawnLocation.Length)].position, Quaternion.identity);
     }
        // Annouce($"{PhotonNetwork.NickName} Joined");
    }

    private void Update()
    {
        var sortedScore = (from entry in score orderby entry.Value descending select entry).ToList();
        bool meInTopFive = false;

        rank.text ="";
        for (int i = 0; i < Mathf.Min(sortedScore.Count, 5); i++)
        {
            rank.text += $"{i + 1}. {GetFormatNameIfIsMine(i)}\n";
        }

        if (MasterMode) return;
        currentSurviveTime += Time.deltaTime;
        // _pv.RPC("RPC_UpdateRanking", RpcTarget.AllBufferedViaServer, PhotonNetwork.NickName, currentSurviveTime);
        score[PhotonNetwork.NickName] = currentSurviveTime;

        string GetFormatNameIfIsMine(int idx)
        {
            if (PhotonNetwork.NickName.Equals(sortedScore[idx].Key))
            {
                meInTopFive = true;
                return $"<b>{sortedScore[idx].Key}</b> - {GetTimeFormat(sortedScore[idx].Value)}";
            }

            if (idx == 4 && meInTopFive == false)
            {
                return $"<b>{PhotonNetwork.NickName}</b> - {GetTimeFormat(sortedScore.Single(x => x.Key == PhotonNetwork.NickName).Value)}";
            }

            return $"{sortedScore[idx].Key} - {GetTimeFormat(sortedScore[idx].Value)}";
        }

        string GetTimeFormat(float second) => System.TimeSpan.FromSeconds(second).ToString("m\\:ss\\.ff");
    }
    public static GameController Main() => instance;

    public void SetAmmo(int number) => Debug.Log($"Set ammo to {number}");
    public void SetHP(int number) => hp.text = number.ToString();

    // public override void OnLeftRoom() => Annouce($"{PhotonNetwork.NickName} Leaved");
    public void Annouce(string message) => _pv.RPC("RPC_Annoucement", RpcTarget.AllBufferedViaServer, message);

    [PunRPC]
    public void RPC_Annoucement(string message)
    {
        if (activitys.Count >= 5)
        {
            activitys.RemoveAt(0);
        }
        activitys.Add(message);

        activity.text = "";
        foreach (var item in activitys)
        {
            activity.text += item + "\n";
        }
    }

    [PunRPC]
    public void RPC_UpdateRanking(string playerName, float time)
    {
        if (score.ContainsKey(playerName))
        {
            score[playerName] = time;
        }
        else
        {
            score.Add(playerName,time);
        }
    }
}
