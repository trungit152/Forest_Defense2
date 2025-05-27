using System.Collections;
using UnityEngine;

public class EnemyRotatedBullet : EnemyBasicBullet
{
    [SerializeField] private float _rotationSpeed;
    protected override void Rotate(Vector2 direction)
    {
        float zRotation = _rotationSpeed * Time.deltaTime * GameStat.gameTimeScale;
        _visual.transform.Rotate(0, 0, zRotation);
    }
    protected override IEnumerator MoveToTargetNewCoroutine()
    {
        bool isOverTarget = false;  
        Vector2 direction = _targetNew - transform.position;
        while (!_destroyed)
        {
            transform.position = Vector2.MoveTowards(transform.position, (transform.position + (Vector3)direction.normalized),
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
        if (_exploseAnimator != null)
        {
            _exploseAnimator.SetTrigger("explose");
        }
        else
        {
            gameObject.SetActive(false);
        }
        _vfxPool.GetObject(transform.position);

    }
}
