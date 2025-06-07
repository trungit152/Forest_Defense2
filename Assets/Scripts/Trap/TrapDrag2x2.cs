using UnityEngine;

public class TrapDrag2x2 : DragObject
{
    [SerializeField] private int _trapId;
    protected override void CheckCanSet()
    {
        foreach (var tile in _highlightTiles)
        {
            if (tile != null)
            {
                if (tile.GetTileType() != Tile.Type.Way) 
                { 
                    _canSet = false; 
                    break; 
                }
                _canSet = true;
            }
        }
    }
    protected override void Transfer()
    {
        AudioManager.instance.PlaySoundEffect("SetTurret");
        var trap = Instantiate(_trapsObject, _allTurrets);
        trap.transform.position = transform.position;
        TurretManager.instance.AddTurret(_trapId);
        TurretManager.instance.AddTrapObject(trap);
        Destroy(gameObject);
    }
    public override void SetOnPosition()
    {
        base.SetOnPosition();
        if (_isSet)
        {
            foreach (var tile in _highlightTiles)
            {
                tile.SetType(Tile.Type.Trap);
            }
        }
    }
}
