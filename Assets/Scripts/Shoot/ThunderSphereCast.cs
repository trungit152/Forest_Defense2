using UnityEngine;

public class ThunderSphereCast : BulletSphereCast
{
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
                        enemy.LoseHealth(_damage);
                        enemy.Shock();
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
