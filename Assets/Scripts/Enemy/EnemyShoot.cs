using UnityEngine;

public class EnemyShoot : EnemyAttack
{
    [SerializeField] private ObjectPool _bulletPool;
    [SerializeField] private Transform _spawnPos;
    [SerializeField] private float _bulletSpeed = 8f;
    public override void SpawnBullet()
    {
        var bullet = _bulletPool.GetObject(_spawnPos.position);
        var bulletComponent = bullet.GetComponent<EnemyBasicBullet>();
        if (bulletComponent != null)
        {
            bulletComponent.SetEnemyParent(this);
            if (EnemyChase.IsTargetWall())
            {
                //bulletComponent.SetTarget(EnemyAI.GetTarget());
                bulletComponent.SetTargetNew(EnemyAI.GetTarget().position);
            }
            else
            {
                //bulletComponent.SetTarget(PlayerHealth.instance.GetPlayerCenter());
                bulletComponent.SetTargetNew(PlayerHealth.instance.GetPlayerCenter().position);
            }
            bulletComponent.SetSpeed(_bulletSpeed);
            bulletComponent.SetDamage(_damage);
            //bulletComponent.StartMoveToTarget();
            bulletComponent.StartMoveToTargetNew();
        }
    }
}
