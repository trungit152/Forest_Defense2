using System.Collections.Generic;
using UnityEngine;

public class FirePressure : Traps
{
    [SerializeField] private float _cdFixed = 0.1f;
    private List<Enemy> _enemyList;
    [SerializeField] private List<Animator> _fires;
    private void Start()
    {
        _enemyList = new List<Enemy>();
        _cd = _cdFixed;
    }
    private void FixedUpdate()
    {
        if (_enemyList.Count > 0)
        {
            CountAttack();
        }
    }
    private void CountAttack()
    {
        if (_cd <= 0)
        {
            TakeDamage();
            _cd = _cdFixed;
        }
        else
        {
            _cd -= Time.deltaTime * GameStat.gameTimeScale;
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
                if (_enemyList.Count == 1)
                {
                    TurnOnFire();
                }
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
                if (_enemyList.Count == 0)
                {
                    TurnOffFire();
                }
            }
        }
    }
    private void TurnOnFire()
    {
        foreach (var fire in _fires)
        {
            fire.gameObject.SetActive(true);
        }
    }
    private void TurnOffFire()
    {
        foreach (var fire in _fires)
        {

            fire.SetTrigger("off");
        }
    }

    private void TakeDamage()
    {
        for (int i = 0; i < _enemyList.Count; i++)
        {
            if (!_enemyList[i].IsDie())
            {
                _enemyList[i].LoseHealth(_damage, false);
                if (_enemyList[i].GetCurrentHp() <= 0)
                {
                    _enemyList[i].Die();
                }
            }
        }
        foreach (var enemy in _enemyList)
        {
            if(enemy != null && enemy.gameObject.activeSelf)
            {

            }
        }
    }
}
