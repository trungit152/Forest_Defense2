using EasyUI.Toast;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SpawnEnemiesController : MonoBehaviour
{
    [Header("Wave amount")]
    [SerializeField] private float _timeAfterEachWave = 5;
    [SerializeField] private List<EnemyWave> _waves;
    [SerializeField] private List<Transform> _targets;
    [SerializeField] private int _enemyID;

    //[Header("Wave UI")]
    //[SerializeField] private TextMeshProUGUI _waveText;
    //[SerializeField] private TextMeshProUGUI _waveCounterText;
    //[SerializeField] private GameObject _waveCounterImage;
    //[SerializeField] private GameObject _bossIncoming;
    //[SerializeField] private GameObject _warningScreen;
    //[SerializeField] private RectTransform _bossHealthBar;
    //[SerializeField] private RectTransform _bossHealthBarBorder;

    private EnemyWave _currentWave;
    private int _currentTurnIndex;
    private bool _endWave;
    private bool _endGame;
    private float _currentWaveCounter;
    private float _totalWaveCounter;
    private float _totalWaveTime;
    private float _timeAfterEachWaveCounter;
    private Vector3 topScreenPosition;
    private bool isStart;
    private bool _paused;


    public static SpawnEnemiesController instance;

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
    private void Start()
    {
        Init();      
        topScreenPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0));
    }

    private void Init()
    {
        CaculateWavesTotalTime();
        _endWave = false;
        _paused = false;
        _endGame = false;
        isStart = true;
        _currentWave = _waves[0];
        _currentTurnIndex = 0;
        _timeAfterEachWaveCounter = 0;
        //if(_waveCounterImage != null)
        //{
        //    _waveCounterImage.SetActive(false);
        //}
        foreach (var wave in _waves)
        {
            _totalWaveTime += wave.totalTime;
        }
    }

    //private void UpdateWaveText()
    //{
    //    if(_waveText != null)
    //    {
    //        //_waveText.text = "Wave " + (_waves.IndexOf(_currentWave) + 1).ToString() + "/" + _waves.Count.ToString();
    //    }
    //}
    //private void UpdateWaveCounterText()
    //{
    //    if(_waveCounterImage != null && _waveCounterText != null)
    //    {
    //        if (_timeAfterEachWaveCounter >= _timeAfterEachWave - 3.5f)
    //        {
    //            _waveCounterImage.SetActive(true);
    //            _waveCounterText.text = $"{MyMath.FormatTime(_timeAfterEachWave - _timeAfterEachWaveCounter)}";
    //        }
    //    }
    //}
    public void WaveBarProcess(Slider slider)
    {
        float delta = _totalWaveCounter / _totalWaveTime;
        if (delta < 1)
        {
            slider.value = delta;
        }
    }
    private void CaculateWavesTotalTime()
    {
        foreach (var wave in _waves)
        {
            wave.CaculateTotalTime();
        }
    }
    private void Update()
    {
        if (!_paused)
        {
            if (!_endGame)
            {
                if (!_endWave)
                {
                    SpawnWaves();
                }
                else
                {
                    //UpdateWaveCounterText();
                    WaitAfterEachWave();
                }
                //UpdateWaveText();
            }
        }

        //db
        if (Input.GetMouseButtonDown(1))
        {
            for (int i = 0; i < 1; i++)
            {
                Enemy enemy = EnemiesController.instance.CreateEnemy(_enemyID);
                if (enemy != null)
                {
                    enemy.gameObject.SetActive(true);
                    enemy.transform.position = MouseController.instance.GetMouseWorldPosition();
                    enemy.Init();
                    enemy.SetTarget(_targets);
                }
            }
        }
    }
    private void SpawnWaves()
    {
        if (isStart)
        {
            _totalWaveCounter += Time.deltaTime * GameStat.gameTimeScale;
            _currentWaveCounter += Time.deltaTime * GameStat.gameTimeScale;
            if (!_currentWave.waveData[_currentTurnIndex].isSpawned && _currentWaveCounter >= _currentWave.waveData[_currentTurnIndex].turnTime)
            {
                _currentWave.waveData[_currentTurnIndex].isSpawned = true;
                StartCoroutine(SpawnWave());
            }
        }
    }
    private void WaitAfterEachWave()
    {
        _timeAfterEachWaveCounter += Time.deltaTime * GameStat.gameTimeScale;
        if (_timeAfterEachWaveCounter >= _timeAfterEachWave)
        {
            _timeAfterEachWaveCounter = 0;
            //if(_waveCounterImage != null)
            //{
            //    _waveCounterImage.SetActive(false);
            //}
            GoNextWave();
        }
    }
    public void CheckWin()
    {
        if (_currentWave == _waves[_waves.Count - 1])
        {
            Toast.Show("WIN", 0.5f, ToastColor.Blue, ToastPosition.MiddleCenter);
        }
    }
    public IEnumerator SpawnWave()
    {
        //if (_currentWave.waveData[_currentTurnIndex].isBoss)
        //{
        //    StartCoroutine(ShowBossIncoming(1.5f));
        //    StartCoroutine(ShowWarning(5));
        //}
        for (int i = 0; i < _currentWave.waveData[_currentTurnIndex].enemyAmount; i++)
        {
            Enemy enemy = EnemiesController.instance.CreateEnemy(_currentWave.waveData[_currentTurnIndex].enemy);
            if (enemy != null)
            {
                if (_currentWave.waveData[_currentTurnIndex].isBoss)
                {
                    //enemy.SetHpBar(_bossHealthBarBorder, _bossHealthBar);
                }
                enemy.gameObject.SetActive(true);
                enemy.transform.position = MyMath.RandomVector(topScreenPosition, 3.4f, 0.5f, 0, 0.5f);
                enemy.Init();
                enemy.SetTarget(_targets);
                if (i == _currentWave.waveData[_currentTurnIndex].enemyAmount - 1 && _currentTurnIndex == _currentWave.waveData.Count - 1)
                {
                    enemy.ChangeCardPercent(100);
                }
                yield return new WaitForSeconds(0.8f);
            }
        }
        if (_currentTurnIndex >= _currentWave.waveData.Count - 1)
        {
            if (!CheckLastWave())
            {
                _endWave = true;
                _currentWaveCounter = 0;
            }
            else
            {
                _endGame = true;
            }
        }
        else
        {
            _currentTurnIndex++;
        }
    }
    //private IEnumerator ShowBossIncoming(float time)
    //{
    //    _bossIncoming.SetActive(true);
    //    yield return new WaitForSeconds(time);
    //    _bossIncoming.SetActive(false);
    //}
    //private IEnumerator ShowWarning(float time)
    //{
    //    _warningScreen.SetActive(true);
    //    yield return new WaitForSeconds(time);
    //    _warningScreen.SetActive(false);
    //}
    public void GoNextWave()
    {
        int i = _waves.IndexOf(_currentWave);
        if (i != _waves.Count - 1)
        {
            _currentWave = _waves[i + 1];
            _currentTurnIndex = 0;
            _currentWaveCounter = 0;
            _endWave = false;
        }
    }
    private bool CheckLastWave()
    {
        int i = _waves.IndexOf(_currentWave);
        if (i != _waves.Count - 1)
        {
            return false;
        }
        else return true;
    }
    public void SetStart(bool b = true)
    {
        if (!_endWave)
        {
            isStart = b;
        }
        else
        {
            GoNextWave();
        }
    }
    public int GetWavesAmount()
    {
        return _waves.Count;
    }
    public void PauseSpawn(bool isPause = true)
    {
        _paused = isPause;
    }
    public void JumpToNextWave()
    {
        //neu la turn cuoi cung
        if (_currentTurnIndex == _currentWave.waveData.Count - 1)
        {
            _timeAfterEachWaveCounter = _timeAfterEachWave - 3.49f;
        }
        //neu khong phai turn cuoi
        else
        {
            if (_currentWaveCounter < _currentWave.waveData[_currentTurnIndex].turnTime - 1)
            {
                _currentWaveCounter = _currentWave.waveData[_currentTurnIndex].turnTime - 1;
            }
        }
    }
}
