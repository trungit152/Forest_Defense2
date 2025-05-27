public class RabbitCardUI : TurretCardUI
{
    private void Awake()
    {
        _name = "Rabbit";
    }
    public override string GetName()
    {
        _name = "Rabbit";
        return _name;
    }
}
