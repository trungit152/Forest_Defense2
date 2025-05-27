using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] protected float _range;
    [SerializeField] protected float _coolDown;
    [SerializeField] protected float _damage;
    [SerializeField] protected Transform _bulletSpawnPoint;
    [SerializeField] protected float _critPercent;
    [SerializeField] protected float _critDamage;

    protected bool _canAttack = true;
    protected float _cdRemaining = 0;
    protected bool _isHavingEnemyInRange;
    protected List<Enemy> _enemyList = new();

    public static PlayerAttack instance;
    public void Init(float range, float coolDown, float damage)
    {
        _range = range;
        _coolDown = coolDown;
        _damage = damage;
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
        set
        {
            _playerSpineController = value;
        }
    }
    private ObjectPool _objectPool;
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
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    public void Init()
    {

        _cdRemaining = 0;
    }
    protected virtual void CoolDownCounter()
    {
        
    }
    public virtual void Shoot()
    {
        //override
    }
    public float GetAttackCoolDown()
    {
        return _coolDown;
    }
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.gray;
    //    Gizmos.DrawWireSphere(PlayerHealth.instance.GetPlayerCenter().position, _range);
    //}
    public bool IsHavingEnemyInRange()
    {
        return _isHavingEnemyInRange;
    }

    public void FaceToEnemy()
    {
        if (_enemyList.Count > 0)
        {
            PlayerSpineController.CheckLeftRightRotation(_enemyList[0].transform.position - transform.position);
        }
    }
    public void SetDamage(float d)
    {
        _damage = d;
    }
    public void IncreaseDamage(float percent)
    {
        _damage += _damage * percent * 0.01f;
    }
    public void DecreaseDamage(float percent)
    {
        _damage -= _damage * percent * 0.01f;
    }
    public float GetDamage()
    {
        return _damage;
    }
    public void IncreaseAttackSpeed(float percent)
    {
        _coolDown -= _coolDown * percent * 0.01f; 
    }
    public void IncreaseAttackRange(float percent)
    {
        _range += _range * percent * 0.01f;
    }
    public virtual void AddBullet(int i = 1)
    {

    }
    public virtual void SplitTarget(int i = 1)
    {

    }
    public void SetCanAttack(bool b)
    {
        _canAttack = b;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
