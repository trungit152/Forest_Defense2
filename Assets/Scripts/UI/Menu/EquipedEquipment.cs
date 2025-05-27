using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipedEquipment : MonoBehaviour
{
    [SerializeField] private RectTransform _leftSlot;
    [SerializeField] private RectTransform _rightSlot;

    [SerializeField] private List<EquipedSlot> _slots;
    [SerializeField] private GameObject _equipedEquipmentPrefabs;
    public static EquipedEquipment instance;
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
    public void UpdateEquipedEquipmentImage(List<int> listEquipment)
    {
        for (int i = 0; i < 4; i++)
        {
            _slots[i].UpdateEquipment(listEquipment[i]);
        }
    }

    public void ShowEquipedEquipment()
    {
        _leftSlot.anchoredPosition -= new Vector2(200, 0);
        _rightSlot.anchoredPosition += new Vector2(200, 0);
        StartCoroutine(FeelingTools.MoveToTarget(_leftSlot, 0.5f, _leftSlot.anchoredPosition + new Vector2(200, 0)));
        StartCoroutine(FeelingTools.MoveToTarget(_rightSlot, 0.5f, _rightSlot.anchoredPosition - new Vector2(200, 0)));
    }
}
