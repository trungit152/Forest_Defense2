using System.Collections;
using Photon.Pun;
using TMPro;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class OnlineModeGamePlay : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI txtPlayer;
    [SerializeField] private TextMeshProUGUI txtCompetitor;
    [SerializeField] private TextMeshProUGUI txtPlayerKill;
    [SerializeField] private TextMeshProUGUI txtCompetitorKill;
    [SerializeField] private TextMeshProUGUI tmpTime;
    [SerializeField] private int totalTime = 300;

    private float currentTime;
    private bool isFinish;
    
    private void Start()
    {
        if (GlobalController.CurrentModeGame == GlobalController.ModeGame.ModeOffline)
        {
            return;
        }
        
        SetScore(0);
        GlobalController.OnEnemyDie += (scoreToAdd) =>
        {
            SetScore(scoreToAdd);
        };
        InitTimePlay(totalTime);
    }

    private void SetScore(int score)
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Score"))
        {
            int current = (int)PhotonNetwork.LocalPlayer.CustomProperties["Score"];
            current += score;
            Hashtable scoreUpdate = new Hashtable();
            scoreUpdate["Score"] = current;
            PhotonNetwork.LocalPlayer.SetCustomProperties(scoreUpdate);
        }
    }
    
    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player player, Hashtable changedProps)
    {
        UpdateUI();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player player)
    {
        UpdateUI();

        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
        }

        Debug.Log("Join map");
    }
    
    private void UpdateUI()
    {
        var players = PhotonNetwork.PlayerList;

        Photon.Realtime.Player user;
        Photon.Realtime.Player competitor;

        if (players.Length == 2)
        {
            if (players[0] == PhotonNetwork.LocalPlayer)
            {
                user = players[0];
                competitor = players[1];
            }
            else
            {
                competitor = players[0];
                user = players[1];
            }
        
            txtPlayer.text = user.NickName;
            txtCompetitor.text = competitor.NickName;
            txtPlayerKill.text = user.CustomProperties["Score"].ToString();
            txtCompetitorKill.text = competitor.CustomProperties["Score"].ToString();
        }
        else if (players.Length == 1)
        {
            user = players[0];
            txtPlayer.text = user.NickName;
            txtPlayerKill.text = user.CustomProperties["Score"].ToString();
        }
    }

    public void InitTimePlay(int totalSeconds)
    {
        currentTime = totalSeconds;
        UpdateTimeDisplay();
        StartCoroutine(IEOnStartCountDown());
    }
    
    private IEnumerator IEOnStartCountDown()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            currentTime--;
            UpdateTimeDisplay();
        }

        currentTime = 0;
        UpdateTimeDisplay();

        OutOfTime();
    }
    
    private void UpdateTimeDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
            
        tmpTime.text = $"{minutes:00}:{seconds:00}";
            
        if(minutes == 0 && seconds <= 10)
        {
            tmpTime.color = Color.red;
        }
        else
        {
            tmpTime.color = Color.white;
        }
    }

    private void OutOfTime()
    {
        
        int playerScore = int.Parse(txtPlayerKill.text);
        int competitorScore = int.Parse(txtCompetitorKill.text);
        
        if (playerScore >= competitorScore)
        {
            InGameUI.instance.ShowOnlineWinPanel();
        }
        else
        {
            InGameUI.instance.ShowLosePanel();
        }
    }
}
