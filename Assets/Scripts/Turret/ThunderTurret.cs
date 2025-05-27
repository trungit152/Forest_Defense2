public class ThunderTurret : Turrets
{
    private ShootObjects _shootObject;
    public ShootObjects ShootObjects
    {
        get
        {
            if (_shootObject == null)
            {
                _shootObject = GetComponent<ShootObjects>();
            }
            return _shootObject;
        }
        set
        {
            _shootObject = value;
        }
    }
    private void Awake()
    {
        SetBoneSetting();
    }
    public override void Shoot()
    {
        ChangeVisual();
        ShootObjects.ShootThunder(_spawnPos != null ? _spawnPos.position : transform.position, _range, _spawnPos, _bounceTime);
    }
}
