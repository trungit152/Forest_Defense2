using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrapCardUI : CardUI, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] protected SetableObjects _trapDrag;
    public void OnPointerDown(PointerEventData eventData)
    {
        TurretManager.instance.ShowMergeArrow(_iD);
        VisualUp();
        //Set block = can set
        List<Tile.Type> types = new() { Tile.Type.Way };
        //show tile + instantiate turret drag
        GridManager.instance.ShowTile(true);
        GameStat.ChangeGameTimeScale(0.1f);
        Vector2 worldPos = GetMouseWorldPosition();
        _currentDrag = Instantiate(_trapDrag, worldPos + new Vector2(0, 1.5f), Quaternion.identity);
        _currentDrag.SetTurretCardUI(this);
    }

    public override string GetName()
    {
        return _name;
    }
}
