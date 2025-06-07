using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : PlayerAttack
{
    [SerializeField] private int _bulletAmount = 1;
    [SerializeField] private int _targetAmount = 1;
    [SerializeField] private int _bulletSpeed = 10;

    private void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        CoolDownCounter();

    }
    public override void Shoot()
    {
        foreach (var enemy in _enemyList)
        {
            SpawnBullets(enemy, _bulletAmount);
        }
    }
    private void SpawnBullets(Enemy enemy, int bulletAmount)
    {
        if (bulletAmount <= 0) return;

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySoundEffect("PlayerShoot");
        }

        List<float> offsets = new List<float>();
        float offsetStep = 0.5f;

        if (bulletAmount % 2 == 1)
        {
            offsets.Add(0);
        }

        int pairs = bulletAmount / 2;
        float startOffset = (bulletAmount % 2 == 0) ? 0.25f : offsetStep;

        for (int i = 0; i < pairs; i++)
        {
            float offset = startOffset + i * offsetStep;
            offsets.Add(-offset);
            offsets.Add(offset);
        }

        foreach (float offset in offsets)
        {
            CreateBullet(enemy, offset);
        }
    }

    private void CreateBullet(Enemy enemy, float offset)
    {
        GameObject bullet = ObjectPool.GetObject(_bulletSpawnPoint.position);
        SetBulletData(bullet, enemy, offset);
    }

    private void SetBulletData(GameObject bullet, Enemy enemy, float offset)
    {
        var bulletComponent = bullet.GetComponent<Bullets>();
        var bulletSphereCast = bullet.GetComponent<BulletSphereCast>();

        if (bulletComponent != null)
        {
            bulletComponent.SetTarget(enemy.GetCenter());
            bulletComponent.SetOffset(offset);
            bulletComponent.SetSpeed(_bulletSpeed);

            float damage = _damage;
            bool isCrit = FeelingTools.RandomChance(_critPercent);
            if (isCrit)
            {
                damage *= _critDamage * 0.01f;
            }

            bulletSphereCast.SetDamage(damage, isCrit);
        }
    }
    protected void GetEnemyInRange()
    {
        _enemyList = EnemiesController.instance.GetNearestEnemies(transform.position, _range, _targetAmount);
        if (_enemyList.Count == 0)
        {
            _isHavingEnemyInRange = false;

        }
        else if (_enemyList.Count > 0)
        {
            _isHavingEnemyInRange = true;
        }
    }
    protected override void CoolDownCounter()
    {
        if (_canAttack)
        {
            if (_cdRemaining <= 0 && !Player.instance.PlayerHealth.IsDeath())
            {
                GetEnemyInRange();
                if (_isHavingEnemyInRange)
                {
                    SetAttackAnimation();
                }
                _cdRemaining = _coolDown;
            }
            else
            {
                _cdRemaining -= Time.fixedDeltaTime * GameStat.gameTimeScale;
            }
        }
    }
    private void SetAttackAnimation()
    {

        if (_isHavingEnemyInRange)
        {
            PlayerSpineController.CheckLeftRightRotation(_enemyList[0].transform.position - transform.position);
        }
        PlayerSpineController.SetAttack();
    }
    public void ClearEnemyList()
    {
        _enemyList.Clear();
        GetEnemyInRange();
    }
    public override void AddBullet(int i = 1)
    {
        _bulletAmount += i;
        DecreaseDamage(40);
    }
    public override void SplitTarget(int i = 1)
    {
        _targetAmount += i;
        DecreaseDamage(30);
    }
}