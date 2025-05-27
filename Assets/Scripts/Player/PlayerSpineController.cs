using Spine.Unity;
using System.Collections;
using UnityEngine;
using System;
public class PlayerSpineController : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _skeletonAnimation;
    [SerializeField] private float _attackDuration = 0.533f;
    [SerializeField] private float _walkAttackDuration = 0.533f;
    [SerializeField] private Transform _particle;
    public Action actAttack;

    private PlayerShoot _playerShoot;
    public PlayerShoot PlayerShoot
    {
        get
        {
            if (_playerShoot == null)
            {
                _playerShoot = GetComponent<PlayerShoot>();
            }
            return _playerShoot;
        }
        set
        {
            _playerShoot = value;
        }
    }
    private PlayerMeleAttack _playerMeleAttack;
    public PlayerMeleAttack PlayerMeleAttack
    {
        get
        {
            if (_playerMeleAttack == null)
            {
                _playerMeleAttack = GetComponent<PlayerMeleAttack>();
            }
            return _playerMeleAttack;
        }
        set
        {
            _playerMeleAttack = value;
        }
    }
    private void Start()
    {
    }
    public void SetAttack(float cd = 1f)
    {
        string status = _skeletonAnimation.AnimationState.GetCurrent(0)?.Animation.Name;
        if (status != "attack")
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, "attack", false);
            _skeletonAnimation.AnimationState.AddAnimation(0, "idle", true, 0);
            actAttack = Attack;
            actAttack?.Invoke();
        }
    }
    public void SetWalkAtack(float cd = 1)
    {
        string status = _skeletonAnimation.AnimationState.GetCurrent(0)?.Animation.Name;
        if (status != "walk_attack")
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, "walk_attack", false);
            _skeletonAnimation.AnimationState.AddAnimation(0, "walk", true, 0);
            actAttack = Attack;
            actAttack?.Invoke();
        }
    }
    public void SetIdle()
    {
        string status = _skeletonAnimation.AnimationState.GetCurrent(0)?.Animation.Name;
        if (status != "idle")
        {
            _skeletonAnimation.AnimationState.TimeScale = 1;
            _skeletonAnimation.AnimationState.SetAnimation(0, "idle", true);
        }
    }
    public void SetWalk()
    {
        _skeletonAnimation.AnimationState.TimeScale = 1;
        string status = _skeletonAnimation.AnimationState.GetCurrent(0)?.Animation.Name;
        if (status != "walk")
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, "walk", true);
        }
    }
    public void CheckLeftRightRotation(Vector2 direction)
    {
        if (direction.x > 0)
        {
            _skeletonAnimation.Skeleton.ScaleX = -1;
        }
        else
        {
            _skeletonAnimation.Skeleton.ScaleX = 1;
        }
    }
    Coroutine croAttack;
    private void Attack()
    {
        if (croAttack != null)
        {
            StopCoroutine(croAttack);
        }
        croAttack = StartCoroutine(DelayAttack());
    }
    IEnumerator DelayAttack()
    {
        if (PlayerShoot != null && PlayerShoot.gameObject.activeSelf)
        {
            if (PlayerShoot.GetAttackCoolDown() > _walkAttackDuration / 1.7f)
            {
                yield return new WaitForSeconds(_walkAttackDuration / 1.7f);
                PlayerShoot.Shoot();
            }
            else
            {
                yield return new WaitForSeconds(PlayerShoot.GetAttackCoolDown());
                PlayerShoot.Shoot();
                SkipToNextAnimation();
            }
        }
        else if (PlayerMeleAttack != null && PlayerMeleAttack.gameObject.activeSelf)
        {
            if (PlayerMeleAttack.GetAttackCoolDown() > _walkAttackDuration / 1.7f)
            {
                yield return new WaitForSeconds(_walkAttackDuration / 1.7f);
                PlayerMeleAttack.Shoot();
            }
            else
            {
                yield return new WaitForSeconds(PlayerShoot.GetAttackCoolDown());
                PlayerMeleAttack.Shoot();
                SkipToNextAnimation();
            }
        }
    }
    private void SkipToNextAnimation()
    {
        var currentEntry = _skeletonAnimation.AnimationState.GetCurrent(0);
        if (currentEntry != null)
        {
            currentEntry.TrackTime = currentEntry.AnimationEnd;
        }
    }
    public string GetCurrentStatus()
    {
        string status = _skeletonAnimation.AnimationState.GetCurrent(0)?.Animation.Name;
        return status;
    }
    public bool IsPlayingAttack()
    {
        string status = _skeletonAnimation.AnimationState.GetCurrent(0)?.Animation.Name;
        if (status == "attack" || status == "walk_attack") return true;
        return false;
    }
    public void StopAttack()
    {
        if (croAttack != null)
        {
            StopCoroutine(croAttack);
            croAttack = null;
        }

        string status = _skeletonAnimation.AnimationState.GetCurrent(0)?.Animation.Name;
        if (status == "attack" || status == "walk_attack")
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, "idle", true);
        }
    }

    public void ChangeSkin(int type)
    {
        if (type == 0)
        {
            _skeletonAnimation.Skeleton.SetSkin("weapon1");
        }
        else if (type == 1)
        {
            _skeletonAnimation.Skeleton.SetSkin("weapon2");
        }
        else
        {
            Debug.LogError("Invalid skin type");
        }
    }
}
