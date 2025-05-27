using UnityEngine;
using System.Collections;

public class EnemyKnockBack : MonoBehaviour
{
    [SerializeField] private float _knockbackForce = 0.5f;
    [SerializeField] private float _knockbackDuration = 0.2f;
    [SerializeField] private CircleCollider2D _circleCollider;
    [SerializeField] private LayerMask _wallLayer;
    private bool isKnockedBack = false;

    private Enemy _enemy;
    private Enemy Enemy
    {
        get
        {
            if (_enemy == null)
            {
                _enemy = GetComponent<Enemy>();
            }
            return _enemy;
        }
        set
        {
            _enemy = value;
        }
    }
    public void Init()
    {
        isKnockedBack = false;
    }

    public void ApplyKnockback(Vector2 attackerPosition)
    {
        if (isKnockedBack) return;
        isKnockedBack = true;

        Vector2 knockbackDirection = (Enemy.GetCenter().position - (Vector3)attackerPosition).normalized;
        Vector2 targetPosition = (Vector2)transform.position + (knockbackDirection * _knockbackForce);

        StartCoroutine(KnockbackCoroutine(targetPosition));
    }

    private IEnumerator KnockbackCoroutine(Vector2 targetPos)
    {
        bool isCollide = false;
        Vector2 noBlockPos = Vector3.zero;

        float elapsedTime = 0;
        Vector2 startPos = transform.position;

        while (elapsedTime < _knockbackDuration)
        {
            Vector2 newPosition = Vector2.Lerp(startPos, targetPos, elapsedTime / _knockbackDuration);

            if (Physics2D.OverlapCircle(newPosition, _circleCollider.radius, _wallLayer))
            {
                isCollide = true;
                noBlockPos = transform.position;
            }
            else
            {
                isCollide = false;
            }

            transform.position = newPosition;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Enemy.EnemyAI.CheckLeftRightRotation();
        if (isCollide)
        {
            StartCoroutine(MoveToNonBlockPos(noBlockPos));
        }
        isKnockedBack = false;
    }

    private IEnumerator MoveToNonBlockPos(Vector2 targetPos)
    {
        float elapsedTime = 0;
        Vector2 startPos = transform.position;
        while (elapsedTime < _knockbackDuration)
        {
            transform.position = Vector2.Lerp(startPos, targetPos, elapsedTime / _knockbackDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Enemy.EnemyAI.CheckLeftRightRotation();
    }
}
