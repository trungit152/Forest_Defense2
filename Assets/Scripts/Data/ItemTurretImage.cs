using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTurretImage : MonoBehaviour
{
    public Sprite _rabbit;
    public Sprite _croc;
    public Sprite _fox;
    public Sprite _penguin;
    public Sprite _duck;
    public Sprite _thunderBird;
    public Sprite _badger;
    public Sprite _spike;
    public Sprite _fire;
    public Sprite _swamp;
    public Sprite _unknow;

    public static ItemTurretImage instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    public Sprite GetItemTurretImageById(int id)
    {
        Sprite res = null;
        switch (id)
        {
            case IDs.TURRET_BOW:
                res = _rabbit;
                break;
            case IDs.TURRET_CANON:
                res = _croc; 
                break;
            case IDs.TURRET_FIRE:
                res = _fox;
                break;
            case IDs.TURRET_ICE:
                res = _penguin;
                break;
            case IDs.TURRET_WATER:
                res = _duck;
                break;
            case IDs.TURRET_THUNDER:
                res = _thunderBird;
                break;
            case IDs.TURRET_BALISTA:
                res = _badger;
                break;
            case IDs.TRAP_SPIKE:
                res = _spike;
                break;
            case IDs.TRAP_FIRE:
                res = _fire;
                break;
            case IDs.TRAP_SWAMP:
                res = _swamp;
                break;

            default:
                res = _unknow;
                break;

        }
        return res;
    }
}
