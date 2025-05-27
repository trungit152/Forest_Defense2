using DamageNumbersPro;
using UnityEngine;

public class BombSphereCast : MonoBehaviour
{
    [SerializeField] private float radius = 1f;
    [SerializeField] private float maxDistance = 1f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] protected DamageNumber _critNumber;
    [SerializeField] protected DamageNumber _normalNumber;

    private bool _isCrit;
    private float _damage;
    void Update()
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
                        if (!_isCrit)
                        {
                            enemy.SetDamageNumber(_normalNumber);
                        }
                        else
                        {
                            enemy.SetDamageNumber(_critNumber);
                        }
                        enemy.LoseHealth(_damage);
                    }
                }
            }
            gameObject.SetActive(false);
            return;
        }

        Debug.DrawLine(origin, origin + direction * maxDistance, Color.blue);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    public void SetDamage(float damage, bool isCrit)
    {
        _damage = damage;
        _isCrit = isCrit;
    }
}
