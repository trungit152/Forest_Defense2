using UnityEngine;

public class EnemyStatusController : MonoBehaviour
{
    private Transform player;

    private EnemyAttack _enemyAttack;
    public EnemyAttack EnemyAttack
    {
        get
        {
            if (_enemyAttack == null)
            {
                _enemyAttack = GetComponent<EnemyAttack>();
            }
            return _enemyAttack;
        }
        set
        {
            _enemyAttack = value;
        }
    }

    private EnemyChase _enemyChase;
    public EnemyChase EnemyChase
    {
        get
        {
            if (_enemyChase == null)
            {
                _enemyChase = GetComponent<EnemyChase>();
            }
            return _enemyChase;
        }
        set
        {
            _enemyChase = value;
        }
    }
}
