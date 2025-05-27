using System.Collections.Generic;
using UnityEngine;

public class EnemiesAnimator : MonoBehaviour
{
    private List<Animator> _enemyAnimators;
    public static EnemiesAnimator instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        _enemyAnimators = new List<Animator>();
    }

    public void AddAnimator(Animator animator)
    {
        _enemyAnimators.Add(animator);
    }
    public void RemoveAnimator(Animator animator)
    {
        _enemyAnimators.Remove(animator);
    }
    public void ChangeAnimatorSpeed(float t)
    {
        foreach (var animator in _enemyAnimators)
        {
            animator.speed = t;
        }
    }
}
