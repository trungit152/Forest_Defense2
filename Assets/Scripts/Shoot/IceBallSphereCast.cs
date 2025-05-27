using UnityEngine;

public class IceBallSphereCast : BulletSphereCast
{
    [SerializeField] private float _freezeTime = 1f;
    private void Update()
    {
        Vector2 origin = transform.position;
        Vector2 direction = Vector2.zero;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, radius, direction, maxDistance, layerMask);

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    var enemy = hit.collider.GetComponent<Enemy>();
                    if (enemy != null && !enemy.IsDie())
                    {
                        SetDamageNumber(enemy);
                        //damage
                        enemy.LoseHealth(_damage);
                        enemy.EnemyKnockBack.ApplyKnockback(_knockBackPosition != null ? _knockBackPosition.position : transform.position);
                        //freeze
                        if (FeelingTools.RandomChance(20))
                        {
                            enemy.Freeze(_freezeTime);

                            //MethodInfo method = typeof(Enemy).GetMethod("Freeze");
                            //method.Invoke(enemy, new object[] { _freezeTime });
                        }
                    }
                                 
                    gameObject.SetActive(false);
                    return;
                }
            }
        }
        
        Debug.DrawLine(origin, origin + direction * maxDistance, Color.blue);
    }
}
