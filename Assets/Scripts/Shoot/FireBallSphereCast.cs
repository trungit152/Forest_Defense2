using UnityEngine;

public class FireBallSphereCast : BulletSphereCast
{
    [SerializeField] private float _dmgPerHalfSecond = 2f;
    [SerializeField] private float _bunrtTime = 5f;
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
                        if (enemy != null && !enemy.IsDie())
                        {
                            SetDamageNumber(enemy);
                            //damage
                            enemy.LoseHealth(_damage);
                            enemy.EnemyKnockBack.ApplyKnockback(_knockBackPosition != null ? _knockBackPosition.position : transform.position);

                            //burnt
                            enemy.BurntHealth(_dmgPerHalfSecond, _bunrtTime);

                        }
                        if (_attackImpact != null)
                        {

                            _attackImpact.GetObject(transform.position);
                        }

                        gameObject.SetActive(false);
                        return;
                    }
                }
            }

            Debug.DrawLine(origin, origin + direction * maxDistance, Color.blue);
        }
    }
}
