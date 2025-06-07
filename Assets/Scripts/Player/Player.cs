using Photon.Pun;
using SRF;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;
    [SerializeField] private ObjectPool _weaponPool;
    public PlayerLevel _playerLevel;
    public PickupItem _pickupItem;
    public PhotonView _photonView;
    //public static Player instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);
        
        _photonView = GetComponent<PhotonView>();
        
    }
    private PlayerAttack _playerAttack;
    public PlayerAttack PlayerAttack
    {
        get
        {
            if (_playerAttack == null)
            {
                _playerAttack = GetComponent<PlayerAttack>();
            }
            return _playerAttack;
        }
        set
        {
            _playerAttack = value;
        }
    }
    private PlayerSpineController _playerSpineController;
    public PlayerSpineController PlayerSpineController
    {
        get
        {
            if (_playerSpineController == null)
            {
                _playerSpineController = GetComponent<PlayerSpineController>();
            }
            return _playerSpineController;
        }
        set
        {
            _playerSpineController = value;
        }
    }

    public JoystickMove _joystickMove;
    public JoystickMove JoystickMove
    {
        get
        {
            if(_joystickMove == null)
            {
                _joystickMove = GetComponent<JoystickMove>();
            }
            return _joystickMove;
        }
    }
    public PlayerHealth _playerHealth;
    public PlayerHealth PlayerHealth
    {
        get
        {
            if( _playerHealth == null)
            {
                _playerHealth = GetComponent<PlayerHealth>();
            }
            return _playerHealth;
        }
    }

    public void ChangeWeapon(int type)
    {
        if (type == 0)
        {

        }
        else if (type == 1)
        {
            StartCoroutine(RemoveAndAddComponent<PlayerAttack, PlayerMeleAttack>((component) =>
            {
                component.Init(1f, 1f, 10);
                _weaponPool.ChangeWeapon(1);
                PlayerSpineController.ChangeSkin(1);
            }));
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeapon(1);
        }
    }

    public void Init(/*JoystickMove: */ Joystick joystick,
        /*PlayerHealth*/ TextMeshProUGUI reviveText, GameObject revivePanel,
    /*PickUpItem*/Transform deskPosition, Transform expTarget,
    /*PlayerLevel*/ RectTransform expBar, RectTransform glow, RectTransform expBarBorder, Image chooseTalentPanel, List<TalentCardsUI> talentCardsUI,
        EnemiesAnimator enemiesAnimator, TextMeshProUGUI levelText, TextMeshProUGUI chooseTalentsText, GameObject levelUpBanner)
    {
        _joystickMove.Init(joystick);
        _playerHealth.Init(reviveText,revivePanel, joystick.gameObject);
        _pickupItem.Init(deskPosition, expTarget);
        _playerLevel.Init(expBar, glow, expBarBorder, chooseTalentPanel, talentCardsUI, enemiesAnimator, levelText, chooseTalentsText, levelUpBanner);
    }
    
    public IEnumerator RemoveAndAddComponent<TRemove, TAdd>(Action<TAdd> onAdded = null)
    where TRemove : Component
    where TAdd : Component
    {
        TRemove compToRemove = GetComponent<TRemove>();
        if (compToRemove != null)
        {
            Destroy(compToRemove);
        }
        yield return null;
        TAdd compToAdd = gameObject.AddComponent<TAdd>();
        onAdded?.Invoke(compToAdd);
    }
}
