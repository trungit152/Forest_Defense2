using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEquipmentController : MonoBehaviour
{
    [SerializeField] private Transform _listEquipment;
    [SerializeField] private EquipmentItem _itemEquipmentPrefab;

    private List<int> _equipedId = new List<int>();
    public List<int> EquipedId { get => _equipedId; }
    private List<RectTransform> _equipmentItems;
    public static ItemEquipmentController instance;
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
        _equipmentItems = new List<RectTransform>();
    }

    private void Start()
    {
        foreach (var item in SaveGame.SaveEquipment.ItemEquipmentDataList)
        {
            if (item.equipedIndex != -1)
            {
                _equipedId.Add(item.equipmentId);
            }
            var createdItem = Instantiate(_itemEquipmentPrefab, _listEquipment);
            _equipmentItems.Add(createdItem.GetComponent<RectTransform>());
            createdItem.SetInfo(item.equipmentId);
        }
        ShowItemEquipment();
        EquipedEquipment.instance.UpdateEquipedEquipmentImage(_equipedId);
    }
    public void ReplaceEquipment(int inId, int outId)
    {
        SaveGame.SaveEquipment.ReplaceEquipment(inId, outId);
        _equipedId[_equipedId.IndexOf(outId)] = inId;        
        EquipedEquipment.instance.UpdateEquipedEquipmentImage(_equipedId);
    }
    public void ShowItemEquipment()
    {
        foreach (var item in _equipmentItems)
        {
            item.gameObject.SetActive(false);
        }
        StartCoroutine(FeelingTools.ZoomInList(_equipmentItems));
    }
}
