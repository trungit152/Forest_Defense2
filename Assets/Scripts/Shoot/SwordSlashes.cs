using System.Collections;
using UnityEngine;

public class SwordSlashes : MonoBehaviour
{
    private Transform _target;
    private Vector3 _targetPosition;
    [SerializeField] private PolygonCollider2D _attackCollider;
    [SerializeField] private float _speed = 0.3f;
    [SerializeField] private float _damage;
    [SerializeField] private float _visualExistTime = 0.4f;
    [SerializeField] private float _colliderExistTime = 0.1f;
    private void OnEnable()
    {
        _targetPosition = Vector3.zero;
        _attackCollider.enabled = true;
        SetDestroy(_visualExistTime);
        SetOffCollider(_colliderExistTime);
    }
    public void SetTarget(Transform t)
    {
        _target = t;
        _targetPosition = t.position;
    }
    public void RemoveTarget()
    {
        _target = null;
    }
    public void SetSpeed(float s)
    {
        _speed = s;
    }
    private void FixedUpdate()
    {
        MoveToTarget();
    }
    private void MoveToTarget()
    {
        if (_target != null)
        {
            MoveAndRotate(_targetPosition);
        }
    }
    private void MoveAndRotate(Vector3 _target)
    {
        Vector2 direction = transform.position - _target;
        transform.position = Vector2.MoveTowards(transform.position, _target, _speed * Time.fixedDeltaTime * GameStat.gameTimeScale);
        //rotate
        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }
    public float GetDamage()
    {
        return _damage;
    }

    private void SetDestroy(float f)
    {
        StartCoroutine(DelayDestroy(f));
    }
    IEnumerator DelayDestroy(float f)
    {
        yield return new WaitForSeconds(f);
        gameObject.SetActive(false);
    }
    private void SetOffCollider(float f)
    {
        StartCoroutine(DelayOffCollider(f));
    }
    IEnumerator DelayOffCollider(float f)
    {
        yield return new WaitForSeconds(f);
        _attackCollider.enabled = false;
    }
}
