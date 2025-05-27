using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private RectTransform _expBar;

    [SerializeField] private RectTransform _glow;
    [SerializeField] private RectTransform _expBarBorder;
    [SerializeField] private Image _chooseTalentPanel;
    [SerializeField] private List<TalentCardsUI> _talentCardsUI;
    [SerializeField] private EnemiesAnimator _enemiesAnimator;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _chooseTalentsText;
    [SerializeField] private GameObject _levelUpBanner;
    private Dictionary<int, float> _levelInformation;
    private Queue<IEnumerator> _claimCoroutine;
    private int _currentLevel;
    private float _currentExp;
    private Coroutine _coroutine;
    private bool _isChoosing;
    private float _speed;
    private int _activeCoroutines;

    public static PlayerLevel instance;
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
        _claimCoroutine = new Queue<IEnumerator>();
    }
    private void Start()
    {
        SetValue();
    }
    private void SetValue()
    {
        _currentExp = 0;
        _currentLevel = 1;
        if (_levelText != null)
        {
            _levelText.text = _currentLevel.ToString();
        }

        if (DataLevel.levelInformation == null)
        {
            DataLevel.GetListData();
        }
        _levelInformation = DataLevel.levelInformation;
        UpdateExpBar();
    }
    private IEnumerator ProcessQueue()
    {
        while (_claimCoroutine.Count > 0)
        {
            yield return StartCoroutine(_claimCoroutine.Dequeue());
        }
        _coroutine = null;
    }
    public void AddClaimCoroutineToQueue(float expAmount)
    {
        _claimCoroutine.Enqueue(ClaimExpCoroutine(expAmount));
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(ProcessQueue());
        }
    }
    public IEnumerator ClaimExpCoroutine(float expAmount)
    {

        float elapsedExp = 0;
        _speed = _levelInformation[_currentLevel];
        while (elapsedExp < expAmount)
        {
            if (GameStat.gameTimeScale == 0)
            {
                yield return null;
            }
            else
            {
                float deltaExp = Time.deltaTime * _speed;
                elapsedExp += deltaExp;
                _currentExp += deltaExp;
                if (_claimCoroutine.Count == 0 && elapsedExp > expAmount * 0.7f)
                {
                    _speed *= 0.95f;
                }
                CheckLevelUp();
                UpdateExpBar();
                yield return null;
            }
        }
    }
    private void CheckLevelUp()
    {

        if (_currentExp >= _levelInformation[_currentLevel])
        {
            _currentExp = 0;
            _currentLevel++;
            _levelText.text = _currentLevel.ToString();
            UpdateExpBar();
            ChooseTalentPrepare();
            _speed = _levelInformation[_currentLevel] / 1.3f;
        }
    }

    private void UpdateExpBar()
    {
        ChangeRight(_expBar, _expBarBorder.sizeDelta.x - (_currentExp * _expBarBorder.sizeDelta.x / _levelInformation[_currentLevel]));
    }
    private void ChangeRight(RectTransform rectTransform, float right)
    {
        {
            Vector2 offset = rectTransform.offsetMax;
            offset.x = -right;
            rectTransform.offsetMax = offset;
        }
    }
    private void ChooseTalentPrepare(float time = 0.5f)
    {
        _isChoosing = true;
        _chooseTalentPanel.raycastTarget = true;
        _chooseTalentPanel.maskable = true;
        TalentsInfo.instance.CloseClick();
        GameStat.ChangeGameTimeScale(0);
        DeskController.instance.StopDrag();
        _enemiesAnimator.ChangeAnimatorSpeed(GameStat.gameTimeScale);
        List<int> existId = TalentsController.instance.GetRandomExistId();
        for (int i = 0; i < 3; i++)
        {
            _talentCardsUI[i].SetInfo(existId[i]);
            _talentCardsUI[i].gameObject.SetActive(true);
            var card = _talentCardsUI[i].GetComponent<RectTransform>();
            card.anchoredPosition = new Vector2(card.anchoredPosition.x, 1920);
        }
        //
        StartCoroutine(FeelingTools.ZoomInCoroutine(_glow, 1f, time));
        StartCoroutine(FeelingTools.Rotate(_glow, time));
        StartCoroutine(FeelingTools.FadeInCoroutine(_chooseTalentPanel, time, 0.8f, RotateCard));
        StartCoroutine(CardDrop());
    }
    private void RotateCard()
    {
        for (int i = 0; i < 3; i++)
        {
            StartCoroutine(_talentCardsUI[i].Rotate());
        }
        _chooseTalentsText.gameObject.SetActive(true);
        _levelUpBanner.SetActive(true);

    }
    private IEnumerator CardDrop()
    {
        foreach (var cardUI in _talentCardsUI)
        {
            var card = cardUI.GetComponent<RectTransform>();
            StartCoroutine(FeelingTools.MoveToTarget(card, 0.4f, card.anchoredPosition - new Vector2(0, 1920), null, 250f));
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void ChooseTalentEnd()
    {
        GameStat.ChangeGameTimeScale(1);
        _chooseTalentPanel.raycastTarget = false;
        _chooseTalentPanel.maskable = false;
        _chooseTalentsText.gameObject.SetActive(false);
        _levelUpBanner.SetActive(false);
        _enemiesAnimator.ChangeAnimatorSpeed(GameStat.gameTimeScale);
        StartCoroutine(FeelingTools.FadeOutCoroutine(_chooseTalentPanel, 0.5f));
        CardMoveUp();
        _isChoosing = false;
    }
    private void CardMoveUp()
    {
        _activeCoroutines = _talentCardsUI.Count;
        foreach (var cardUI in _talentCardsUI)
        {
            var card = cardUI.GetComponent<RectTransform>();
            StartCoroutine(FeelingTools.MoveToTarget(card, 0.3f, card.anchoredPosition + new Vector2(0, 1920f), OnMoveToTargetComplete));
        }
    }
    private void OnMoveToTargetComplete()
    {
        _activeCoroutines--;
        if (_activeCoroutines == 0)
        {
            _isChoosing = false;
        }
    }
    public bool IsChoosing()
    {
        return _isChoosing;
    }
    public int GetLevel()
    {
        return _currentLevel;
    }
}
