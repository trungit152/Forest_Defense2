using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipedTurretBar : MonoBehaviour
{
    [SerializeField] private List<EquipedSlot> _slots;
    [SerializeField] private GameObject _equipedTurretPrefabs;

    private RectTransform _allSlot;

    public static EquipedTurretBar instance;
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
        _allSlot = GetComponent<RectTransform>();
    }
    public void UpdateEquipedTurretImage(List<int> listTurret)
    {
        for (int i = 0; i < 5; i++)
        {
            _slots[i].UpdateTurret(listTurret[i]);
        }
    }
    public void ShowEquipedTurret()
    {
        _allSlot.anchoredPosition += new Vector2(0, 75);
        StartCoroutine(FeelingTools.MoveToTarget(_allSlot, 0.5f, _allSlot.anchoredPosition - new Vector2(0, 75)));
    }
}
