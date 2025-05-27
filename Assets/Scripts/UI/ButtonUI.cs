using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour
{
    [Header("PLAY")]
    [SerializeField] private RectTransform _playPanel;
    [Space(10)]
    [Header("INVENTORY")]
    [SerializeField] private RectTransform _inventoryPanel;
    [SerializeField] private ScrollRect _wareHouseScroll;
    [SerializeField] private RectTransform _turretList;
    [SerializeField] private RectTransform _itemList;

    private int _status;
    private int _inventoryStatus = 0;
    public void PauseGame()
    {
        GameStat.ChangeGameTimeScale(0);
        EnemiesAnimator.instance.ChangeAnimatorSpeed(GameStat.gameTimeScale);
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void LoadLevel5()
    {
        SceneManager.LoadScene("Level5");
    }
    public void ShowTurretClick()
    {
        _itemList.gameObject.SetActive(false);
        if (!_turretList.gameObject.activeSelf)
        {
            _turretList.gameObject.SetActive(true);
            ItemTurretController.instance.ShowItemTurret();
        }
        _wareHouseScroll.content = _turretList;
        _inventoryStatus = 1;
    }
    public void ShowItemClick()
    {
        _turretList.gameObject.SetActive(false);
        if(!_itemList.gameObject.activeSelf)
        {
            _itemList.gameObject.SetActive(true);
            ItemEquipmentController.instance.ShowItemEquipment();
        }
        _wareHouseScroll.content = _itemList;
        _inventoryStatus = 0;
    }
    public void ShowInventory()
    {
        if (_inventoryPanel.gameObject.activeSelf) return;
        StartCoroutine(FeelingTools.ShowPanelWithBounceEffect(_inventoryPanel));
        if (_status != 1)
        {
            if(_inventoryStatus == 0)
            {
                ItemEquipmentController.instance.ShowItemEquipment();
                EquipedTurretBar.instance.ShowEquipedTurret();
                EquipedEquipment.instance.ShowEquipedEquipment();
            }
            else if(_inventoryStatus == 1)
            {
                ItemTurretController.instance.ShowItemTurret();
                EquipedEquipment.instance.ShowEquipedEquipment();
                EquipedTurretBar.instance.ShowEquipedTurret();
            }
            _status = 1;
        }
        _playPanel.gameObject.SetActive(false);
    }
    public void ShowPlay()
    {
        if (_playPanel.gameObject.activeSelf) return;
        _inventoryPanel.gameObject.SetActive(false);
        StartCoroutine(FeelingTools.ShowPanelWithBounceEffect(_playPanel));
        if(_status != 2)
        {
            _status = 2;
        }
    }
    public void NextLevel()
    {
        if (SaveGame.SaveGameLevel.currentLevel >= SaveGame.SaveGameLevel.levelUnlocked) return;
        SaveGame.SaveGameLevel.currentLevel++;
        SaveGame.SaveGameLevel.SaveGameLevels();
        _currentLevelText.text = SaveGame.SaveGameLevel.currentLevel.ToString();
    }
    public void PreviousLevel()
    {
        if (SaveGame.SaveGameLevel.currentLevel<= 1) return;
        SaveGame.SaveGameLevel.currentLevel--;
        SaveGame.SaveGameLevel.SaveGameLevels();
        _currentLevelText.text = SaveGame.SaveGameLevel.currentLevel.ToString();
    }

    [SerializeField] private TextMeshProUGUI _currentLevelText;
    private void Start()
    {
        if(_currentLevelText != null)
        {
            _currentLevelText.text = SaveGame.SaveGameLevel.currentLevel.ToString();
        }
    }

}
