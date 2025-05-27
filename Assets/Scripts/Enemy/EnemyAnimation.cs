using Spine;
using UnityEngine;
public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Transform _visual;
    public void SetIdle()
    {
        _animator.SetBool("isIdle", true);
        _animator.SetBool("isWalk", false);
        _enemy.SetCanmove(false);
    }
    public void SetWalk()
    {
        if (!_animator.GetBool("isWalk"))
        {
            _animator.SetBool("isIdle", false);
            _animator.SetBool("isWalk", true);
            //float randomTime = Random.Range(0f, 1f);
            //_animator.Play("Walk", 2, randomTime);
            _enemy.SetCanmove(true);
        }
    }
    public void SetDetect()
    {
        _animator.SetTrigger("detect");
    }
    public void SetAttack()
    {
        _animator.SetTrigger("attack"); 
    }
    public void SetHurt()
    {
        _animator.SetTrigger("hurt");
    }
    public void SetDie()
    {
        _animator.SetTrigger("die");
    }
    public void CheckLeftRightRotation(Vector2 direction)
    {
        if (direction.x > 0)
        {
            _visual.SetPositionAndRotation(_visual.position, Quaternion.Euler(0, 180, 0));

        }
        else
        {
            _visual.SetPositionAndRotation(_visual.position, Quaternion.Euler(0, 0, 0));
        }
    }
    public void EnabledAnimator(bool isEnabled)
    {
        _animator.enabled = isEnabled;  
    }
    public void FaceToPlayer(Vector3 playerPos)
    {
        CheckLeftRightRotation(playerPos - transform.position);
    }
    public bool GetBool(string name) 
    {
        return _animator.GetBool(name);
    }
    public Animator GetAnimator()
    {
        return _animator;
    }
    public void ChangeAnimationSpeed(float speed)
    {
        _animator.speed = speed;
    }
    public bool IsPlayingHurt() 
    {
        return _animator.GetCurrentAnimatorStateInfo(1).IsName("enemy_hit");
    }
}
