using System;

[Serializable]
public class TurretInventStatus
{
    public Turrets turrets;
    public bool isUnlocked;
    public ID id;
    public enum ID
    {
        Badger,
        Croc,
        Duck,
        Fox,
        Penguin,
        Rabbit
    }
}
