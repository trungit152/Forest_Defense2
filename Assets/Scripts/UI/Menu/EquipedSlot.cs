using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipedSlot : MonoBehaviour
{
    [SerializeField] private Type _type;
    [SerializeField] private Image _image;
    [SerializeField] private int _id;
    public Image Image { get => _image; }
    public enum Type
    {
        Turret,
        Equipment
    }
    public void UpdateTurret(int id)
    {
        if(_type != Type.Turret) return;
        _id = id;
        _image.sprite = ItemTurretImage.instance.GetItemTurretImageById(id);
        _image.SetNativeSize();
    }
    public void UpdateEquipment(int id)
    {
        if (_type != Type.Equipment) return;
        _id = id;
        _image.sprite = ItemEquipmentImage.instance.GetItemEquipmentImageById(id);

    }
    public void OnClick()
    {
        if(_type == Type.Turret)
        {
            TurretInfoPanel.instance.SetID(_id);
            TurretInfoPanel.instance.Show(false);
        }
        else if(_type == Type.Equipment)
        {
            EquipmentInfoPanel.instance.SetID(_id);
            EquipmentInfoPanel.instance.Show(false);
        }
    }
}
