using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeskController : MonoBehaviour
{
    [SerializeField] private List<CardUI> _allCardUI;
    [SerializeField] private GameObject _cardUI;
    [SerializeField] private GameObject _leftButton;
    [SerializeField] private GameObject _rightButton;
    [SerializeField] private HorizontalLayoutGroup _cardView;
    public static DeskController instance;
    private List<CardUI> _cardList;
    private int _currentIndex;
    private int _maxIndex = 0;
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
        AddExistCardUI();
        _currentIndex = 0;
    }

    public void AddCard(CardUI card)
    {
        var _card = Instantiate(card, _cardUI.transform);
        _cardList.Add(_card);
        _maxIndex = (_cardList.Count - 1)/ 3;
        ShowButtonMode();
        UpdateVisibleCard();
    }
    public void AddCard(int id)
    {
        foreach (CardUI card in _allCardUI)
        {
            if (card.ID() == id)
            {
                AddCard(card);
                break;
            }
        }
    }
    public void RemoveCard(CardUI card)
    {
        _cardList.Remove(card);
        if (_cardList.Count <= 3)
        {
            _currentIndex = 0;
            _maxIndex = 0;
        }
        else
        {
            _maxIndex = (_cardList.Count - 1) / 3;
            if(_currentIndex > _maxIndex)
            {
                _currentIndex = _maxIndex;
            }
        }
        ShowButtonMode();
        UpdateVisibleCard();
    }

    public void RightButton()
    {
        if (_currentIndex < _maxIndex)
        {
            _currentIndex++;
            for (int i = 0; i < _cardList.Count; i++)
            {
                if (i < _currentIndex * 3 + 3 && i >= _currentIndex * 3)
                {
                    _cardList[i].gameObject.SetActive(true);
                }
                else
                {
                    _cardList[i].gameObject.SetActive(false);
                }
            }
        }
        ShowButtonMode();
    }
    public void LeftButton()
    {
        if (_currentIndex > 0)
        {
            _currentIndex--;
            for (int i = 0; i < _cardList.Count; i++)
            {
                if (i < _currentIndex * 3 + 3 && i >= _currentIndex * 3)
                {
                    _cardList[i].gameObject.SetActive(true);
                }
                else
                {
                    _cardList[i].gameObject.SetActive(false);
                }
            }
        }
        ShowButtonMode();
    }
    public void UpdateVisibleCard()
    {
        if (_cardList.Count <= 3)
        {
            for(int i = 0; i < _cardList.Count; i++) 
            {
                _cardList[i].gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < _cardList.Count; i++)
            {            
                if(i < _currentIndex * 3 + 3 && i >= _currentIndex * 3)
                {
                    _cardList[i].gameObject.SetActive(true);
                }
                else
                {
                    _cardList[i].gameObject.SetActive(false);
                }
            }
        }
    }
    public void StopDrag()
    {
        foreach (var card in _cardList)
        {
            card.StopDrag();
        }
    }
    private void AddExistCardUI()
    {
        _cardList = new List<CardUI>();
        for (int i = 0; i < _cardUI.transform.childCount; i++)
        {
            var cardUI = _cardUI.transform.GetChild(i).GetComponent<CardUI>();
            _cardList.Add(cardUI);
        }
    }
    private void ShowButtonMode()
    {
        if(_maxIndex == 0)
        {
            _leftButton.SetActive(false);
            _rightButton.SetActive(false);
        }
        else
        {
            if (_currentIndex == 0)
            {
                _leftButton.SetActive(false);
                _rightButton.SetActive(true);
            }
            else if (_currentIndex == _maxIndex)
            {
                _leftButton.SetActive(true);
                _rightButton.SetActive(false);
            }
            else
            {
                _leftButton.SetActive(true);
                _rightButton.SetActive(true);
            }
        }
    }
}
