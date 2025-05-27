public class DragObject4x4 : SetAbleObject4x4
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
    }

    public override void SetTurretCardUI(CardUI cardUI)
    {
        _cardUI = cardUI;
    }
}
