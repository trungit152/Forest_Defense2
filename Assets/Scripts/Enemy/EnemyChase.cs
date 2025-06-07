using System.Collections;
using UnityEngine;
public class EnemyChase : MonoBehaviour
{
    [SerializeField] protected float _range = 1;
    protected float _rangeRandomize = 1;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected LayerMask wallLayer;
    protected Transform player;
    [SerializeField] private float _attackCd;
    [SerializeField] protected GameObject _exclamationMark;

    protected bool _canFind = true;
    private float _cd = 0;
    protected bool isAttackPlayer;
    protected bool isAttackWall;
    protected bool _canWalk = true;
    protected bool _canCountCd = true;
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
    protected virtual void FixedUpdate()
    {
        if (GameStat.gameTimeScale > 0 && _canFind)
        {
            FindPlayer();
        }
    }
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
    public void Init(float cd)
    {
        _attackCd = cd;
    }
    protected virtual void FindPlayer()
    {
        if (_cd > 0 && _canCountCd)
        {
            _cd -= Time.deltaTime;
        }
        Collider2D playerHit = Physics2D.OverlapCircle(Enemy.GetCenter().position, _rangeRandomize, playerLayer); 
        Collider2D wallHit = Physics2D.OverlapCircle(Enemy.GetCenter().position, Mathf.Max(0.5f, _rangeRandomize - 0.5f), wallLayer);
        if (wallHit != null)
        {
            EnemyAttack.TurnOnCollideAI(false);
            EnemyAnimation.SetIdle();
            EnemyAnimation.FaceToPlayer(EnemyAI.GetTarget().position);
            if (_cd <= 0)
            {
                isAttackPlayer = false;
                isAttackWall = true;
                IsTargetWall();
                EnemyAnimation.SetAttack();
                _cd = _attackCd;

            }
            return;
        }
        else if (playerHit != null)
        {
            if (!isAttackPlayer)
            {
                EnemyAnimation.SetDetect();
            }
            EnemyAttack.TurnOnCollideAI(false);
            EnemyAnimation.SetIdle();
            EnemyAnimation.FaceToPlayer(MultiplayerSpawner.localPlayer.PlayerHealth.GetPlayerCenter().position);
            if (_cd <= 0)
            {
                isAttackPlayer = true;
                isAttackWall = false;
                IsTargetWall();
                EnemyAnimation.SetAttack();
                _cd = _attackCd;
            }
            return;
        }
        else
        {
            EnemyAttack.TurnOnCollideAI(true);
            SetCanCountCd(true);
            isAttackPlayer = false;
            isAttackWall = false;
        }
        if (EnemyAI.HavePath() && _canWalk)
        {
            EnemyAnimation.SetWalk();
        }
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(Enemy.GetCenter().position, _rangeRandomize);
    //}
    public Transform GetPlayerTransform()
    {
        if (player == null)
        {
            return null;
        }
        return player.transform;
    }
    public virtual void CountAttack()
    {
        //
    }
    public virtual void SetIsBulling()
    {

    }
    public bool IsTargetWall()
    {
        if (isAttackWall)
        {
            EnemyAttack.SetAttackWall();
            return true;
        }
        EnemyAttack.SetAttackPlayer();
        return false;
    }
    public void Init()
    {
        _canWalk = true;
        _rangeRandomize = Random.Range(_range - _range / 6, _range);
    }
    public void SetCanCountCd(bool b)
    {
        _canCountCd = b;
    }
    private void TurnOnExclamation(float time = 1f)
    {
        StartCoroutine(TurnOnExclamationCoroutine(time));
    }
    private IEnumerator TurnOnExclamationCoroutine(float time)
    {
        if(_exclamationMark == null)
        {
            yield break;
        }
        _exclamationMark.SetActive(true);
        yield return new WaitForSeconds(time);
        _exclamationMark.SetActive(false);
    }
}