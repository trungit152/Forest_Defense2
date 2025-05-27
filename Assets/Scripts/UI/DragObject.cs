
public class DragObject : SetableObject2x2
{
    private CardUI _cardUI;
    public override void SetOnPosition()
    {
        base.SetOnPosition();
        if (_isSet)
        {
            Destroy(_cardUI.gameObject);
            DeskController.instance.RemoveCard(_cardUI);
        }
        GameStat.ChangeGameTimeScale(1);
    }

    public override void SetTurretCardUI(CardUI cardUI)
    {
        _cardUI = cardUI;
    }
}
