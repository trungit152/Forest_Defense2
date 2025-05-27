using System.Collections.Generic;
using UnityEngine;

public class GroundSpike : Traps
{
    private List<Enemy> _enemyList;
    private void Start()
    {
        _enemyList = new List<Enemy>();
    }

    private void FixedUpdate()
    {
        if (_cd <= 0)
        {
            _trapAnimationEvent.SetAttack();
            _cd = _coolDown;
        }
        else
        {
            _cd -= Time.deltaTime * GameStat.gameTimeScale;
        }
    }

    public override void EffectEnemy()
    {
        foreach (Enemy enemy in _enemyList)
        {
            enemy.SetDamageNumber(_damageNumber);
            enemy.LoseHealth(_damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                _enemyList.Add(enemy);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                _enemyList.Remove(enemy);
            }
        }
    }
}
