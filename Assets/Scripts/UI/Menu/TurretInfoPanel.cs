using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretInfoPanel : MonoBehaviour
{
    [SerializeField] private GameObject _infoVisual;
    [SerializeField] private RectTransform _infoDetail;
    [SerializeField] private GameObject _equipButton;
    [SerializeField] private TextMeshProUGUI _turretName;
    [SerializeField] private TextMeshProUGUI _turretSize;
    [Space(10)]
    [SerializeField] private GameObject _replaceVisual;
    [SerializeField] private List<ReplaceTurretButton> _replaceTurretButton;
    [Space(10)]
    [SerializeField] private int _turretId;

    public static TurretInfoPanel instance;
    private void Awake()
    {
        if(instance != null && instance != this)
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
        _turretId = id;
    }
    public void Show(bool canEquip = true)
    {
        _infoVisual.SetActive(true);
        StartCoroutine(FeelingTools.ZoomInUI(_infoDetail, 0.5f, 1f, 0.25f, 1.1f));
        _equipButton.SetActive(canEquip);
        _turretName.text = $"Name: {DataTurret.GetData(_turretId).Name}";
        _turretSize.text = $"Size: {DataTurret.GetData(_turretId).Size}";
    }
    public void ClosePanel()
    {
        _infoVisual.SetActive(false); 
        _replaceVisual.SetActive(false);
    }
    public void EquipClick()
    {
        _infoVisual.SetActive(false);
        _replaceVisual.SetActive(true);
        for (int i = 0; i < _replaceTurretButton.Count; i++)
        {
            _replaceTurretButton[i].SetInfo(SaveGame.SaveTurret.EquipedId[i]);
        }
    }
    public void ReplaceClick(int id)
    {
        SaveGame.SaveTurret.GetTurretDataByID(_turretId).isEquiped = true;
        SaveGame.SaveTurret.GetTurretDataByID(id).isEquiped = false;
        _replaceVisual.SetActive(false);
        ItemTurretController.instance.ReplaceTurret(_turretId, id);
        SaveGame.SaveTurret.SaveData();
    }
}
