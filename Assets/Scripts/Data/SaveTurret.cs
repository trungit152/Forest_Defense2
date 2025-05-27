using System;
using System.Collections.Generic;

[Serializable]
public class ItemTurretData
{
    public int turretId;
    public bool isEquiped;
    public ItemTurretData(int id, bool isEquiped = false)
    {
        turretId = id;
        this.isEquiped = isEquiped;
    }
}
public class SaveTurret
{
    const string ITEM_TURRET = "ITEM_TURRET";
    const string EQUIPED_ID = "EQUIPED_ID";

    private List<ItemTurretData> _itemTurretDataList;
    private List<int> _equipedId;
    public List<int> EquipedId
    {
        get
        {
            if (_equipedId == null)
            {
                _equipedId = ES3.Load("EQUIPED_ID", new List<int>());
            }
            return _equipedId;
        }
    }
    public List<ItemTurretData> ItemTurretDataList
    {
        get
        {
            if (_itemTurretDataList == null)
            {
                _itemTurretDataList = ES3.Load(ITEM_TURRET, new List<ItemTurretData>());
            }
            return _itemTurretDataList;
        }
    }
    public SaveTurret()
    {
        _itemTurretDataList = ES3.Load(ITEM_TURRET, new List<ItemTurretData>());

        if (_itemTurretDataList == null || _itemTurretDataList.Count == 0)
        {
            _itemTurretDataList = new List<ItemTurretData>
        {
            new ItemTurretData(1, true),
            new ItemTurretData(2, true),
            new ItemTurretData(3, true),
            new ItemTurretData(4, true),
            new ItemTurretData(5, true),
            new ItemTurretData(6),
            new ItemTurretData(7),
            new ItemTurretData(8),
            new ItemTurretData(9),
            new ItemTurretData(10)
        };
            _equipedId = new List<int>
            {
                1,2,3,4,5
            };
            SaveData();
        }
    }

    public void SaveData()
    {
        ES3.Save(ITEM_TURRET, _itemTurretDataList);
        ES3.Save(EQUIPED_ID, _equipedId);
    }
    public void AddTurret(ItemTurretData turretData)
    {
        if (!_itemTurretDataList.Contains(turretData))
        {
            _itemTurretDataList.Add(turretData);
        }
        SaveData();
    }
    public void ReplaceTurretItem(int inId, int outId)
    {
        _equipedId[_equipedId.IndexOf(outId)] = inId;
        SaveData();
    }
    public ItemTurretData GetTurretDataByID(int id)
    {
        foreach (var t in ItemTurretDataList)
        {
            if (t.turretId == id)
            {
                return t;
            }
        }
        return null;
    }
}
