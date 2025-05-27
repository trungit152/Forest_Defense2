using UnityEngine;

public class SetableObject1x1 : SetableObjects
{
    protected override void InitPivot()
    {
        _listCount = 1;
        CreatePoint();
    }
    protected override void CreatePoint()
    {
            _pivots.Add(Instantiate(_pivotPref, new Vector3(transform.position.x, transform.position.y, transform.position.z),
                Quaternion.identity, transform).transform);
    }
    protected override void OnMouseUp()
    {
        base.OnMouseUp();
        if (_highlightTiles.Count == 1)
        {
            transform.position = _highlightTiles[0].transform.position;
        }
    }
}
