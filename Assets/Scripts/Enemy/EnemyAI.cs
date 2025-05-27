using UnityEngine;
using Pathfinding;
public class EnemyAI : MonoBehaviour
{
    private float _speed;
    [SerializeField] private float nextWaypointDistance = 0.1f;
    [SerializeField] private float _repulsionRadius = 0.25f;
    [SerializeField] private float _repulsionForce = 0.01f;

    [SerializeField] private float randomTargetRadius = 0.5f;
    private Transform _target;
    private Path _path;
    private int _currentWaypoint = 0;
    public Seeker seeker;

    private Vector2 _direction;
    private Vector3 _prePoint;
    private float _countTime;
    private float _countFindTime;
    private bool _canMove;

    private EnemyAnimation _enemyAnimation;
    public EnemyAnimation EnemyAnimation
    {
        get
        {
            if (_enemyAnimation == null)
            {
                _enemyAnimation = GetComponent<EnemyAnimation>();
            }
            return _enemyAnimation;
        }
        set
        {
            _enemyAnimation = value;
        }
    }
    private Enemy _enemy;
    public Enemy Enemy
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
    private void Awake()
    {
        if (seeker == null) seeker = GetComponent<Seeker>();
    }
    public void Init()
    {
        _canMove = true;
        _countTime = 0;
        _countFindTime = 0;
    }
    public void UpdatePath()
    {
        if (seeker.IsDone())
        {
            Vector3 randomTargetPoint = GetRandomPointNearTarget();
            seeker.StartPath(transform.position, randomTargetPoint, OnPathComplete);
        }
    }
public void FindPath()
{
    if (_target != null && seeker != null)
    {
        Vector3 randomTargetPoint = GetRandomPointNearTarget();
        seeker.StartPath(Enemy.transform.position, randomTargetPoint, OnPathComplete);
    }
}

    void OnPathComplete(Path p)
    {
        
        if (!p.error)
        {
            Vector3 finalPos = p.vectorPath[p.vectorPath.Count - 1];
            float distanceToTarget = Vector3.Distance(finalPos, _target.position);

            if (distanceToTarget > 0.5f) 
            {
                _path = null;
                EnemyAnimation.SetIdle();
            }
            else
            {
                _path = p;
                _currentWaypoint = 0;
            }
        }
    }

    public void CheckLeftRightRotation()
    {
        EnemyAnimation.CheckLeftRightRotation(_direction);
    }
    public void MoveToNextPoint()
    {
        if (_path == null) return;
        EnemyAnimation.SetWalk();
        CheckStuck();
        //Move by vector:
        if (_currentWaypoint >= _path.vectorPath.Count) return;
        Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] - (Vector2)transform.position).normalized;
        _direction = direction;
        EnemyAnimation.CheckLeftRightRotation(direction);

        if (Vector2.Distance(transform.position, _path.vectorPath[_currentWaypoint]) < nextWaypointDistance)
        {
            //transform.position = _path.vectorPath[_currentWaypoint];
            _currentWaypoint++;
        }
        else
        {
            Vector3 repulsion = CalculateRepulsionForce();
            Vector3 moveDirection = (Vector3)direction * _speed * Time.fixedDeltaTime * GameStat.gameTimeScale + repulsion;
            transform.position += moveDirection;
        }
    }

    private Vector3 CalculateRepulsionForce()
    {
        Vector3 repulsion = Vector3.zero;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _repulsionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject && collider.CompareTag("EnemyAI") && !collider.transform.IsChildOf(transform))
            {
                Vector3 direction = transform.position - collider.transform.position;
                float distance = direction.magnitude;
                if (distance < _repulsionRadius)
                {
                    repulsion += direction.normalized * (_repulsionForce / (distance==0 ? 0.001f : distance));
                }
            }
        }
        return repulsion;
    }
    private void CheckStuck()
    {
        _countTime += Time.deltaTime * GameStat.gameTimeScale;
        if (_countTime >= 2)
        {
            _prePoint = transform.position;
            _countTime = 0;
        }

        if ((transform.position - _prePoint).magnitude < 0.02f)
        {           
            _countFindTime += Time.deltaTime * GameStat.gameTimeScale;
            if (_countFindTime >= 2)
            {
                UpdatePath();
                _countFindTime = 0;
            }
        }
        else
        {
            _countFindTime = 0;
        }
    }
    private Vector3 GetRandomPointNearTarget()
    {
        if (_target == null) return transform.position;

        Vector2 randomOffset = Random.insideUnitCircle * randomTargetRadius;
        Vector3 randomPoint = _target.position + (Vector3)randomOffset;
        return randomPoint;
    }

    void FixedUpdate()
    {
        if (_canMove)
        {
            MoveToNextPoint();
        }
    }
    public void SetTarget(Transform t)
    {
        _target = t;
        UpdatePath();
    }
    public void SetSpeed(float s)
    {
        _speed = s;
    }
    public float GetSpeed()
    {
        return _speed;
    }
    public Transform GetTarget()
    {
        return _target;
    }
    public void SetCanMove(bool b)
    {
        _canMove = b;
    }
    public bool CanMove()
    {
        return _canMove;
    }
    public bool HavePath()
    {
        return _path != null;
    }
}
