using UnityEngine;
using UnityEngine.UI;

public class EquipmentItem : MonoBehaviour
{
    [SerializeField] private int _equipmentId;
    public int EquipmentId { get => _equipmentId; }
    [SerializeField] private Image _image;
    public void SetInfo(int id)
    {
        _equipmentId = id;
        _image.sprite = ItemEquipmentImage.instance.GetItemEquipmentImageById(id);
    }
    public void OnClick()
    {
        EquipmentInfoPanel.instance.SetID(_equipmentId);
        Debug.Log(SaveGame.SaveEquipment.GetEquipmentDataByID(_equipmentId).equipedIndex);
        EquipmentInfoPanel.instance.Show(SaveGame.SaveEquipment.GetEquipmentDataByID(_equipmentId).equipedIndex == -1);
    }
}
