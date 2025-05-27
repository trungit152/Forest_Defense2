public class DuckCardUI : TurretCardUI
{
    private void Awake()
    {
        _name = "Duck";
    }
    public override string GetName()
    {
        _name = "Duck";
        return _name;
    }
}
