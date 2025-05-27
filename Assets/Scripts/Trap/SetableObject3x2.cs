using UnityEngine;
public class SetableObject3x2 : SetableObjects
{
    protected override void InitPivot()
    {
        _listCount = 6;
        CreatePoint();
    }
    protected override void CreatePoint()
    {
        _pivots.Add(Instantiate(_pivotPref, new Vector3(transform.position.x + 1f, transform.position.y + 0.5f, transform.position.y),
            Quaternion.identity, transform).transform);
        _pivots.Add(Instantiate(_pivotPref, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.y),
            Quaternion.identity, transform).transform);
        _pivots.Add(Instantiate(_pivotPref, new Vector3(transform.position.x - 1f, transform.position.y + 0.5f, transform.position.y),
            Quaternion.identity, transform).transform);

        _pivots.Add(Instantiate(_pivotPref, new Vector3(transform.position.x + 1f, transform.position.y - 0.5f, transform.position.y),
            Quaternion.identity, transform).transform);
        _pivots.Add(Instantiate(_pivotPref, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.y),
            Quaternion.identity, transform).transform);
        _pivots.Add(Instantiate(_pivotPref, new Vector3(transform.position.x - 1f, transform.position.y - 0.5f, transform.position.y),
            Quaternion.identity, transform).transform);
    }
}
