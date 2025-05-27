using UnityEngine;
using UnityEngine.EventSystems;

public class TurretCardUI : CardUI, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] protected SetableObjects _turretDrag;
    public void OnPointerDown(PointerEventData eventData)
    {
        TurretManager.instance.ShowMergeArrow(_iD);
        //show tile + instantiate turret drag
        VisualUp();
        GridManager.instance.ShowTile(true);
        GameStat.ChangeGameTimeScale(0.1f);
        Vector2 worldPos = GetMouseWorldPosition();
        _currentDrag = Instantiate(_turretDrag, worldPos + new Vector2(0, 1.5f), Quaternion.identity);
        _currentDrag.Init();
        _currentDrag.ShowAttackRange(true);
        _currentDrag.SetTurretCardUI(this);
    }
    public override string GetName()
    {
        return _name;
    }
}
