using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTurretController : MonoBehaviour
{
    [SerializeField] private Transform _listTurret;
    [SerializeField] private TurretItem _itemTurretPrefab;

    private List<RectTransform> _turretItems;

    public static ItemTurretController instance;
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
        _turretItems = new List<RectTransform>();
    }

    private void Start()
    {
        foreach (var item in SaveGame.SaveTurret.ItemTurretDataList)
        {
            var createdItem = Instantiate(_itemTurretPrefab, _listTurret);
            _turretItems.Add(createdItem.GetComponent<RectTransform>());
            createdItem.SetInfo(item.turretId);
        }
        ShowItemTurret();
        EquipedTurretBar.instance.UpdateEquipedTurretImage(SaveGame.SaveTurret.EquipedId);
    }

    public void ReplaceTurret(int inId, int outId)
    {
        SaveGame.SaveTurret.ReplaceTurretItem(inId, outId);
        EquipedTurretBar.instance.UpdateEquipedTurretImage(SaveGame.SaveTurret.EquipedId);
    }
    public void ShowItemTurret()
    {
        foreach (var item in _turretItems)
        {
            item.gameObject.SetActive(false);
        }
        StartCoroutine(FeelingTools.ZoomInList(_turretItems));
    }
}
