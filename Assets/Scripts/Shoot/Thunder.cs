using DamageNumbersPro;
using System.Collections;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    [SerializeField] private float _existTime = 0.2f;
    [SerializeField] private float _damage = 5;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private DamageNumber _normalNumber;
    [SerializeField] private DamageNumber _critNumber;
    private float _stunPercent;
    private float _stunTime;
    private bool _isCrit;
    public void SetTransformToTarget(float angle)
    {
        transform.rotation = Quaternion.Euler(0,0, angle);
    }

    public void SetDamage(float damage, bool isCrit)
    {
        isCrit = _isCrit;
        _damage = damage;
    }
    public void SetStun(float percent, float time)
    {
        _stunPercent = percent;
        _stunTime = time;
    }
    public float GetDamage()
    {
        return _damage;
    }
    public IEnumerator MoveLineRenderer(Vector3 from, Enemy to, float speed = 30f)
    {
        Vector3 localFrom = transform.InverseTransformPoint(from);
        Vector3 localTo = transform.InverseTransformPoint(to.GetCenter().position);

        _lineRenderer.SetPosition(0, localFrom);
        _lineRenderer.SetPosition(1, localFrom);

        while (Vector2.Distance(localTo, _lineRenderer.GetPosition(1)) > 0.05f)
        {
            _lineRenderer.SetPosition(1, Vector2.MoveTowards(_lineRenderer.GetPosition(1), localTo, Time.deltaTime * speed));
            yield return null;
        }
        if (FeelingTools.RandomChance(_stunPercent))
        {
            to.Stun(_existTime);
        }
        ShockEnemy(to, _damage);
        while (Vector2.Distance(localTo, _lineRenderer.GetPosition(0)) > 0.1f)
        {
            _lineRenderer.SetPosition(0, Vector2.MoveTowards(_lineRenderer.GetPosition(0), localTo, Time.deltaTime * speed));
            yield return null;
        }
        gameObject.SetActive(false);
    }
    private void ShockEnemy(Enemy enemy, float damage)
    {
        if (enemy != null && enemy.gameObject.activeSelf)
        {
            if (_isCrit)
            {
                enemy.SetDamageNumber(_critNumber);
            }
            else
            {
                enemy.SetDamageNumber(_normalNumber);
            }
            enemy.LoseHealth(damage);
            enemy.Shock();
        }
    }
    public void SetCrit(bool isCrit)
    {
        _isCrit = isCrit;
    }
}
