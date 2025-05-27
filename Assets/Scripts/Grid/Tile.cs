using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _tempBlock;
    private Vector2 _positionInList;
    private Type _type;
    private Turrets _turret;
    public Turrets Turret { get => _turret; }
    public enum Type
    {
        Way,
        Wall,
        Turret,
        Trap,
        Block,  
        Pivot
    }
    private TileRayCast _tileRayCast;
    public TileRayCast TileRayCast
    {
        get
        {
            if (_tileRayCast == null)
            {
                _tileRayCast = GetComponent<TileRayCast>();
            }
            return _tileRayCast;
        }
        set
        {
            _tileRayCast = value;
        }
    }
    public void Init()
    {
        _tempBlock.SetActive(false);
        _type = Type.Way;
        TileRayCast.Init();
    }
    public void SetPosition(float i, float j)
    {
        _positionInList = GridManager.instance.GetGridIndex(new Vector2(i, j)) - new Vector2(1, 1);
    }

    public void Highlight(bool isHighlight, bool canBlock = true)
    {
        if (_highlight != null)
        {
            _highlight.SetActive(isHighlight);
        }
        if (isHighlight && canBlock)
        {
            AddTempBlock();
        }
        else if (canBlock)
        {
            RemoveTempBlock();
        }
    }
    private void OnMouseDown()
    {
        Debug.Log(this.ToString());
    }
    public override string ToString()
    {
        if (_positionInList != null)
        {
            return $"tile {_positionInList.x} {_positionInList.y}";
        }
        else
        {
            return "null";
        }
    }
    public virtual void SetType(Type type)
    {
        _type = type;
        if(_type != Type.Turret)
        {
            _turret = null;
        }
    }
    public void SetTurret(Turrets turret)
    {
        _turret = turret;
    }
    public Vector2 GetPositionInList()
    {
        return _positionInList;
    }
    public void ChangeHighlightColor(Color color)
    {
        var highlightSr = _highlight.GetComponent<SpriteRenderer>();
        highlightSr.color = color;
        FeelingTools.ChangeAlpha(highlightSr, 0.5f);
    }
    public void Show(bool isShown = true)
    {
        gameObject.SetActive(isShown);
    }
    public Type GetTileType()
    {
        return _type;
    }
    public void AddTempBlock()
    {
        _tempBlock.SetActive(true);
    }
    public void RemoveTempBlock()
    {
        _tempBlock.SetActive(false);
    }
}
