using UnityEngine;

public class Bullets : MonoBehaviour
{
    private Transform _target;
    private Vector3 _targetPosition;
    private float _speed = 1.5f;
    [SerializeField] private Transform _visual;
    private Vector2 _startPos;
    private float _totalDistance;
    private Vector2 _normalVector;
    private float _offset = 0;
    private Vector2 _originalPos;
    private float _lerpSpeed;
    private void OnEnable()
    {
        _targetPosition = Vector3.zero;
    }
    public void SetTarget(Transform t)
    {
        _target = t;
        _startPos = transform.position;
        _totalDistance = Vector2.Distance(_target.position, _startPos);
        _normalVector = MyMath.GetNormal(_startPos, _target.position);
        _originalPos = transform.position;
    }
    public void SetOffset(float offset)
    {
        _offset = offset;
    }
    public void RemoveTarget()
    {
        _target = null;
    }
    public void SetSpeed(float s)
    {
        _speed = s;
        _visual.localScale = new Vector3(0.7f, 0.7f, 0.7f);
    }
    private void FixedUpdate()
    {
        MoveToTarget();
    }
    private void MoveToTarget()
    {
        if (_target != null)
        {         
            if (_target.gameObject.activeInHierarchy)
            {
                MoveAndRotate(_target.transform.position);
                _targetPosition = _target.position;
            }
            else
            {
                MoveAndRotate(_targetPosition);
                if(Vector2.Distance(transform.position, _targetPosition) < 0.2f)
                {
                    gameObject.SetActive(false);
                    RemoveTarget();
                }
            }
        }
    }
    private void MoveAndRotate(Vector3 _target)
    {
        //move
        float delta = 1;
        _normalVector = MyMath.GetNormal(_originalPos, _target);
        float distance = Vector2.Distance(_originalPos, _startPos);

        Vector2 direction = ((Vector2)_target - _originalPos).normalized;
        _originalPos += direction * Time.deltaTime * _speed * GameStat.gameTimeScale;

        Vector2 heighVector = _offset * _normalVector;
        if (distance / _totalDistance > 0.5f)
        {
            heighVector = -heighVector;
        }
        if(distance / _totalDistance < 0.6 &&  distance / _totalDistance > 0.4)
        {
            delta = 0.1f;
        }
        else if (distance / _totalDistance < 0.7 && distance / _totalDistance > 0.3)
        {
            delta = 0.3f;
        }
        if (Vector2.Distance(_originalPos, _target) < 0.1f)
        {
            direction = (Vector2)(_target - transform.position).normalized;
        }
        transform.position += (Vector3)(direction + heighVector* delta) * Time.deltaTime * _speed * GameStat.gameTimeScale;

        //rotate
        direction = ((Vector2)_target - _startPos).normalized;
        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float rad = 270f;
        transform.rotation = Quaternion.Euler(0, 0, rot + rad);
        //
    }
}
