using System.Collections.Generic;
using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    private Transform _droppedItems;

    [SerializeField] private List<TurretsCard> _turretsCards;
    [SerializeField] private float _expAmount;
    [SerializeField] private Enemy _enemy;
    private ExpPool _expPool;
    public ExpPool ExpPool
    {
        get
        {
            if (_expPool == null)
            {
                _expPool = GetComponent<ExpPool>();
            }
            return _expPool;
        }
        set
        {
            _expPool = value;
        }
    }

    public void Init(float expAmount)
    {
        _expAmount = expAmount;
    }
    private void Awake()
    {
        _droppedItems = GameObject.Find("DroppedItems").transform;
    }
    public void DropItem(float cardPercent, float expPercent, bool isCardFly = true)
    {
        DropCard(cardPercent, isCardFly);
        DropExp(expPercent);
    }
    private void DropCard(float percent, bool isFly = true)
    {
        if (RandomChance(percent))
        {
            int i = Random.Range(0,_turretsCards.Count);
            if (EnemiesController.instance.GetTrapAmount() > 0)
            {
                while (_turretsCards[i]._type == TurretsCard.Type.Trap)
                {
                    i = Random.Range(0, _turretsCards.Count);
                }
            }
            else
            {
                EnemiesController.instance.AddTrap();
            }
            var card = Instantiate(_turretsCards[i], _droppedItems);
            card.transform.position = transform.position;
            if (isFly)
            {
                card.Init();
            }
        }
    }
    private void DropExp(float percent)
    {
        if (RandomChance(percent))
        {
            var exp = ExpPool.GetObject(_enemy.GetCenter().position);
            var expComponent = exp.GetComponent<ClaimableObject>();
            expComponent.Init();
            expComponent.SetExpAmount(_expAmount);
        }
    }
    public bool RandomChance(float percent)
    {
        return Random.Range(0f, 100f) < percent;
    }
}
