using DamageNumbersPro;
using UnityEngine;

public class BulletSphereCast : MonoBehaviour
{
    [SerializeField] protected float radius = 0.1f;
    [SerializeField] protected float maxDistance = 1f;
    [SerializeField] protected LayerMask layerMask;
    [SerializeField] protected DamageNumber _critNumber;
    [SerializeField] protected DamageNumber _normalNumber;
    protected float _damage;
    [SerializeField] protected ObjectPool _attackImpact;
    [SerializeField] protected Transform _knockBackPosition;
    private bool _isCrit;
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
                        enemy.SetNumberDirection(CheckLeftRight(enemy.GetCenter()));
                        enemy.LoseHealth(_damage);
                        enemy.EnemyKnockBack.ApplyKnockback(_knockBackPosition != null ? _knockBackPosition.position : transform.position);
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    public void SetDamage(float damage, bool isCrit = false)
    {
        _damage = damage;
        _isCrit = isCrit;
    }
    public bool CheckLeftRight(Transform enemy)
    {
        if (transform.position.x > enemy.transform.position.x)
        {
            return false;
        }
        return true;
    }
    public void SetDamageNumber(Enemy enemy)
    {
        if (!_isCrit)
        {
            enemy.SetDamageNumber(_normalNumber);
        }
        else
        {
            enemy.SetDamageNumber(_critNumber);
        }
    }
}
