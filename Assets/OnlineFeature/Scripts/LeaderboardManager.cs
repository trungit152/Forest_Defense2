using System;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using System.Threading.Tasks;
using UnityEngine.UI;
using System.Text;
using TMPro;
using Unity.Services.Leaderboards.Models;

public class LeaderboardManager : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button btnRankBoard;
    [SerializeField] private Button closeRankBoard;
    [SerializeField] private Button btnOpenPanelChangName;
    [SerializeField] private Button btnChangeName;
    
    [Header("UI")]
    [SerializeField] private GameObject rankUI;
    [SerializeField] private Transform content;
    [SerializeField] private TextMeshProUGUI txtUserName;
    [SerializeField] private GameObject panelChangeName;
    [SerializeField] private TMP_InputField inputField;
    
    [Header("Prefab")]

    [SerializeField] private RankObject rankObjectPrefab;

    private void Awake()
    {
        btnRankBoard.onClick.AddListener(OpenRankBoard);
        closeRankBoard.onClick.AddListener(CloseRankBoard);
        btnChangeName.onClick.AddListener(OnChangeName);
        btnOpenPanelChangName.onClick.AddListener(OpenPanelChangName);
    }
    
    async void Start()
    {
        await RankingFeature.InitServices();
        await RankingFeature.SignInAnonymously();
        if (AuthenticationService.Instance.IsSignedIn)
        {
            Debug.Log(Application.persistentDataPath);
        }

        if (string.IsNullOrEmpty(AuthenticationService.Instance.PlayerName))
        {
            Debug.Log(Application.persistentDataPath + AuthenticationService.Instance.PlayerName);
        }
        
        string userName = await AuthenticationService.Instance.GetPlayerNameAsync();
        
        txtUserName.text = userName.Split('#')[0];
    }

    private void OnDestroy()
    {
        btnRankBoard.onClick.RemoveListener(OpenRankBoard);
        closeRankBoard.onClick.RemoveListener(CloseRankBoard);
        btnChangeName.onClick.RemoveListener(OnChangeName);
        btnOpenPanelChangName.onClick.RemoveListener(OpenPanelChangName);
    }
    
    private void OpenRankBoard()
    {
        rankUI.SetActive(true);
        CreateRankBoard();
    }

    private void CloseRankBoard()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        
        rankUI.SetActive(false);
    }

    private void OpenPanelChangName()
    {
        panelChangeName.SetActive(true);
    }
    
    private void OnChangeName()
    {
        string newName = inputField.text.Replace(" ", "");
        AuthenticationService.Instance.UpdatePlayerNameAsync(newName);
        txtUserName.text = newName.Split('#')[0];
        panelChangeName.SetActive(false);
    }
    
    private async void CreateRankBoard()
    {
        LeaderboardScoresPage scores =  await RankingFeature.CreateRankBoard();
        
        var currentPlayerId = AuthenticationService.Instance.PlayerId;
        
        foreach (var entry in scores.Results)
        {
            var rankObj = Instantiate(rankObjectPrefab, content);
            int rank = entry.Rank + 1;
            string name = entry.PlayerName ?? entry.PlayerId;
            int coin = (int)entry.Score;
            
            bool isMyRank = currentPlayerId == entry.PlayerId;
            rankObj.SetRankParam(rank, name, coin, isMyRank);
        }
    }
}
