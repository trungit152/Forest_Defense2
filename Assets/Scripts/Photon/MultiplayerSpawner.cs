using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerSpawner : MonoBehaviour
{

    [SerializeField] private GameObject _playerPrefab;

    [SerializeField] private List<Transform> _spawnPosList;

    [SerializeField] private Joystick joystick;
    [SerializeField] private TextMeshProUGUI reviveText;
    [SerializeField] private GameObject revivePanel;
    [SerializeField] private Transform deskPosition;
    [SerializeField] private Transform expTarget;
    [SerializeField] private RectTransform expBar;
    [SerializeField] private RectTransform glow;
    [SerializeField] private RectTransform expBarBorder;
    [SerializeField] private Image chooseTalentPanel;
    [SerializeField] private List<TalentCardsUI> talentCardsUI;
    [SerializeField] private EnemiesAnimator enemiesAnimator;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI chooseTalentsText;
    [SerializeField] private GameObject levelUpBanner;
    
    [SerializeField] private GameObject player;

    
    public static Player localPlayer;

    void Start()
    {
        InitializePlayer(player);
        return;
    }

    private void InitializePlayer(GameObject player)
    {
        localPlayer = player.GetComponent<Player>();
        localPlayer.Init(joystick, reviveText, revivePanel, deskPosition, expTarget, expBar, glow, expBarBorder, chooseTalentPanel,
            talentCardsUI, enemiesAnimator, levelText, chooseTalentsText, levelUpBanner);
    }
}