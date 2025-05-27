using Spine.Unity;
using System;
using System.Collections;
using UnityEngine;

public class TurretSpineController : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _skeletonAnimation;
    [SerializeField] private float _attackDuration = 0.133f;
    [SerializeField] private float _callFunctionScale = 2f;
    private Action actShoot;
    private Turrets _turrets;
    public Turrets Turrets
    {
        get
        {
            if (_turrets == null)
            {
                _turrets = GetComponent<Turrets>();
            }
            return _turrets;
        }
        set
        {
            _turrets = value;
        }
    }

    private AttackObject _attackObject;
    public AttackObject AttackObject
    {
        get
        {
            if (_attackObject == null)
            {
                _attackObject = GetComponent<AttackObject>();
            }
            return _attackObject;
        }
        set
        {
            _attackObject = value;
        }
    }
    public void SetShootAnimation()
    {
        if (_skeletonAnimation != null)
        {
            actShoot = Shoot;
            actShoot?.Invoke();
            if (AttackObject.GetNeareastEnemy() != null || Turrets.IsHavingEnemy())  
            {
                _skeletonAnimation.AnimationState.SetAnimation(0, "attack", false);
                _skeletonAnimation.AnimationState.AddAnimation(0, "idle", true, _attackDuration);
            }
        }
    }
    public void SetDropped()
    {
        if (_skeletonAnimation.Skeleton.Data.FindAnimation("drop") != null)
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, "drop", false);
        }
        _skeletonAnimation.AnimationState.AddAnimation(0, "idle", true, _attackDuration);
    }
    Coroutine croShoot;
    private void Shoot()
    {
        if (croShoot != null)
        {
            StopCoroutine(croShoot);
        }
        croShoot = StartCoroutine(DelayShoot());
    }

    IEnumerator DelayShoot()
    {
        Turrets.ChangeVisual();
        yield return new WaitForSeconds(_attackDuration/(_callFunctionScale!=0?_callFunctionScale:2));
        Turrets.Shoot();
    }
    public void ChangeDataAsset(SkeletonAnimation skeletonAnimation)
    {
        _skeletonAnimation = skeletonAnimation;
    }
    public void ChangeDataAsset(GameObject skeletonAnimation)
    {
        _skeletonAnimation = skeletonAnimation.GetComponent<SkeletonAnimation>();
    }
    
}
