using ES3Types;
using ntDev;
using Spine;
using Spine.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Turrets : MonoBehaviour
{
    [Header("Turret Stat")]
    [SerializeField] private int _id;
    [SerializeField] protected float _range;
    [SerializeField] protected float _coolDown;
    [SerializeField] protected float _damage;
    [SerializeField] protected string _name;
    [SerializeField] protected int _mergeLevel = 1;
    public int MergeLevel { get => _mergeLevel; }
    [Space(10)]
    [SerializeField] protected GameObject _mergeUp;
    [SerializeField] protected GameObject _levelUpText;
    [Space(10)]
    [SerializeField] protected ManagerSpine _upSpine;
    [SerializeField] protected BoneFollower _upBoneFollower;

    [SerializeField] protected ManagerSpine _downSpine;
    [SerializeField] protected BoneFollower _downBoneFollower;

    [SerializeField] protected Transform _downSpawn;
    [SerializeField] protected Transform _upSpawn;

    [SerializeField] protected GameObject _underBlock;

    [SerializeField] protected int _bulletAmount = 1;
    [SerializeField] protected int _bounceTime = 2;

    protected float _multiShootPercent = 0;

    //damage
    protected float _damageIncreasePercent = 0;
    protected float _basicDamage;

    private bool _canShoot;
    protected List<Enemy> _nearestEnemy;
    protected bool _isUp;
    protected Transform _spawnPos;
    protected Bone _downBoneTarget;
    protected Bone _upBoneTarget;

    public Skeleton skeleton;

    private TurretSpineController _turretSpineController;
    public TurretSpineController TurretSpineController
    {
        get
        {
            if (_turretSpineController == null)
            {
                _turretSpineController = GetComponent<TurretSpineController>();
            }
            return _turretSpineController;
        }
        set
        {
            _turretSpineController = value;
        }
    }
    private AttackObject _attackObject;
    public AttackObject AttackObject
    {
        get
        {
            if (_attackObject == null)
            {
                _attackObject = GetComponent<AttackObject>();
            }
            return _attackObject;
        }
        set
        {
            _attackObject = value;
        }
    }

    private void Start()
    {
        _mergeUp.SetActive(false);
        _basicDamage = DataTurret.GetData(_id).ATK[_mergeLevel - 1];
    }

    protected float _cdRemaining = 0;
    public virtual void Shoot()
    {

    }
    public void Init(float range, float damage, float coolDown, int mergeLevel = 1)
    {
        _mergeLevel = mergeLevel;
        _cdRemaining = 0;
        _range = range;
        _damage = damage;
        _basicDamage = damage;
        _coolDown = coolDown;
        AttackObject.Init(_damage);
    }
    private void FixedUpdate()
    {
        ShootCount();
        CheckSpineVisual();
    }
    protected virtual void ShootCount()
    {
        if (_cdRemaining <= 0)
        {
            TurretSpineController.SetShootAnimation();
            _cdRemaining = _coolDown;
        }
        else
        {
            _cdRemaining -= Time.fixedDeltaTime * GameStat.gameTimeScale;
        }
    }


    public List<Enemy> GetNearestEnemy()
    {
        return _nearestEnemy;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
    protected void SetBoneSetting()
    {
        _upSpine.TimeScaleFactor = 1;
        _downSpine.TimeScaleFactor = 1;
        _downBoneTarget = _downSpine.Ske.FindBone("crosshair");
        _upBoneTarget = _upSpine.Ske.FindBone("crosshair");
        //
        _isUp = true;
        ShowDownSpine();
        TurretSpineController.SetDropped();
    }
    protected void ShowDownSpine()
    {
        if (_isUp)
        {
            _isUp = false;
            _upSpine.gameObject.SetActive(false);
            _downSpine.gameObject.SetActive(true);
            TurretSpineController.ChangeDataAsset(_downSpine.gameObject);
        }
    }
    protected void ShowUpSpine()
    {
        if (!_isUp)
        {
            _isUp = true;
            _upSpine.gameObject.SetActive(true);
            _downSpine.gameObject.SetActive(false);
            TurretSpineController.ChangeDataAsset(_upSpine.gameObject);
        }
    }
    protected virtual void CheckSpineVisual()
    {
        if (_nearestEnemy != null)
        {
            Vector3 direction = _nearestEnemy[0].transform.position - transform.position;
            if (direction.x > 0 && direction.y < 1)
            {
                ShowDownSpine();
                FeelingTools.FlipY(transform, 180);
                _downBoneTarget.SetLocalPosition(MyMath.GetSymmetricOY(direction));
            }
            else if (direction.x < 0 && direction.y < 1)
            {
                ShowDownSpine();
                FeelingTools.FlipY(transform, 0);
                _downBoneTarget.SetLocalPosition(direction);
            }
            else if (direction.x < 0 && direction.y > 1)
            {
                ShowUpSpine();
                FeelingTools.FlipY(transform, 0);
                _upBoneTarget.SetLocalPosition(direction);
            }
            else if (direction.x > 0 && direction.y > 1)
            {
                ShowUpSpine();
                FeelingTools.FlipY(transform, 180);
                _upBoneTarget.SetLocalPosition(MyMath.GetSymmetricOY(direction));
            }
        }
    }
    protected void SetSpawnPos(Vector2 pos)
    {
        _spawnPos.position = pos;
    }
    protected void SetSpawnPos()
    {
        if (_isUp)
        {
            _spawnPos = _upSpawn;
        }
        else
        {
            _spawnPos = _downSpawn;
        }
    }
    public bool IsHavingEnemy()
    {
        return AttackObject.IsHavingEnemy(_range, _bulletAmount);
    }
    public void ChangeVisual()
    {
        AttackObject.IsHavingEnemy(_range, _bulletAmount);
        _nearestEnemy = AttackObject.GetNeareastEnemy();
        CheckSpineVisual();
        SetSpawnPos();
    }
    public void SetUnderBlock(bool isShown)
    {
        if (isShown)
        {
            _underBlock.SetActive(true);
        }
        else
        {
            _underBlock.SetActive(false);
        }
    }
    public int ID()
    {
        return _id;
    }
    public void IncreaseATK(float percent)
    {
        _damageIncreasePercent += percent / 100;
        UpdateDamage();
    }
    public void IncreaseATKS(float percent)
    {
        float AS = 1 / _coolDown;
        AS -= AS * percent / 100;
        _coolDown = 1 / AS;
    }
    public void IncreaseRange(float percent)
    {
        _range += _range * percent / 100;
    }

    public void AddBulletAmount(float addAmount = 0)
    {
        _bulletAmount += (int)addAmount;
    }

    public void AddBounceTime(float addAmount = 0)
    {
        _bounceTime += (int)addAmount;
    }
    public void ResetValue()
    {
        _bulletAmount = 1;
        _bounceTime = 2;
        _damageIncreasePercent = 0;
    }
    public void AddMultishootPercent(float percent)
    {
        _multiShootPercent += percent / 100;
    }
    public void AddStunStat(float percent, float time)
    {
        AttackObject.AddStunPercent(percent, time);
    }
    public void Merge()
    {
        _mergeLevel++;
        Instantiate(_levelUpText, transform.position, Quaternion.identity);
        var data = DataTurret.GetData(_id);
        Init(data.Range, data.ATK[_mergeLevel - 1], data.CD[_mergeLevel - 1], data.MergeLevel[_mergeLevel - 1]);
        UpdateDamage();
    }

    public bool IsMaxLevel()
    {
        if (_mergeLevel >= DataTurret.GetData(_id).MergeLevel.Length)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ShowMergeArrow()
    {
        if (!IsMaxLevel())
        {
            if (_mergeUp != null)
            {
                _mergeUp.SetActive(true);
            }
        }
    }
    public void HideMergeArrow()
    {
        if (_mergeUp != null)
        {
            _mergeUp.SetActive(false);
        }
    }
    private void UpdateDamage()
    {
        _damage = _basicDamage + _basicDamage * _damageIncreasePercent;
        AttackObject.Init(_damage);
    }
}
