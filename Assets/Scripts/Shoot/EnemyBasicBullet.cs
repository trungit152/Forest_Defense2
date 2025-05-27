using System.Collections;
using UnityEngine;

public class EnemyBasicBullet : MonoBehaviour
{
    protected float _speed;
    [SerializeField] protected Transform _target;
    [SerializeField] protected GameObject _visual;
    [SerializeField] protected ObjectPool _vfxPool;
    [SerializeField] protected Animator _exploseAnimator;
    [SerializeField] private LayerMask _wallLayer;
    protected Vector3 _targetNew; 
    protected bool _destroyed = false;
    protected EnemyAttack _enemyAttack;
    protected float _damage;
    protected Coroutine _coroutine;
    [SerializeField] protected float _lastedDistance = 2f;
    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
    public void SetDamage(float damage)
    {
        _damage = damage;
    }
    public void SetEnemyParent(EnemyAttack enemyAttack)
    {
        _enemyAttack = enemyAttack;
    }
    public void SetTarget(Transform transform)
    {
        _target = transform;
    }
    public void SetTargetNew(Vector3 target)
    {
        _targetNew = target;
        _destroyed = false;
    }
    public void StartMoveToTargetNew()
    {
        if (_coroutine != null) { StopCoroutine(_coroutine); }
        _coroutine = StartCoroutine(MoveToTargetNewCoroutine());
    }
    protected virtual IEnumerator MoveToTargetNewCoroutine()
    {
        bool isOverTarget = false;
        Vector2 direction = _targetNew - transform.position;
        Rotate(direction);
        while (!_destroyed)
        {
            transform.position = Vector2.MoveTowards(transform.position, (transform.position + (Vector3)direction.normalized), 
                _speed * Time.fixedDeltaTime * GameStat.gameTimeScale);
            if(!isOverTarget && Vector2.Distance(transform.position, _targetNew) < 0.1f)
            {
                isOverTarget = true;
            }
            if (isOverTarget)
            {
                if(Vector2.Distance(transform.position, _targetNew) > _lastedDistance)
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

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _enemyAttack.TakeDamagePlayer();
            _destroyed = true;
        }
        else if(collision.gameObject.CompareTag("Main Wall"))
        {
            _enemyAttack.TakeDamageWall();
            _destroyed = true;
        }
    }
    protected virtual void Rotate(Vector2 direction)    
    {
        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _visual.transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }
}
