public class PenguinCardUI : TurretCardUI
{
    private void Awake()
    {
        _name = "Penguin";
    }
    public override string GetName()
    {
        _name = "Penguin";
        return _name;
    }
}
