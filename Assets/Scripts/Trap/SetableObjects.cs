using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class SetableObjects : MonoBehaviour
{
    protected Transform _keyPivot;
    [SerializeField] protected Transform _initPivotPoint;
    [SerializeField] protected Turrets _turretObject;
    [SerializeField] protected Traps _trapsObject;
    [SerializeField] private GameObject _attackRange;
    [SerializeField] protected GameObject _pivotPref;
    [SerializeField] protected GameObject _tempBlock;

    protected List<GameObject> _tempBlocks;
    protected bool _canSet;
    protected bool _canMerge;
    protected bool _isSet;
    protected int _listCount;
    protected int _listCountConst;
    protected bool _isClicked = true;
    protected List<Transform> _pivots;
    protected List<Tile> _highlightTiles;
    protected Transform _allTurrets;
    private Vector3 _initialPosition, _offset;

    protected bool _isShownUnderBlock;

    private float _rescanTimeFix = 0.1f;
    private float _rescanTime = 0f;
    private void Awake()
    {
        _pivots = new List<Transform>();
        _highlightTiles = new List<Tile>();
        _allTurrets = GameObject.Find("Turrets").transform;
    }
    private void Start()
    {
        InitPivot();
        //TurnOnPivot(false);
    }
    private void Update()
    {
        CheckHighlight();
        CheckPivotOnGrid();
        AstarPathControl.instance.ScanAll();
    }
    public void RescanMap()
    {
        if (_rescanTime < _rescanTimeFix)
        {
            _rescanTime += Time.deltaTime;
        }
        else
        {
            Scan();
            _rescanTime = 0f;
        }
    }
    public void Scan()
    {
        Bounds bounds = new Bounds(transform.position, Vector3.one * 2);
        AstarPathControl.instance.RescanMap(bounds);
    }
    public void Init()
    {
        if (_attackRange != null)
        {
            _attackRange.SetActive(false);
        }
        TurnOnPivot(true);
        _isClicked = true;
        _initialPosition = transform.position;
        _offset = _initialPosition - MouseController.instance.GetMouseWorldPosition();
    }
    protected void CheckPivotOnGrid()
    {
        if (IsOnGrid())
        {
            _listCount = _listCountConst;
        }
        else
        {
            _listCount = 0;
            foreach (var tile in _highlightTiles)
            {
                tile.Highlight(false);
            }
            _highlightTiles.Clear();
        }
    }
    protected bool IsOnGrid()
    {
        foreach (var pos in _pivots)
        {
            if (pos.position.y > GridManager.instance.GetTopPos() || pos.position.y < GridManager.instance.GetBotPos()
    || pos.position.x > GridManager.instance.GetRightPos() || pos.position.x < GridManager.instance.GetLeftPos())
            {
                return false;
            }
        }
        return true;
    }
    protected virtual void OnMouseUp()
    {
        TurnOnPivot(false);
        _isClicked = false;
        TurretManager.instance.HideMergeArrow();
    }

    public void ShowAttackRange(bool b)
    {
        if (_attackRange != null)
        {
            _attackRange.SetActive(b);
        }
    }
    protected virtual void InitPivot()
    {
        //override (1x1), (2x2), (3x3),....
    }

    protected virtual void CreatePoint()
    {
        //override (1x1), (2x2), (3x3),....
    }
    protected virtual void CheckCanSet()
    {
        //
    }
    protected virtual void CheckHighlight()
    {
        CheckCanSet();
        foreach (var p in _pivots)
        {
            Tile curTile = GridManager.instance.GetTileInRange(p.position);
            if (curTile != null)
            {
                //set astar block
                CheckColor(curTile);
                if (!_highlightTiles.Contains(curTile))
                {
                    _highlightTiles.Add(curTile);
                    if (_turretObject != null)
                    {
                        curTile.Highlight(true, true);
                    }
                    else if (_trapsObject != null)
                    {
                        curTile.Highlight(true, false);
                    }
                    HighlightListCheck();
                }
            }
        }
    }
    protected void CheckColor(Tile curTile)
    {
        if (_canSet || _canMerge)
        {
            curTile.ChangeHighlightColor(Color.green);
        }
        else
        {
            curTile.ChangeHighlightColor(Color.red);
        }
    }
    protected virtual void HighlightListCheck()
    {
        if (_highlightTiles.Count > _listCount)
        {
            while (_highlightTiles.Count > _listCount)
            {
                _highlightTiles[0].Highlight(false);
                _highlightTiles.RemoveAt(0);
            }
        }
    }
    public virtual void SetOnPosition()
    {
        //
    }
    protected virtual void Transfer()
    {
        //
    }
    protected virtual void Merge()
    {
        //override
    }
    protected virtual void TransferToTrap()
    {
        var trap = Instantiate(_trapsObject, _allTurrets);
        trap.transform.position = transform.position;
        Destroy(gameObject);
    }
    private void TurnOnPivot(bool isOn)
    {
        foreach (var p in _pivots)
        {
            p.gameObject.SetActive(isOn);
        }
    }
    public virtual void SetTurretCardUI(CardUI cardUI)
    {

    }
    public void StopDrag()
    {
        Debug.Log("stop: " + gameObject);
        GridManager.instance.TurnOffAllHighlight();
        Destroy(gameObject);
    }

    public virtual void CheckCanMerge()
    {
        //override
    }
}
