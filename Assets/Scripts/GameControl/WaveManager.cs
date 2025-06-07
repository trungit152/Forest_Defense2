using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Wave UI")]
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private TextMeshProUGUI _waveCounterText;
    [SerializeField] private GameObject _waveCounterImage;
    [SerializeField] private GameObject _bossIncoming;
    [SerializeField] private GameObject _warningScreen;
    [SerializeField] private RectTransform _bossHealthBar;
    [SerializeField] private RectTransform _bossHealthBarBorder;
    [Space(25)]
    [SerializeField] private List<DataWave> _mainWaves;
    private Dictionary<int, List<DataTurn>> _turnList;
    private float _turnDelayTime;
    private float _enemyDelayTime;
    private float _waveDelayTime;
    private int _currentWave;
    private int _currentTurn;
    private int _currentEnemy;
    private bool _endGame;

    private bool _isLastEnemyInWave;
    public static WaveManager instance;
    void Awake()
    {
        Init();
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Init()
    {
        _endGame = false;
        _isLastEnemyInWave = false;
        _turnDelayTime = 0f;
        _enemyDelayTime = 0f;
        _waveDelayTime = 0f;
        _currentWave = 0;
        _currentTurn = 0;
        _currentEnemy = 0;

        _mainWaves = DataWave.GetWaveByLevel(SaveGame.SaveGameLevel.currentLevel);
        _turnList = new Dictionary<int, List<DataTurn>>();
        foreach (var wave in _mainWaves)
        {
            _turnList.Add(wave.WaveId, new List<DataTurn>());
            foreach (var turnId in wave.TurnId)
            {
                _turnList[wave.WaveId].Add(DataTurn.GetData(turnId));
            }
        }

        InitUI();
    }

    public void InitUI()
    {
        if(SaveGame.SaveGameLevel.currentLevel != 0)
        {
            _waveText.text = $"Wave {(_currentWave + 1)}/{_mainWaves.Count}";
        }
        else
        {
            _waveText.text = $"Wave {_currentWave + 1}";
        }
            _waveCounterImage.SetActive(false);
        _waveText.gameObject.SetActive(true);
        _warningScreen.SetActive(false);
        _bossIncoming.SetActive(false);
        _bossHealthBar.gameObject.SetActive(false);
        _bossHealthBarBorder.gameObject.SetActive(false);
    }
    private void Update()
    {
        if(!_endGame)
        {
            WaveCount();
        }
    }
    private void WaveCount()
    {
        if (_waveDelayTime > 0)
        {
            _waveDelayTime -= Time.deltaTime * GameStat.gameTimeScale;
        }
        else
        {
            _waveCounterImage.SetActive(false);
            TurnCount();
        }
    }
    private void TurnCount()
    {
        if (_turnDelayTime > 0)
        {
            _turnDelayTime -= Time.deltaTime * GameStat.gameTimeScale;
        }
        else
        {
            EnemyCount();
        }
    }

    private void EnemyCount()
    {
        if (_isLastEnemyInWave)
        {
            _enemyDelayTime = 5;
            _isLastEnemyInWave = false;
        }
        if (_enemyDelayTime > 0)
        {
            _enemyDelayTime -= Time.deltaTime * GameStat.gameTimeScale;
            if(_currentTurn == 0 && _currentEnemy ==0)
            {
                _waveCounterImage.SetActive(true);
                _waveCounterText.text = $"{MyMath.FormatTime(_enemyDelayTime)}";
            }
        }
        else
        {
            _waveCounterImage.SetActive(false);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        float x = Random.Range(-3.5f, 3.5f);
        var currentTurnList = _turnList[_mainWaves[_currentWave].WaveId];
        var currentTurn = currentTurnList[_currentTurn];
        var currentEnemy = currentTurn.EnemyId[_currentEnemy];
        var enemy = EnemiesController.instance.SpawnEnemy(new Vector3(x, 10, 0), currentEnemy);
        if (enemy.IsBoss())
        {
            enemy.SetHpBar(_bossHealthBarBorder, _bossHealthBar);
            enemy.UpdateHpBar();
            StartCoroutine(ShowBossIncoming(1.5f));
            StartCoroutine(ShowWarning(5));
        }

        if (_currentEnemy < currentTurn.EnemyId.Length - 1)
        {
            _currentEnemy++;
            currentEnemy = currentTurn.EnemyId[_currentEnemy];
            _enemyDelayTime = currentTurn.DelayTime[_currentEnemy];
        }
        else
        {
            if (_currentTurn < currentTurnList.Count - 1)
            {
                _currentTurn++;
                _currentEnemy = 0;
                currentTurn = currentTurnList[_currentTurn];
                currentEnemy = currentTurn.EnemyId[_currentEnemy];
                _turnDelayTime = currentTurn.DelayTime[_currentEnemy];
                _enemyDelayTime = currentTurn.DelayTime[_currentEnemy];
                
            }
            else
            {
                if (_currentWave < _mainWaves.Count - 1)
                {
                    _currentWave++;
                    if (SaveGame.SaveGameLevel.currentLevel != 0)
                    {
                        _waveText.text = $"Wave {(_currentWave + 1)}/{_mainWaves.Count}";
                    }
                    else
                    {
                        _waveText.text = $"Wave {_currentWave + 1}";
                    }
                    _currentTurn = 0;
                    _currentEnemy = 0;
                    currentTurnList = _turnList[_mainWaves[_currentWave].WaveId];
                    currentTurn = currentTurnList[_currentTurn];
                    currentEnemy = currentTurn.EnemyId[_currentEnemy];

                    _waveDelayTime = _mainWaves[_currentWave].Delay;
                    _turnDelayTime = currentTurn.DelayTime[_currentEnemy];
                    _enemyDelayTime = currentTurn.DelayTime[_currentEnemy];
                    _isLastEnemyInWave = true;
                }
                else
                {
                    if (!_endGame)
                    {
                        _endGame = true;
                    }              
                }
            }
        }
    }
    private IEnumerator ShowBossIncoming(float time)
    {
        _bossIncoming.SetActive(true);
        yield return new WaitForSeconds(time);
        _bossIncoming.SetActive(false);
    }
    private IEnumerator ShowWarning(float time)
    {
        _warningScreen.SetActive(true);
        yield return new WaitForSeconds(time);
        _warningScreen.SetActive(false);
    }

    public bool IsEndGame()
    {
        return _endGame;
    }
}
