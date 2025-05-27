using UnityEngine;

public class TrapAnimationEvent : MonoBehaviour
{
    [SerializeField] private Traps _trap;
    private Animator _animator;
    public Animator Animator
    {
        get
        {
            if(_animator == null)
            {
                _animator = GetComponent<Animator>();
            }
            return _animator;
        }
        set
        {
            _animator = value;
        }
    }

    public void SetAttack()
    {
        Animator.SetTrigger("attack");
    }

    public void TakeDamage()
    {
        _trap.EffectEnemy();
    }
}
