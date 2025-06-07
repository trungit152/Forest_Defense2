
using TMPro;
using UnityEngine;
using Photon.Pun;

public class PhotonWait : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countText;

    void Update()
    {
        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom != null)
        {
            _countText.text = "Players: " + PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        }
        else
        {
            Debug.Log("NULL ROOM");
        }
    }

    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount > 0)
        {
            PhotonNetwork.LoadLevel("Level5"); // Chuyển sang scene chơi game
            Debug.Log("Game started...");
        }
        else
        {
            Debug.LogWarning("Only the host can start the game, and at least 2 players are required.");
        }
    }

}
