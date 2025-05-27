using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipmentInfoPanel : MonoBehaviour
{
    [SerializeField] private GameObject _infoVisual;
    [SerializeField] private RectTransform _infoDetail;
    [SerializeField] private GameObject _equipButton;
    [SerializeField] private TextMeshProUGUI _equipmentIdText;
    [SerializeField] private TextMeshProUGUI _typeText;
    [Space(10)]
    [SerializeField] private int _equipmentId;
    public static EquipmentInfoPanel instance;
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
    public void SetID(int id)
    {
        _equipmentId = id;
    }
    public void Show(bool canEquip = true)
    {
        _infoVisual.SetActive(true);
        StartCoroutine(FeelingTools.ZoomInUI(_infoDetail, 0.5f, 1f, 0.25f, 1.1f));
        _equipButton.SetActive(canEquip);
        _equipmentIdText.text = $"ID: {_equipmentId.ToString()}";
        _typeText.text = $"Type: {SaveGame.SaveEquipment.GetEquipmentDataByID(_equipmentId)._type}";
    }
    public void ClosePanel()
    {
        _infoVisual.SetActive(false);
    }
    public void EquipClick()
    {
        _infoVisual.SetActive(false);
        for (int i = 0; i < ItemEquipmentController.instance.EquipedId.Count; i++)
        {
            if (SaveGame.SaveEquipment.GetEquipmentDataByID(ItemEquipmentController.instance.EquipedId[i])._type
                == SaveGame.SaveEquipment.GetEquipmentDataByID(_equipmentId)._type)
            {
                ItemEquipmentController.instance.ReplaceEquipment(_equipmentId, ItemEquipmentController.instance.EquipedId[i]);
            }
        }
    }
}
