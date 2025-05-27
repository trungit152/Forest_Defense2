using UnityEngine;
public class SetAbleObject4x4 : SetableObjects
{
    protected override void InitPivot()
    {
        _listCount = 16;
        CreatePoint();
    }
    protected override void CreatePoint()
    {
        for (int i = -2; i <= 2; i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                if (i != 0 && j != 0)
                {
                    Vector3 pivot = new Vector3(transform.position.x + 0.55f * i + (i < 0 ? 0.275f : -0.275f),
                        transform.position.y + 0.55f * j + (j < 0 ? 0.275f : -0.275f), transform.position.z);
                    _pivots.Add(Instantiate(_pivotPref, pivot, Quaternion.identity, transform).transform);
                }
            }
        }
    }
    protected override void OnMouseUp()
    {
        Debug.Log("MouseUp");
        base.OnMouseUp();
        SetOnPosition();
    }
    public override void SetOnPosition()
    {
        if (_highlightTiles.Count == _listCount)
        {
            float sumX = 0, sumY = 0;
            foreach (var tile in _highlightTiles)
            {
                Destroy(gameObject);
                foreach (var tile2 in _highlightTiles)
                {
                    tile2.Highlight(false);
                }
                _isSet = false;
                return;
            }
            foreach (var tile in _highlightTiles)
            {
                sumX += tile.transform.position.x;
                sumY += tile.transform.position.y;
                tile.SetType(Tile.Type.Turret);
                tile.Highlight(false);
            }
            transform.position = new Vector3(sumX / _listCount, sumY / _listCount);
            if (_canSet)
            {
                Transfer();
                _isSet = true;
            }
        }
    }
}
