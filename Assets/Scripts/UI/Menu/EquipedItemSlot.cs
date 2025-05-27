using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipedItemSlot : MonoBehaviour
{
    [SerializeField] private Type _type;
    [SerializeField] private Image _image;
    [SerializeField] private int _id;
    public Image Image { get => _image; }
    public enum Type
    {
        Body,
        Glass,
        Hair,
        Weapon
    }
    public void UpdateEquipment(int id)
    {
        _id = id;
        _image.sprite = ItemEquipmentImage.instance.GetItemEquipmentImageById(id);
    }
}
