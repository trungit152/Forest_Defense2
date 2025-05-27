using UnityEngine;

public class ThrowObjects : AttackObject
{
    public void Shoot(Vector2 from, float range, int bulletAmount = 1)
    {
        if (IsHavingEnemy(range, bulletAmount))
        {
            GameObject bomb = ObjectPool.GetObject(from);
            var bombComponent = bomb.GetComponent<Bombs>();
            if (bombComponent != null)
            {
                float damage = _damage;
                bool isCrit = FeelingTools.RandomChance(_critPercent);
                if (isCrit)
                {
                    damage *= _critDamage * 0.01f;
                }

                bombComponent.SetDamage(damage, isCrit);
                bombComponent.StartParabolMove(from, _nearestEnemy[0].transform.position);
            }
            
        }
    }
}
