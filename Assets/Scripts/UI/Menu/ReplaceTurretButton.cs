using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplaceTurretButton : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private Image _image;

    public void SetInfo(int id)
    {
        _id = id;
        _image.sprite = ItemTurretImage.instance.GetItemTurretImageById(id);
    }

    public void OnClick()
    {
        TurretInfoPanel.instance.ReplaceClick(_id);
    }
}
