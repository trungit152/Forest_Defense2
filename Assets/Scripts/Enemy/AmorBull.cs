using UnityEngine;

public class AmorBull : EnemyChase
{
    [SerializeField] private float _skillRange;
    [SerializeField] private float _skillDamage;
    [SerializeField] private Animator _smokeAnimator;
    [SerializeField] private GameObject _dangerAreaPref;
    private Vector3 _bullTarget;
    private int _canBull = 0;
    private bool _isBulling = false;
    private RectTransform _hpBar;
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (GameStat.gameTimeScale > 0 && _canBull == 0)
        {
            BullPlayer();
        }
        BullMove();
    }
    private GameObject _dangerArea;
    private void BullPlayer()
    {
        if (_canBull == 0)
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, _skillRange, playerLayer);
            if (hit != null)
            {
                if (CheckCanBull(hit.transform))
                {
                    //danger area
                    _dangerArea = Instantiate(_dangerAreaPref);
                    float distance = Vector3.Distance(transform.position, hit.transform.position);
                    _dangerArea.transform.localScale = new Vector3(1, distance, 1);
                    _dangerArea.transform.position = MyMath.GetMiddleVector(transform.position, hit.transform.position);
                    float angle = MyMath.GetAngleBetweenObjects(hit.transform, transform);
                    _dangerArea.transform.rotation = Quaternion.Euler(0, 0, angle+90); 

                    //
                    _bullTarget = hit.transform.position;
                    EnemyAnimation.SetIdle();
                    EnemyAnimation.GetAnimator().SetBool("isBulling", true);
                    _canWalk = false;
                    EnemyAnimation.GetAnimator().SetTrigger("charge");
                    _canFind = false;
                    EnemyAnimation.FaceToPlayer(_bullTarget);
                    _smokeAnimator.transform.parent.gameObject.SetActive(true);
                    _canBull = 4;
                }
            }
        }
    }

    private void BullMove()
    {
        if (_isBulling)
        {
            transform.position = Vector2.MoveTowards(transform.position, _bullTarget, EnemyAI.GetSpeed() * 15 * Time.deltaTime * GameStat.gameTimeScale);
            EnemyAnimation.FaceToPlayer(_bullTarget);
            if (Vector2.Distance(transform.position, _bullTarget) < 0.55f)
            {
                _canFind = true;
                _isBulling = false;
                Destroy(_dangerArea);
                CheckWentBullDone();
                EnemyAnimation.GetAnimator().SetBool("isBulling", false);
                _canWalk = true;
                _smokeAnimator.SetTrigger("end");
                EnemyAI.UpdatePath();
            }
        }
    }

    private void CheckWentBullDone()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, _range, playerLayer);
        if (hit != null)
        {
            EnemyAttack.TakeDamagePlayer(_skillDamage);
        }
    }

    public override void CountAttack()
    {
        if(_canBull > 0)
        {
            _canBull--;
        }
    }
    public override void SetIsBulling()
    {
        _isBulling = true;
    }
    private bool CheckCanBull(Transform target)
    {
        Vector2 startPosition = transform.position;
        Vector2 targetPosition = target.position;
        Vector2 direction = (targetPosition - startPosition).normalized;
        float distance = Vector2.Distance(startPosition, targetPosition);

        RaycastHit2D hit = Physics2D.Raycast(startPosition, direction, distance, LayerMask.GetMask("Block"));

        if (hit.collider != null)
        {
            return false; 
        }

        return true; 
    }
}
