using System;
using System.Collections.Generic;

[Serializable]
public class ItemData
{
    public int equipmentId;
    public int equipedIndex;
    public ItemType _type;
    public enum ItemType
    {
        Body,
        Glass,
        Head,
        Weapon
    }
    public ItemData(int id, ItemType type, int equipedIndex = -1) 
    {
        equipmentId = id;
        _type = type;
        this.equipedIndex = equipedIndex;
    }
}
public class SaveEquipment
{
    const string ITEM_EQUIPMENT = "ITEM_EQUIPMENT";

    private List<ItemData> _itemEquipmentDataList;

    public List<ItemData> ItemEquipmentDataList
    {
        get
        {
            if (_itemEquipmentDataList == null)
            {
                _itemEquipmentDataList = ES3.Load(ITEM_EQUIPMENT, new List<ItemData>());
            }
            return _itemEquipmentDataList;
        }
    }
    public SaveEquipment()
    {
        _itemEquipmentDataList = ES3.Load(ITEM_EQUIPMENT, new List<ItemData>());
        if (_itemEquipmentDataList == null || _itemEquipmentDataList.Count == 0)
        {
            _itemEquipmentDataList = new List<ItemData>
            {
                new ItemData(1,ItemData.ItemType.Body,1),
                new ItemData(2,ItemData.ItemType.Glass,2),
                new ItemData(3,ItemData.ItemType.Head,3),
                new ItemData(4,ItemData.ItemType.Weapon, 4),
                new ItemData(5,ItemData.ItemType.Body),
                new ItemData(6,ItemData.ItemType.Glass)
            };
            SaveData();
        }
    }

    public void SaveData()
    {
        ES3.Save(ITEM_EQUIPMENT, _itemEquipmentDataList);
    }

    public void AddEquipment(ItemData itemData)
    {
        if (!_itemEquipmentDataList.Contains(itemData))
        {
            _itemEquipmentDataList.Add(itemData);
        }
        SaveData();
    }
    public void ReplaceEquipment(int inId, int outId)
    {
        if (GetEquipmentDataByID(inId)._type != GetEquipmentDataByID(outId)._type) return;
        GetEquipmentDataByID(inId).equipedIndex = GetEquipmentDataByID(outId).equipedIndex;
        GetEquipmentDataByID(outId).equipedIndex = -1;
        SaveData();
    }
    public ItemData GetEquipmentDataByID(int id)
    {
        foreach (var item in _itemEquipmentDataList)
        {
            if (item.equipmentId == id)
            {
                return item;
            }
        }
        return null;
    }
    public ItemData GetEquipmentDataByEquippedID(int id)
    {
        foreach (var item in _itemEquipmentDataList)
        {
            if (item.equipedIndex == id)
            {
                return item;
            }
        }
        return null;
    }
}
