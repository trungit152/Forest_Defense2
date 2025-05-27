public class FoxCardUI : TurretCardUI
{
    private void Awake()
    {
        _name = "Fox";
    }
    public override string GetName()
    {
        _name = "Fox";
        return _name;
    }
}
