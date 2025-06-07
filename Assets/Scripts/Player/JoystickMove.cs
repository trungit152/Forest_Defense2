using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickMove : MonoBehaviour
{
    [SerializeField] private Joystick _movementJoystick;
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _followPoint;
    private Status _status;
    private bool _isMoving;
    private enum Status
    {
        Idle,
        Walk
    }
    //public static JoystickMove instance;

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
    private ShootObjects _shootObject;
    public ShootObjects ShootObjects
    {
        get
        {
            if (_shootObject == null)
            {
                _shootObject = GetComponent<ShootObjects>();
            }
            return _shootObject;
        }
        set
        {
            _shootObject = value;
        }
    }
    private PlayerAttack _playerAttack;
    private PlayerAttack PlayerAttack
    {
        get
        {
            if(_playerAttack == null)
            {
                _playerAttack = GetComponent<PlayerAttack>();
            }
            return (_playerAttack);
        }
        set
        {
            _playerAttack = value;
        }
    }
    private void FixedUpdate()
    {
        MoveByJoystick();
    }

    private void MoveByJoystick()
    {
        if(PlayerAttack != null)
        {
            if (_movementJoystick != null && _rigidbody != null)
            {
                if (_movementJoystick.Direction != Vector2.zero)
                {
                    _isMoving = true;
                    if (_status != Status.Walk && !PlayerSpineController.IsPlayingAttack())
                    {
                        _status = Status.Walk;
                        PlayerSpineController.SetWalk();
                    }
                    //handle follow point
                    if (!_followPoint.gameObject.activeSelf)
                    {
                        _followPoint.gameObject.SetActive(true);
                    }
                    _followPoint.localPosition = _movementJoystick.Direction.normalized;

                    _rigidbody.velocity = new Vector2(_movementJoystick.Direction.x, _movementJoystick.Direction.y).normalized * _speed * GameStat.gameTimeScale * Time.deltaTime;
                }
                else
                {
                    //handle follow point
                    if (_followPoint.gameObject.activeSelf)
                    {
                        _followPoint.gameObject.SetActive(false);
                    }

                    _isMoving = false;
                    if (_status != Status.Idle && !PlayerSpineController.IsPlayingAttack())
                    {
                        _status = Status.Idle;
                        PlayerSpineController.SetIdle();
                    }
                    _rigidbody.velocity = Vector2.zero;
                }
                if (!PlayerAttack.IsHavingEnemyInRange() && _movementJoystick.Direction != Vector2.zero)
                {
                    CheckLeftRightRotation();
                }
                else if (PlayerAttack.IsHavingEnemyInRange())
                {
                    PlayerAttack.FaceToEnemy();
                }
            }
        }
    }
    public void CheckLeftRightRotation()
    {
        PlayerSpineController.CheckLeftRightRotation(_movementJoystick.Direction);
    }
    public bool IsMoving()
    {
        return _isMoving;
    }
    public void TurnOffJoystick(bool b = true)
    {
        _movementJoystick.gameObject.SetActive(!b);
    }
    public void ResetJoystick()
    {
        _movementJoystick.OnPointerUp(new PointerEventData(EventSystem.current));
    }

    public void Init(Joystick joystick)
    {
        _movementJoystick = joystick;
    }
}