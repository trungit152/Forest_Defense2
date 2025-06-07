
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using Unity.Services.Authentication;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI competitorNameText;
    public TextMeshProUGUI textState;
    public async void Start()
    {
        await RankingFeature.InitServices();
        
        if (PhotonNetwork.IsConnectedAndReady)
        {
            JoinOrCreateMyRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnConnectedToMaster()
    {
        JoinOrCreateMyRoom();
    }

    void JoinOrCreateMyRoom()
    {
        RoomOptions options = new RoomOptions { MaxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom("defense", options, TypedLobby.Default);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("JoinFailed");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LocalPlayer.NickName = AuthenticationService.Instance.PlayerName;
        Debug.Log(PhotonNetwork.LocalPlayer.NickName);
        Hashtable playerData = new Hashtable();
        playerData["Score"] = 0;
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerData);
        
        var players = PhotonNetwork.PlayerList;
        
        if(players.Length == 1)
            playerNameText.text = PhotonNetwork.LocalPlayer.NickName;
        if(players.Length == 2)
            competitorNameText.text = PhotonNetwork.LocalPlayer.NickName;
    }

    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player player, Hashtable changedProps)
    {
        UpdateUI();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player player)
    {
        UpdateUI();

        StartCoroutine(IEStart());

    }

    private IEnumerator IEStart()
    {
        yield return new WaitForSeconds(0.1f);

        Debug.Log("players[0].NickName");
        var players = PhotonNetwork.PlayerList;
        Debug.Log(players[1].NickName);
            
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            playerNameText.text = players[0].NickName;
            competitorNameText.text = players[1].NickName;
            textState.text = "Start";
            
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
        }

        yield return new WaitForSeconds(0.5f);
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Level5");
        }
    }
    
    

    void UpdateUI()
    {
        
    }

    public void OnBackMenu()
    {

        StartCoroutine(IEBack());
    }

    private IEnumerator IEBack()
    {
        PhotonNetwork.LeaveRoom();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Menu");
    }
}
