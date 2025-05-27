using UnityEngine;
using UnityEngine.UI;

public class TurretItem : MonoBehaviour
{
    [SerializeField] private int _turretId;
    public int TurretId { get => _turretId; }
    [SerializeField] private Image _image;

    public void SetInfo(int id)
    {
        _turretId = id;
        _image.sprite = ItemTurretImage.instance.GetItemTurretImageById(id);
    }
    public void OnCLick()
    {
        TurretInfoPanel.instance.SetID(_turretId);
        TurretInfoPanel.instance.Show(!SaveGame.SaveTurret.GetTurretDataByID(_turretId).isEquiped);
    }
}