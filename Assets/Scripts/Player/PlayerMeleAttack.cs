using UnityEngine;

public class PlayerMeleAttack : PlayerAttack
{
    private void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        CoolDownCounter();
    }
    protected override void CoolDownCounter()
    {
        if (_cdRemaining <= 0 && !MultiplayerSpawner.localPlayer.PlayerHealth.IsDeath())
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
    protected void GetEnemyInRange()
    {
        _enemyList = EnemiesController.instance.GetNearestEnemies(MultiplayerSpawner.localPlayer.PlayerHealth.GetPlayerCenter().position, _range);
        if (_enemyList.Count == 0)
        {
            _isHavingEnemyInRange = false;

        }
        else if (_enemyList.Count > 0)
        {
            _isHavingEnemyInRange = true;
        }
    }
    private void SetAttackAnimation()
    {
        if (MultiplayerSpawner.localPlayer.JoystickMove.IsMoving())
        {
            PlayerSpineController.SetWalkAtack();
        }
        else
        {
            if (_isHavingEnemyInRange)
            {
                PlayerSpineController.CheckLeftRightRotation(_enemyList[0].transform.position - transform.position);
            }
            PlayerSpineController.SetAttack();
        }
    }
    public override void Shoot()
    {
        foreach (var enemy in _enemyList)
        {
            GameObject swordSlashes = ObjectPool.GetObject(transform.position);
            var swordSlashesComponent = swordSlashes.GetComponent<SwordSlashes>();
            if (swordSlashesComponent != null)
            {
                swordSlashesComponent.SetTarget(enemy.transform);
            }
        }
    }
}
