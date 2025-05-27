public class ThunderBirdCardUI : TurretCardUI
{
    private void Awake()
    {
        _name = "ThunderBird";
    }
    public override string GetName()
    {
        _name = "ThunderBird";
        return _name;
    }
}
