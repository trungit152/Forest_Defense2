using UnityEngine;
using UnityEngine.UI; // Nếu bạn sử dụng UI
using Photon.Pun;
using Photon.Realtime;

public class PhotonLauncher : MonoBehaviourPunCallbacks
{
    private string _roomName;

    // UI Buttons (gắn từ Inspector nếu cần)
    public Button createRoomButton;
    public Button joinRoomButton;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Connecting to Photon...");
        }

        // Disable buttons until connected
        if (createRoomButton != null) createRoomButton.interactable = false;
        if (joinRoomButton != null) joinRoomButton.interactable = false;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server.");
        PhotonNetwork.JoinLobby(); // Phải vào lobby để dùng matchmaking
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby. Ready to create or join rooms.");
        // Enable buttons
        if (createRoomButton != null) createRoomButton.interactable = true;
        if (joinRoomButton != null) joinRoomButton.interactable = true;
    }

    public void CreateRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.NetworkingClient.Server == ServerConnection.MasterServer)
        {
            _roomName = "Room " + Random.Range(1000, 9999);
            RoomOptions options = new RoomOptions { MaxPlayers = 4 };
            PhotonNetwork.CreateRoom(_roomName, options);
            Debug.Log("Creating Room: " + _roomName);
        }
        else
        {
            Debug.LogWarning("Not ready to create room. Wait for OnJoinedLobby.");
        }
    }

    public void JoinRandomRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.NetworkingClient.Server == ServerConnection.MasterServer)
        {
            PhotonNetwork.JoinRandomRoom();
            Debug.Log("Joining Random Room...");
        }
        else
        {
            Debug.LogWarning("Not ready to join room. Wait for OnJoinedLobby.");
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Successfully joined room: " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("WaitScene"); // Chuyển sang scene chơi game
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No room found. Creating new room...");
        CreateRoom(); // Nếu không có phòng nào, tạo phòng mới
    }

    public override void OnCreatedRoom()
    {
        if (PhotonNetwork.CurrentRoom != null)
        {
            Debug.Log("Room created: " + PhotonNetwork.CurrentRoom.Name);
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError("Disconnected from Photon. Reason: " + cause.ToString());
    }
}
