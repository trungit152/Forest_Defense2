public class CrocCardUI : TurretCardUI
{
    private void Awake()
    {
        _name = "Croc";
    }
    public override string GetName()
    {
        _name = "Croc";
        return _name;
    }
}
