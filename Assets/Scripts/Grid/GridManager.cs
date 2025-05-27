using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private float tileScale = 0.55f;
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private Tile _tilePref;
    private Dictionary<Vector2, Tile> _tiles;
    private Vector2 _offset;
    public Vector2 _halfTile;
    public static GridManager instance;

    private float _botX;
    private float _topX;
    private float _leftY;
    private float _rightY;

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
    }
    private void Start()
    {
        //set target frame rate
        Application.targetFrameRate = 60;
        if(MapSetData.instance!=null) Instantiate(MapSetData.instance.GetMap(SaveGame.SaveGameLevel.currentLevel));
        AstarPathControl.instance.ScanAll();
        GenerateGrid();
        _halfTile = new Vector2(tileScale / 2, tileScale / 2);
    }
    private void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                float xPos = i * tileScale;
                float yPos = j * tileScale;
                var tileGenerated = Instantiate(_tilePref, new Vector3(MyMath.RoundToDecimals(xPos, 2), MyMath.RoundToDecimals(yPos, 2)), Quaternion.identity, this.transform);
                tileGenerated.name = $"Tile {i} {j}";
                tileGenerated.SetPosition(xPos, yPos);
                _tiles[new Vector2(i, j)] = tileGenerated;
            }
        }
        MoveGridToCenter();
        InitTile();
        ShowTile(false);
    }
    private void InitTile()
    {
        foreach (var tile in _tiles.Values)
        {
            tile.Init();
        }
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        else return null;
    }
    public Tile GetTileInRange(Vector2 pos)
    {
        Vector2 _v = GetGridIndex(pos);
        foreach (var tile in _tiles.Values)
        {
            if (tile.GetPositionInList() == _v) return tile;
        }
        return null;
    }

    private void MoveGridToCenter()
    {
        float width = _width * 0.55f;
        float height = _height * 0.55f;

        _botX = -width / 2;
        _topX = _botX + width + 0.55f * 4;
        _leftY = -height / 2 + 0.55f * 2;
        _rightY = _leftY + height - +0.55f * 4;

        _offset = new Vector2(-width / 2 + 0.55f / 2, -height / 2 + 0.275f) + new Vector2(0, 0.55f * 2);
        transform.position = _offset;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log(GetTileAtPosition(Vector2.zero));
        }
    }
    public Vector2Int GetGridIndex(Vector2 pos, float tileSize = 0.55f)
    {
        int xIndex = Mathf.FloorToInt((pos.x - _offset.x - _halfTile.x) / tileSize);
        int yIndex = Mathf.FloorToInt((pos.y - _offset.y - _halfTile.y) / tileSize);

        return new Vector2Int(xIndex + 1, yIndex + 1);
    }
    public Vector2 GetOffset()
    {
        return _offset;
    }

    public void ShowTile(bool isShown = true)
    {
        foreach (var tile in _tiles.Values)
        {
            tile.Show(isShown);
        }
    }
    public float GetBotPos()
    {
        return _botX;
    }
    public float GetTopPos()
    {
        return _topX;
    }
    public float GetRightPos()
    {
        return _rightY;
    }
    public float GetLeftPos()
    {
        return _leftY;
    }
    public void TurnOffAllHighlight(bool b = false)
    {
        foreach (var tile in _tiles.Values)
        {
            tile.Highlight(b);
            if(b == false)
            {
                tile.RemoveTempBlock();
            }
        }
    }
    
}
