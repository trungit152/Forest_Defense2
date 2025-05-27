using DamageNumbersPro;
using UnityEngine;

public class Traps : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] protected float _damage = 10;
    [SerializeField] protected float _coolDown = 1.5f;
    [SerializeField] protected TrapAnimationEvent _trapAnimationEvent;

    [SerializeField] protected DamageNumber _damageNumber;
    protected float _cd;
    public virtual void EffectEnemy() { }

    public int ID()
    {
        return _id;
    }

    public void IncreaseATK(float percent)
    {
        _damage += _damage * percent;
    } 
    public void IncreaseATKS(float percent)
    {
        float AS = 1 / (_coolDown!=0?_coolDown:0.01f);
        AS -= AS * percent;
        _coolDown = 1 / AS;
    }

    public virtual void SpecialEffect1(float percent)
    {

    }
}
