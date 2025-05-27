using System.Collections;
using UnityEngine;

public class LoopAnimBullet : EnemyBasicBullet
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _sr;
    protected override IEnumerator MoveToTargetNewCoroutine()
    {
        bool isOverTarget = false;  
        Vector2 direction = _targetNew - transform.position;
        while (!_destroyed)
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)direction.normalized, 
                _speed * Time.fixedDeltaTime * GameStat.gameTimeScale);
            //rotate
            Rotate(direction);
            if (!isOverTarget && Vector2.Distance(transform.position, _targetNew) < 0.1f)
            {
                isOverTarget = true;
            }
            if (isOverTarget)
            {
                if (Vector2.Distance(transform.position, _targetNew) > _lastedDistance)
                {
                    _destroyed = true;
                }
            }
            yield return null;
        }
        _vfxPool.GetObject(transform.position);
        _animator.SetTrigger("end");
    }
    protected override void Rotate(Vector2 direction)
    {
        if (direction.x < 0)
        {
            _sr.flipX = false;
        }
        else
        {
            _sr.flipX = true;
        }
    }
}
