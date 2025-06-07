using DamageNumbersPro;
using System.Collections;
using TMPro;
using UnityEngine;
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private SpriteRenderer _healthBar;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private Transform _center;
    [SerializeField] private TextMeshProUGUI _reviveText;
    [SerializeField] private GameObject _revivePanel;
    [SerializeField] private GameObject _childPart;
    [SerializeField] private DamageNumber _numberPrefab;
    [SerializeField] private CapsuleCollider2D _healthCollider;
    [SerializeField] private GameObject _joystick;

    [SerializeField] private float _reviveTime = 10;
    //public static PlayerHealth instance;
    private float _curHealth;
    private Vector2 _healthBarSize;
    private bool _isImmortal = false;
    private bool _isDeath;

    private PlayerAnimation _playerAnimation;
    public PlayerAnimation PlayerAnimation
    {
        get
        {
            if (_playerAnimation == null)
            {
                _playerAnimation = GetComponent<PlayerAnimation>();
            }
            return _playerAnimation;
        }
        set
        {
            _playerAnimation = value;
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
    }
    //private void Awake()
    //{
    //    if(instance != null && instance != this)
    //    {
    //        Destroy(this);
    //    }
    //    else
    //    {
    //        instance = this;
    //    }
    //}
    private void Start()
    {
        _curHealth = _health;
        _healthBarSize = _healthBar.size;
        UpdateHealthBar();
    }
    public void LoseHealth(float damage)
    {
        if (!_isImmortal) 
        {
            _curHealth -= damage;
            if (_numberPrefab != null)
            {
                DamageNumber damageNumber = _numberPrefab.Spawn(_center.position + new Vector3(0, 0.5f), damage);
            }
            if (_curHealth > 0)
            {
                if (PlayerAnimation != null)
                {
                    PlayerAnimation.SetHit();
                }
            }
            else
            {
                Die();
            }
            UpdateHealthBar();
        }
    }
    private void Die()
    {
        _curHealth = 0;
        PlayerSpineController.StopAttack();
        _isDeath = true;
        UpdateHealthBar();
        StartCoroutine(WaitForRevive(_reviveTime));
        _revivePanel.SetActive(true);
        _reviveText.gameObject.SetActive(true);
        _childPart.SetActive(false);
        _healthCollider.enabled = false;
        _joystick.SetActive(false);
        PlayerAttack.instance.SetCanAttack(false);
        MultiplayerSpawner.localPlayer.JoystickMove.ResetJoystick();
    }
    private void Revive()
    {
        StartCoroutine(SetImmortal());
        _revivePanel.SetActive(false);
        _reviveText.gameObject.SetActive(false);
        _curHealth = _health;
        _childPart.SetActive(true);
        UpdateHealthBar();
        gameObject.transform.position = Vector3.zero - new Vector3(0,4,0);
        _healthCollider.enabled = true;
        _joystick.SetActive(true);
        PlayerAttack.instance.SetCanAttack(true);
        _isDeath = false;
    }
    private void UpdateHealthBar()
    {
        _healthBar.size = new Vector2((_curHealth > 0 ? _curHealth : 0) / _health * _healthBarSize.x, _healthBarSize.y);
    }
    public Vector2 GetPlayerPosition()
    {
        return transform.position;
    }
    public Transform GetPlayerCenter()
    {
        return _center;
    }
    
    private IEnumerator WaitForRevive(float reviveTime)
    {
        float elapsedTime = 0;
        while (elapsedTime < reviveTime) 
        {
            elapsedTime += Time.deltaTime * GameStat.gameTimeScale;
            _reviveText.text = MyMath.RoundToDecimals(reviveTime - elapsedTime, 0).ToString();
            yield return null;
        }
        Revive();
    }
    private IEnumerator SetImmortal(float time = 3f)
    {
        _isImmortal = true;
        yield return new WaitForSeconds(time);
        _isImmortal = false;
    }
    public bool IsDeath() { return _isDeath; }

    public void Init(TextMeshProUGUI reviveText, GameObject revivePanel, GameObject joystick)
    {
        _reviveText = reviveText;
        _revivePanel = revivePanel;
        _joystick = joystick;
    }

}
