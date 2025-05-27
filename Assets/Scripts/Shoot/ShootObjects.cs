using System.Collections;
using UnityEngine;
public class ShootObjects : AttackObject
{
    [SerializeField] private float _bulletSpeed = 15f;
    public void Shoot(Vector3 spawnPosition, float range, int bulletAmount)
    {
        if (IsHavingEnemy(range, bulletAmount))
        {
            foreach (var enemy in _nearestEnemy)
            {
                GameObject bullets = ObjectPool.GetObject(spawnPosition);
                var bulletsComponent = bullets.GetComponent<Bullets>();
                var bulletsSphereCast = bullets.GetComponent<BulletSphereCast>();
                if (bulletsComponent != null)
                {
                    //handle crit attack
                    float damage = _damage;
                    bool isCrit = FeelingTools.RandomChance(_critPercent);
                    if (isCrit)
                    {
                        damage *= _critDamage * 0.01f;
                    }
                    //
                    bulletsComponent.SetTarget(enemy.GetCenter());
                    bulletsComponent.SetSpeed(_bulletSpeed);
                    if(bulletsSphereCast != null)
                    {
                        bulletsSphereCast.SetDamage(damage, isCrit);
                    }
                }
            }
        }
    }

    public void ShootThunder(Vector3 spawnPosition, float range, Transform spawnObject = null, int bounceTime = 3)
    {
        if (IsHavingEnemy(range))
        {
            StartCoroutine(ShootThunderCoroutine(spawnPosition, range, spawnObject, bounceTime));
        }
    }

    private IEnumerator ShootThunderCoroutine(Vector3 spawnPosition, float range, Transform spawnObject, int bounceTime)
    {
        for (int i = _nearestEnemy.Count - 1; i >= 0; i--)
        {
            var enemy = _nearestEnemy[i];
            var from = spawnPosition;
            var to = enemy;

            for (int j = 0; j <= bounceTime; j++)
            {
                if (to != null)
                {
                    yield return StartCoroutine(FindNearlyEnemyCoroutine(from, to, j, spawnObject));
                    if (j < bounceTime)
                    {
                        from = to.transform.position;
                        to = to.GetNearlyEnemyToShock();
                    }
                }
            }
        }
    }
    private IEnumerator FindNearlyEnemyCoroutine(Vector3 from, Enemy to, int i = 0,bool isStun = false, Transform parent = null)
    {
        GameObject thunder = ObjectPool.GetObject(from);
        thunder.transform.SetParent(parent);
        var thunderComponent = thunder.GetComponent<Thunder>();
        if (thunderComponent != null && to != null && to.gameObject.activeSelf)
        {
            float damage = _damage;
            bool isCrit = FeelingTools.RandomChance(_critPercent);
            thunderComponent.SetCrit(isCrit);
            if (isCrit)
            {
                damage *= _critDamage * 0.01f;
            }
            thunderComponent.SetDamage(damage, isCrit);
            thunderComponent.SetStun(_stunPercent, _stunTime);
            yield return StartCoroutine(thunderComponent.MoveLineRenderer(
                from, to
            ));
        }
    }
}