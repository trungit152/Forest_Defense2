using Pathfinding.RVO;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] protected float _damage = 5f;
    [SerializeField] protected GameObject _collideAI;
    private bool isAttackPlayer;
    private bool isAttackWall;
    protected float _cd = 0;

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
    private EnemyAI _enemyAI;
    public EnemyAI EnemyAI
    {
        get
        {
            if (_enemyAI == null)
            {
                _enemyAI = GetComponent<EnemyAI>();
            }
            return _enemyAI;
        }
        set
        {
            _enemyAI = value;
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
    public void Init(float damage, float cd)
    {
        _damage = damage;
        EnemyChase.Init(cd);
    }
    public void TakeDamage(float damage = -1)
    {

        if (damage < 0) damage = _damage;
        if (isAttackPlayer && !MultiplayerSpawner.localPlayer.PlayerHealth.IsDeath())
        {
            AudioManager.instance.PlaySoundEffect("EnemyPunch");
            Enemy.SetAttackImpactPosition(GetAttackImpactPos());
            MultiplayerSpawner.localPlayer.PlayerHealth.LoseHealth(damage);
        }
        else if(isAttackWall)
        {
            AudioManager.instance.PlaySoundEffect("EnemyPunch");
            Enemy.SetAttackImpactPosition(EnemyAI.GetTarget().position);
            WallControl.instance.Attacked(damage);
        }
    }
    public void TakeDamagePlayer(float damage = -1)
    {
        if (!MultiplayerSpawner.localPlayer.PlayerHealth.IsDeath())
        {
            if (damage < 0) damage = _damage;
            Enemy.SetAttackImpactPosition(GetAttackImpactPos());
            MultiplayerSpawner.localPlayer.PlayerHealth.LoseHealth(damage);
        }
    }
    public void TakeDamageWall(float damage = -1)
    {
        if (damage < 0) damage = _damage;
        Enemy.SetAttackImpactPosition(EnemyAI.GetTarget().position);
        WallControl.instance.Attacked(damage);
    }
    private Vector3 GetAttackImpactPos()
    {
        Vector3 playerCenter = MultiplayerSpawner.localPlayer.PlayerHealth.GetPlayerCenter().position;
        Vector3 directionToObject = (transform.position - playerCenter).normalized; 
        return playerCenter + directionToObject * 0.3f; 
    }
    public virtual void SpawnBullet() { }
    public void SetAttackPlayer()
    {
        isAttackPlayer = true;
        isAttackWall = false;
    }
    public void SetAttackWall()
    {
        isAttackWall = true;
        isAttackPlayer = false;
    }
    public void TurnOnCollideAI(bool b)
    {
        if (_collideAI != null)
        {
            _collideAI.SetActive(b);
        }
    }
}
