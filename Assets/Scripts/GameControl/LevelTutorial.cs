using EasyUI.Toast;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelTutorial : MonoBehaviour
{
    [SerializeField] private Transform _focusPosition;
    [SerializeField] private Transform _player;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Transform _target;
    [SerializeField] private PickupItem _playerPickUp;
    [SerializeField] private GameObject _desk;
    [Header("Move")]
    [SerializeField] private GameObject _moveText;
    [SerializeField] private GameObject _moveArrow;
    [SerializeField] private List<Transform> _moveList;

    [Header("Attack")]
    [SerializeField] private Transform _enemyTarget;
    [SerializeField] private GameObject _attackText;
    private bool _isSpawned;
    private bool _isSpawnFocus;
    private bool _enemyOnTruePosition;
    private Enemy _attackEnemy;
    [Header("ExpDrop")]
    [SerializeField] private GameObject _expText;
    private bool _isSetExpDropObject;
    [Header("Talent")]
    [SerializeField] private GameObject _talentText;
    private bool _isSetTalent;
    [Header("TurretDrop")]
    [SerializeField] private GameObject _turretDropText;
    private Enemy _turretDropEnemy;
    private bool _isSetTurretDrop;
    private bool _isTurretDropEnemyDie;
    private bool _isEndTurretDrop;
    [Header("DragTurret")]
    [SerializeField] private GameObject _turretDragText;
    [SerializeField] private GameObject _stumpList;
    private int _currentMoveIndex;
    private bool _isDoneState;
    private bool _isDoneDrag;
    private bool _isShowing;
    private List<Enemy> _dragEnemy;
    public TutorialState _tutorialState;
    public enum TutorialState
    {
        Move,
        Attack,
        ExpDrop,
        Talent,
        TurretDrop,
        DragTurret
    }
    private void Start()
    {
        Init();
    }
    private void Update()
    {
        if (!_isDoneState)
        {
            HandleState();
        }
        else
        {
            CheckStateDone();
        }
    }
    private void Init()
    {
        _moveText.SetActive(false);
        _focusPosition.gameObject.SetActive(false);
        _turretDropText.SetActive(false);
        _talentText.SetActive(false);
        _tutorialState = TutorialState.Move;
        _isSpawned = false;
        _enemyOnTruePosition = false;
        _isSpawnFocus = false;
        _isTurretDropEnemyDie = false;
        _isSetTalent = false;
        _isSetTurretDrop = false;
        _isShowing = false;
        _isDoneDrag = false;
        _isSetExpDropObject = false;
        _isDoneState = false;
        _isEndTurretDrop = false;
    }
    private void HandleState()
    {
        switch (_tutorialState)
        {
            case TutorialState.Move:
                HandleMove();
                break;

            case TutorialState.Attack:
                HandleAttack();
                break;

            case TutorialState.ExpDrop:
                HandleExpDrop();
                break;

            case TutorialState.Talent:
                HandleTalent();
                break;

            case TutorialState.TurretDrop:
                HandleTurretDrop();
                break;

            case TutorialState.DragTurret:
                HandleTurretDrag();
                break;
        }
    }

    private void CheckStateDone()
    {
        switch (_tutorialState)
        {
            case TutorialState.Move:
                CheckMoveDone();
                break;
        }
    }
    private void HandleMove()
    {
        _moveText.SetActive(true);
        _moveArrow.SetActive(true);
        _currentMoveIndex = 0;
        _moveArrow.transform.position = _moveList[0].position;
        SpawnEnemiesController.instance.PauseSpawn();
        _isDoneState = true;
    }
    private void CheckMoveDone()
    {
        
        if (Vector2.Distance(_player.position, _moveList[_currentMoveIndex].position) < 1f)
        {
            if (_currentMoveIndex < _moveList.Count - 1)
            {
                _currentMoveIndex++;
                _moveArrow.transform.GetChild(0).GetComponent<Animator>().enabled = false;
                StartCoroutine(FeelingTools.MoveToTarget(_moveArrow.transform, 0.3f, _moveList[_currentMoveIndex].position, PlayMoveArrowAnimator));
            }
            else
            {
                _moveText.SetActive(false);
                _moveArrow.SetActive(false);
                GoNextState();
            }
        }
    }
    private void PlayMoveArrowAnimator()
    {
        _moveArrow.transform.GetChild(0).GetComponent<Animator>().enabled = true;
    }
    private void HandleAttack()
    {
        if (!_enemyOnTruePosition)
        {
            _joystick.OnPointerUp(new PointerEventData(EventSystem.current));
            _joystick.gameObject.SetActive(false);
            if (!_isSpawned)
            {
                CreateEnemy(new Vector3(0, 10, 0), _enemyTarget);
            }
            else
            {
                if (Vector2.Distance(_attackEnemy.transform.position, _enemyTarget.position) < 0.2f)
                {
                    _attackEnemy.EnemyAnimation.SetIdle();
                    _enemyOnTruePosition = true;
                }
            }
        }
        else if(!_isSpawnFocus)
        {
            _focusPosition.transform.localScale = Vector3.one;
            _focusPosition.transform.position = _enemyTarget.position + new Vector3(0,-2.2f);
            StartCoroutine(FeelingTools.ZoomOutCoroutine(_focusPosition, 0.25f, 0.5f));
            _joystick.gameObject.SetActive(true);
            _attackText.SetActive(true);
            _isSpawnFocus = true;
        }
        else
        {
            if (_attackEnemy.IsDie())
            {
                GoNextState();
            }
        }
    }
    private void HandleExpDrop()
    {
        if (!_isSetExpDropObject)
        {
            StartCoroutine(FeelingTools.ZoomOutCoroutine(_focusPosition, 0.04f, 0.75f));
            StartCoroutine(FeelingTools.MoveToTarget(_focusPosition, 0.75f, _enemyTarget.position + new Vector3(0, 0.7f), SetShowingDone));
            _attackText.SetActive(false);
            GameStat.ChangeGameTimeScale(0);
            _isSetExpDropObject = true;
            _expText.SetActive(true);
        }
        else if(_isShowing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(FeelingTools.ZoomInCoroutine(_focusPosition, 1, 0.5f));
                GameStat.ChangeGameTimeScale(1);
                _expText.SetActive(false);
                GoNextState();
            }
        }
    }

    private void HandleTalent()
    {
        if (!_isSetTalent)
        {
            for(int i = 0; i < 3; i++)
            {
                Vector3 pos = (i == 0) ? new Vector3(-2, 12, 0) : new Vector3(2, 13, 0);
                CreateEnemy(pos, _target);
            }
            _isSetTalent = true;
        }
        else if (PlayerLevel.instance.GetLevel() == 2)
        {
            _talentText.SetActive(true);
            GoNextState();
        }
    }
    private void HandleTurretDrop()
    {
        if (GameStat.gameTimeScale != 0 && !_isSetTurretDrop)
        {
            _playerPickUp.SetRadius(0);
            _talentText.SetActive(false);
            _turretDropEnemy = CreateEnemy(new Vector3(0, 10, 0), _target, true);
            _turretDropEnemy.ChangeExpPercent(0);
            _turretDropEnemy.SetIsCardFly(false);
            _turretDropEnemy.ChangeCardPercent(100);
            _isSetTurretDrop = true;
        }
        else if (!_isTurretDropEnemyDie)
        {
            if (_turretDropEnemy!= null && _turretDropEnemy.IsDie())
            {
                _focusPosition.transform.localScale = Vector3.one;
                _focusPosition.transform.position = _turretDropEnemy.transform.position + new Vector3(0, -2.2f);
                StartCoroutine(FeelingTools.ZoomOutCoroutine(_focusPosition, 0.04f, 0.75f));
                StartCoroutine(FeelingTools.MoveToTarget(_focusPosition, 0.75f, _turretDropEnemy.transform.position + new Vector3(0, 0.1f), SetShowingDone));
                _turretDropText.SetActive(true);
                _isTurretDropEnemyDie = true;
                GameStat.ChangeGameTimeScale(0);
                StartCoroutine(WaitToTurnOnClaim(2));
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && !_isEndTurretDrop && _isShowing)
            {
                GameStat.ChangeGameTimeScale(1);
                GameObject.Find("RabbitCard(Clone)").GetComponent<TurretsCard>().Init();
                StartCoroutine(FeelingTools.ZoomInCoroutine(_focusPosition, 1, 0.5f, GoNextState));
                _isEndTurretDrop = true;
            }
        }
    }
    private void HandleTurretDrag()
    {
        if (!_isShowing && _desk.transform.childCount == 1)
        {
            _turretDropText.SetActive(false);
            _turretDragText.SetActive(true);
            _stumpList.SetActive(true);
            _isShowing = true;
        }
        if (_isShowing && _desk.transform.childCount == 0 && !_isDoneDrag) 
        {
            _turretDragText.SetActive(false);
            _stumpList.SetActive(false);
            for (int i = 0; i <3; i++)
            {
                Vector3 pos = Vector3.zero;
                if (i == 0)
                {
                    pos = new Vector3(0, 11, 0);
                }
                else if (i == 1)
                {
                    pos = new Vector3(2, 11.5f, 0);
                }
                else if (i == 2)
                {
                    pos = new Vector3(-1, 11.7f, 0);
                }
                else if (i == 3)
                {
                    pos = new Vector3(3, 10.5f, 0);
                }
                else if (i == 4)
                {
                    pos = new Vector3(-3, 12.3f, 0);
                }
                CreateEnemy(pos, _target);
            }
            _isDoneDrag = true;
        }
        if(_isDoneDrag && EnemiesController.instance.GetAliveEnemyCount() == 0)
        {
            Toast.Show("WIN", 0.5f, ToastColor.Blue, ToastPosition.MiddleCenter);
        }
    }
    private void GoNextState()
    {
        if (_tutorialState < TutorialState.DragTurret)
        {
            _isShowing = false;
            _tutorialState = (TutorialState)(((int)_tutorialState) + 1);
            _isDoneState = false;
        }
    }
    private void SetShowingDone()
    {
        _isShowing = true;  
    }
    private Enemy CreateEnemy(Vector3 pos, Transform target, bool isDropcard = false)
    {
        Enemy enemy = EnemiesController.instance.CreateEnemy(1);
        if (enemy != null)
        {
            enemy.gameObject.SetActive(true);
            enemy.transform.position = pos;
            enemy.Init();
            enemy.SetTarget(target);
            _attackEnemy = enemy;
            _isSpawned = true;
            if (isDropcard)
            {
                enemy.ChangeCardPercent(100);
            }
        }
        return enemy;
    }
    private IEnumerator WaitToTurnOnClaim(float time)
    {
        yield return new WaitForSeconds(time);
        _playerPickUp.SetRadius(2f);
        _isShowing = true;
    }

}
