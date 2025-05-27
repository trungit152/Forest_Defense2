using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    
    public void Die()
    {
        if( _enemy != null)
        {
            _enemy.Die();
        }
    }
    public void SetIdle()
    {
        _enemy.SetIdle();
    }
    public void SetStatic()
    {
        _enemy.SetStatic(); 
    }
    public void TakeDamage()
    {
        _enemy.TakeDamage();
    }
    public void SpawnBullet()
    {
        _enemy.SpawnBullet();
    }
    public void SetCantMove()
    {
        _enemy.SetCanmove(false);
    }
    public void SetCanMove()
    {
        _enemy.SetCanmove(true);
    }
    public void SetAmorBull()
    {
        _enemy.EnemyChase.SetIsBulling();
    }
    public void CanCountCd()
    {
        _enemy.EnemyChase.SetCanCountCd(true);
    }
    public void CantCountCd()
    {
        _enemy.EnemyChase.SetCanCountCd(false);
    }
}
