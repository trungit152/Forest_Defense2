using Unity.VisualScripting;
using UnityEngine;

public class TurretDrag2x2 : DragObject
{
    [SerializeField] private int _turretId;
    protected override void CheckCanSet()
    {
        if (IsBetweenWayAndBlock() || !AstarPathControl.instance._canSet)
        {
            _canSet = false;
            return;
        }
        if (_keyPivot != null)
        {
            _canMerge = true;
            Tile curTile = GridManager.instance.GetTileInRange(_keyPivot.position);
            if (curTile != null)
            {
                if (curTile.GetTileType() == Tile.Type.Pivot || curTile.GetTileType() == Tile.Type.Way)
                {
                    _canSet = true;
                }
                else
                {
                    _canSet = false;
                }
            }
            foreach (var tile in _highlightTiles)
            {
                if (tile.GetTileType() == Tile.Type.Turret)
                {
                    _canSet = false;
                    if(tile.Turret.IsMaxLevel() || _turretObject.ID() != tile.Turret.ID())
                    {
                        _canMerge = false;
                        return;
                    }
                }
                else
                {
                    _canMerge = false;
                }
            }
        }
    }
    protected override void Transfer()
    {
        var turret = Instantiate(_turretObject, _allTurrets);
        turret.transform.position = transform.position;
        turret.SetUnderBlock(_isShownUnderBlock);
        TurretManager.instance.AddTurret(_turretId);
        TurretManager.instance.AddTurretObject(turret);
        foreach (var tile in _highlightTiles)
        {
            tile.SetTurret(turret);
        }
        Destroy(gameObject);
    }
    protected override void Merge()
    {
        if(_keyPivot != null)
        {
            var tile = GridManager.instance.GetTileInRange(_keyPivot.position);
            if(tile != null)
            {
                tile.Turret.Merge();
            }
        }
    }
    public override void CheckCanMerge()
    {

    }
    public override void SetOnPosition()
    {
        ShowAttackRange(false);
        base.SetOnPosition();
        Scan();
        EnemiesController.instance.FindPath();
        if (_isSet)
        {
            foreach (var tile in _highlightTiles)
            {
                tile.SetType(Tile.Type.Turret);
            }
        }
    }
}
