using UnityEngine;
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void SetHit()
    {
        _animator.SetTrigger("hit");
    }
}
