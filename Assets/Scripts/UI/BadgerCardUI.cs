public class BadgerCardUI : TurretCardUI
{
    private void Awake()
    {
        _name = "Badger";
    }
    public override string GetName()
    {
        _name = "Badger";
        return _name;
    }
}
