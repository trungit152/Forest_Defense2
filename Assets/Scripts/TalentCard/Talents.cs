using UnityEngine;

public class Talents : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private string _description;
    [SerializeField] private string _title;
    [SerializeField] private Sprite _icon;    
    [SerializeField] private Sprite _frame;
    [SerializeField] private Sprite _background;    
    protected int _stacks = 0;
    private int _maxStack = 5;
    public void Init()
    {
        _stacks = 0;
        _maxStack = 5;
    }
    public void InitStat(string description)
    {
        _description = description;
    }
    public void LevelUp()
    {
        ++_stacks;
    }
    public virtual void Active()
    {
        _stacks++;
    }
    public bool IsMax()
    {
        if(_stacks >= _maxStack) return true;
        return false;
    }
    public string GetDescription()
    {
        return _description;
    }
    public Sprite GetIcon()
    {
        return _icon;
    }
    public int GetStacks() { return _stacks; }
    public Sprite GetFrame() { return _frame; }
    public Sprite GetBackground() { return _background; }
    public string GetTitle()
    {
        return _title;
    }
}
