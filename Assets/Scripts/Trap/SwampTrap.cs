using System.Collections.Generic;
using UnityEngine;

public class SwampTrap : Traps
{
    [SerializeField] private float _slowPercent = 25f;
    private Dictionary<Enemy, float> _enemyList;
    private void Start()
    {
        _enemyList = new Dictionary<Enemy, float>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();   
            if (enemy != null)
            {
                _enemyList.Add(enemy, 0);
                enemy.SlowDown(_slowPercent);
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
                enemy.SetBasicSpeed();
            }
        }
    }
}
