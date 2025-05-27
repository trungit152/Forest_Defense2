using UnityEngine;

public class SetableObject2x2 : SetableObjects
{
    protected override void InitPivot()
    {
        _listCount = 4;
        _listCountConst = 4;
        CreatePoint();
    }
    protected override void CreatePoint()
    {
        if (_initPivotPoint == null) _initPivotPoint = transform;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i != 0 && j != 0)
                {
                    var pv = Instantiate(_pivotPref, new Vector3(_initPivotPoint.position.x + 0.275f * i, _initPivotPoint.position.y + 0.275f * j, _initPivotPoint.position.z),
                        Quaternion.identity, transform).transform;
                    _pivots.Add(pv);
                    if (i == -1 && j == -1)
                    {
                        _keyPivot = pv;
                    }
                }
            }
        }
    }
    protected override void OnMouseUp()
    {
        base.OnMouseUp();
        SetOnPosition();
    }
    public override void SetOnPosition()
    {
        if(_keyPivot != null)
        {
            var tile = GridManager.instance.GetTileInRange(_keyPivot.position);
            if (tile != null)
            {
                if (tile.GetTileType() == Tile.Type.Way)
                {
                    _isShownUnderBlock = true;
                }
                else
                {
                    _isShownUnderBlock = false;
                }
            }
        }
        GameStat.ChangeGameTimeScale(1);
        if (!IsOnGrid())
        {
            foreach (var tile2 in _highlightTiles)
            {
                tile2.Highlight(false);
            }
            _isSet = false;
            Destroy(gameObject);
            return;
        }
        if (_highlightTiles.Count == 4)
        {
            float sumX = 0, sumY = 0;
            foreach (var tile in _highlightTiles)
            {
                foreach (var tile2 in _highlightTiles)
                {
                    tile2.Highlight(false);
                }
                _isSet = false;
                Destroy(gameObject);
            }
            foreach (var tile in _highlightTiles)
            {
                sumX += tile.transform.position.x;
                sumY += tile.transform.position.y;
                tile.Highlight(false);
            }
            transform.position = new Vector3(sumX / 4, sumY / 4);
            if (_canSet)
            {
                Transfer();
                _isSet = true;
            }
            else if (_canMerge)
            {
                Merge();
                _isSet = true;
            }
        }
    }
    public bool IsBetweenWayAndBlock()
    {
        bool hasWay = false;
        bool hasBlock = false;

        foreach (var pivot in _pivots)
        {
            Tile curTile = GridManager.instance.GetTileInRange(pivot.position);
            if (curTile != null)
            {
                var type = curTile.GetTileType();
                if (type == Tile.Type.Way)
                    hasWay = true;
                else if (type == Tile.Type.Block || type == Tile.Type.Pivot)
                    hasBlock = true;
            }

            if (hasWay && hasBlock)
                return true;
        }

        return false;
    }

}
