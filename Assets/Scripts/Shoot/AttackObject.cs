using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    protected ObjectPool _objectPool;
    [SerializeField] protected float _damage = 5f;
    [SerializeField] protected Transform _center;
    [SerializeField] protected float _critDamage;
    [SerializeField] protected float _critPercent;
    [SerializeField] protected float _stunPercent = 0;
    [SerializeField] protected float _stunTime = 0;
    protected List<Enemy> _nearestEnemy;
    public ObjectPool ObjectPool
    {
        get
        {
            if (_objectPool == null)
            {
                _objectPool = GetComponent<ObjectPool>();
            }
            return _objectPool;
        }
        set
        {
            _objectPool = value;
        }
    }

    private void Awake()
    {
        _nearestEnemy = new List<Enemy>();
        if(_center == null)
        {
            _center = transform;
        }
    }

    public virtual void Init(float damage)
    {
        _damage = damage;
    }
    public bool IsHavingEnemy(float range, int bulletAmount = 1)
    {
        var enemies = EnemiesController.instance.GetNearestEnemies(_center.position, range, bulletAmount);
        if (enemies.Count == 0)
        {
            _nearestEnemy.Clear();
            return false;
        }
        else if (enemies.Count > 0)
        {
            _nearestEnemy = enemies;
        }
        return true;
    }
    public List<Enemy> GetNeareastEnemy()
    {
        if (_nearestEnemy.Count > 0)
        {
            return _nearestEnemy;
        }
        else return null;
    }
    public void AddStunPercent(float stunPercent, float stunTime)
    {
        _stunPercent += stunPercent;
        _stunTime += stunTime;
    }
}
