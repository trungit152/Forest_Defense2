using UnityEngine;

public class EnemyBoss : Enemy
{
    private RectTransform _hpBar;
    private RectTransform _hpBarBorder;

    public override void SetHpBar(RectTransform hpBarBorder, RectTransform hpBar)
    {
        Debug.Log("set hp bar");
        _hpBarBorder = hpBarBorder;
        _hpBar = hpBar;
        _hpBarBorder.gameObject.SetActive(true);
    }

    public override void UpdateHpBar()
    {
        if (_hpBar != null)
        {
            ChangeRight(_hpBar, _hpBarBorder.sizeDelta.x - (_hp * _hpBarBorder.sizeDelta.x / _maxHp));
        }
    }
    private void ChangeRight(RectTransform rectTransform, float right)
    {
        {
            Vector2 offset = rectTransform.offsetMax;
            offset.x = -right;
            rectTransform.offsetMax = offset;
        }
    }
    public override void Die()
    {
        _hpBarBorder.gameObject.SetActive(false);
        base.Die();
    }
}
