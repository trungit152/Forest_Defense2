using EasyUI.Toast;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [Header("DragTurret")]
    [SerializeField] private GameObject _turretDragText;
    [SerializeField] private GameObject _stumpList;
    [Header("UI WIN")]
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private Image _winPanelBlur;
    [SerializeField] private RectTransform _winPanelImage;
    [SerializeField] private List<RectTransform> _stars;
    [SerializeField] private Sprite _starOn;
    [SerializeField] private Sprite _starOff;
    [SerializeField] private TextMeshProUGUI _talentPointText;
    [SerializeField] private TextMeshProUGUI _coinText;
    private int _currentMoveIndex;
    private bool _isDoneState;
    private bool _isDoneDrag;
    private bool _isShowing;
    private bool _isWin = false;
    private List<Enemy> _dragEnemy;
    public TutorialState _tutorialState;
    public enum TutorialState
    {
        Move,
        Attack,
        ExpDrop,
        Talent,
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
        _talentText.SetActive(false);
        _tutorialState = TutorialState.Move;
        _isSpawned = false;
        _enemyOnTruePosition = false;
        _isSpawnFocus = false;
        _isSetTalent = false;
        _isShowing = false;
        _isDoneDrag = false;
        _isSetExpDropObject = false;
        _isDoneState = false;
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
        //SpawnEnemiesController.instance.PauseSpawn();
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
                var enemy = CreateEnemy(new Vector3(0, 10, 0), _enemyTarget);
                enemy.ChangeExp(2);
            }
            else
            {
                if (Vector2.Distance(_attackEnemy.transform.position, _enemyTarget.position) < 0.5f)
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
                StartCoroutine(FeelingTools.ZoomInCoroutine(_focusPosition, 1, 0.5f));
                GameStat.ChangeGameTimeScale(1);
                _expText.SetActive(false);
                GoNextState();
        }
    }

    private void HandleTalent()
    {
        if (!_isSetTalent)
        {
            for(int i = 0; i < 2; i++)
            {
                Vector3 pos = (i == 0) ? new Vector3(-2, 12, 0) : new Vector3(2, 13, 0);
                var enemy = CreateEnemy(pos, _target);
                enemy.ChangeExp(2);
            }
            _isSetTalent = true;
        }
        else if (MultiplayerSpawner.localPlayer._playerLevel.GetLevel() == 2)
        {
            _talentText.SetActive(true);
        }

        if(DeskController.instance.CardCount() > 0)
        {
            Debug.Log("1");
            _talentText.SetActive(false);
            GoNextState();
        }
    }
    private void HandleTurretDrag()
    {
        if (!_isShowing && _desk.transform.childCount == 1)
        {
            Debug.Log("2");
            _talentText.SetActive(false);
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
            if(!_isWin)
            {
                _isWin = true;
                ShowWinPanel();
            }
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

    public void ShowWinPanel(int star = 3)
    {
        if (_winPanel.activeSelf)
        {
            return;
        }
        if (AudioManager.instance != null)
        {
            AudioManager.instance.FadeOutMusic("CombatMusic");
            AudioManager.instance.PlaySoundEffect("Win");
        }
        //Set info off
        foreach (var starItem in _stars)
        {
            starItem.gameObject.SetActive(false);
        }
        _talentPointText.gameObject.SetActive(false);
        _coinText.gameObject.SetActive(false);
        //Start show
        _winPanel.SetActive(true);
        StartCoroutine(FeelingTools.FadeInCoroutine(_winPanelBlur, 0.15f, 0.8f));
        StartCoroutine(FeelingTools.ZoomInUI(_winPanelImage, 0.5f, 1f, 0.3f, 1.3f));
        StartCoroutine(WaitForSecond(0.7f, () => ShowStar(star)));

    }

    public void ShowStar(int star)
    {
        StartCoroutine(ShowStarCoroutine(star));
    }

    private IEnumerator ShowStarCoroutine(int star)
    {
        for (int i = 0; i < _stars.Count; i++)
        {
            if (i < star)
            {
                _stars[i].GetComponent<Image>().sprite = _starOn;
            }
            else
            {
                _stars[i].GetComponent<Image>().sprite = _starOff;
            }

            StartCoroutine(FeelingTools.ZoomInUI(_stars[i], 0.2f, 1f, 0.25f, 1.5f));

            yield return new WaitForSeconds(0.25f);
        }
        //show talent point
        StartCoroutine(FeelingTools.ShowNumberText(_talentPointText, 0, star, 1f));
        SaveGame.SaveTalentPoint.AddTalentPoint(star);
        // show coin
        StartCoroutine(FeelingTools.ShowNumberText(_coinText, 0, star * 582, 1f));
        SaveGame.SaveTalentPoint.AddCoin(star * 582);
    }
    private IEnumerator WaitForSecond(float second, Action callBack = null)
    {
        yield return new WaitForSeconds(second);
        callBack?.Invoke();
    }

    public void LoadMenu()
    {
        AudioManager.instance.FadeOutMusic("CombatMusic", 1f);
            SceneManager.LoadScene("Menu");

    }
}
