using SRF;
using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ObjectPool _weaponPool;
    public static Player instance;

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
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
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
