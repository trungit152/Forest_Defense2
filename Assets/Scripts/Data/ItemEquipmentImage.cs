using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEquipmentImage : MonoBehaviour
{
    public Sprite _armor1;
    public Sprite _armor2;
    public Sprite _pant1;
    public Sprite _pant2;
    public Sprite _boot1;
    public Sprite _ring1;
    public Sprite _unknow;

    public static ItemEquipmentImage instance;

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
    public Sprite GetItemEquipmentImageById(int id)
    {
        Sprite res = null;
        switch (id)
        {
            case 1:
                res = _armor1;
                break;
            case 2:
                res = _pant1;
                break;
            case 3:
                res = _boot1;
                break;
            case 4:
                res = _ring1;
                break;
            case 5:
                res = _armor2;
                break;
            case 6:
                res = _pant2;
                break;
            default:
                res = _unknow;
                break;
        }
        return res;
    }
}
