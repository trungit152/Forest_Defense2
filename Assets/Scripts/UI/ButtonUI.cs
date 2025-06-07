using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum UIWindowType
{
    PLAY,
    INVENTORY,
    TALENT
}

public class ButtonUI : MonoBehaviour
{
    [Header("HEADER")]
    [SerializeField] private TextMeshProUGUI _talentPointText;
    [SerializeField] private TextMeshProUGUI _coinText;
    [Header("PLAY")]
    [SerializeField] private RectTransform _playPanel;
    [SerializeField] private Image _questFill;
    [SerializeField] private TextMeshProUGUI _questText;
    [Header("INVENTORY")]
    [SerializeField] private RectTransform _inventoryPanel;
    [SerializeField] private ScrollRect _wareHouseScroll;
    [SerializeField] private RectTransform _turretList;
    [SerializeField] private RectTransform _itemList;

    [Header("TALENT")]
    [SerializeField] private RectTransform _talentPanel;
    [SerializeField] private TextMeshProUGUI _currentLevelText;

    [Header("SETTINGS")]
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Image _settingBlur;
    [SerializeField] private RectTransform _settingItem;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _soundEffectVolumeSlider;
    [SerializeField] private GameObject _musicOnIcon;
    [SerializeField] private GameObject _musicOffIcon;
    [SerializeField] private GameObject _sfxOnIcon;
    [SerializeField] private GameObject _sfxOffIcon;

    private UIWindowType? _currentWindow = null; // Nullable type to track open window
    private int _inventoryStatus = 0; // 0 = Items, 1 = Turrets

    public static ButtonUI instance;
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
        AudioManager.instance.PlayMusic("MenuMusic");
    }
    private void Start()
    {
        if (_currentLevelText != null)
        {
            _currentLevelText.text = SaveGame.SaveGameLevel.currentLevel.ToString();
        }
        //
        _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChange);
        _soundEffectVolumeSlider.onValueChanged.AddListener(OnSoundEffectVolumeChange);
        _musicVolumeSlider.value = SaveGame.SaveSettings.unmuteMusicVolume;
        _soundEffectVolumeSlider.value = SaveGame.SaveSettings.sfxVolume;
        AudioManager.instance.SetVolumeMusic();
        AudioManager.instance.SetVolumeEffect();
        if (SaveGame.SaveSettings.musicMute)
        {
            _musicOffIcon.SetActive(true);
            _musicOnIcon.SetActive(false);
        }
        else
        {
            _musicOffIcon.SetActive(false);
            _musicOnIcon.SetActive(true);
        }
        if (SaveGame.SaveSettings.sfxMute)
        {
            _sfxOffIcon.SetActive(true);
            _sfxOnIcon.SetActive(false);
        }
        else
        {
            _sfxOffIcon.SetActive(false);
            _sfxOnIcon.SetActive(true);
        }
        //
        UpdateHeaderText();
        ShowPlay();
    }

    public void UpdateHeaderText()
    {
        if (_talentPointText != null)
        {
            _talentPointText.text = SaveGame.SaveTalentPoint.TalentPoint.ToString();
        }
        if(_coinText != null)
        {
            _coinText.text = SaveGame.SaveTalentPoint.Coin.ToString();
        }
    }

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
        GlobalController.CurrentModeGame = GlobalController.ModeGame.ModeOffline;
        AudioManager.instance.FadeOutMusic("MenuMusic");
        if (SaveGame.SaveSettings.newbieTutorial)
        {
            SceneManager.LoadScene("Tutorial");
            SaveGame.SaveSettings.newbieTutorial = false;
            SaveGame.SaveSettings.Save();
        }
        else
        {
            SceneManager.LoadScene("Level5");

        }
    }

    public void ShowPlay()
    {
        AudioManager.instance.PlaySoundEffect("UIClick");
        if (_currentWindow != UIWindowType.PLAY)
        {
            OpenWindow(UIWindowType.PLAY);
            StartCoroutine(FeelingTools.ShowPanelWithBounceEffect(_playPanel));
        }
    }

    public void ShowInventory()
    {
        AudioManager.instance.PlaySoundEffect("UIClick");
        if (_currentWindow != UIWindowType.INVENTORY)
        {
            OpenWindow(UIWindowType.INVENTORY);
            StartCoroutine(FeelingTools.ShowPanelWithBounceEffect(_inventoryPanel));
            if (_inventoryStatus == 0)
            {
                ItemEquipmentController.instance.ShowItemEquipment();
                EquipedTurretBar.instance.ShowEquipedTurret();
                EquipedEquipment.instance.ShowEquipedEquipment();
            }
            else if (_inventoryStatus == 1)
            {
                ItemTurretController.instance.ShowItemTurret();
                EquipedEquipment.instance.ShowEquipedEquipment();
                EquipedTurretBar.instance.ShowEquipedTurret();
            }
        }
    }

    public void ShowTalent()
    {
        AudioManager.instance.PlaySoundEffect("UIClick");
        if (_currentWindow != UIWindowType.TALENT)
        {
            OpenWindow(UIWindowType.TALENT);
            StartCoroutine(FeelingTools.ShowPanelWithBounceEffect(_talentPanel));
        }
    }

    public void NextLevel()
    {
        AudioManager.instance.PlaySoundEffect("UIClick");
        if (SaveGame.SaveGameLevel.currentLevel >= SaveGame.SaveGameLevel.levelUnlocked) return;
        SaveGame.SaveGameLevel.currentLevel++;
        SaveGame.SaveGameLevel.SaveGameLevels();
        _currentLevelText.text = SaveGame.SaveGameLevel.currentLevel.ToString();
    }

    public void PreviousLevel()
    {
        AudioManager.instance.PlaySoundEffect("UIClick");
        if (SaveGame.SaveGameLevel.currentLevel <= 1) return;
        SaveGame.SaveGameLevel.currentLevel--;
        SaveGame.SaveGameLevel.SaveGameLevels();
        _currentLevelText.text = SaveGame.SaveGameLevel.currentLevel.ToString();
    }

    public void ShowTurretClick()
    {
        AudioManager.instance.PlaySoundEffect("UIClick");
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
        AudioManager.instance.PlaySoundEffect("UIClick");
        _turretList.gameObject.SetActive(false);
        if (!_itemList.gameObject.activeSelf)
        {
            _itemList.gameObject.SetActive(true);
            ItemEquipmentController.instance.ShowItemEquipment();
        }
        _wareHouseScroll.content = _itemList;
        _inventoryStatus = 0;
    }

    private void OpenWindow(UIWindowType windowType)
    {
        if (_currentWindow.HasValue && _currentWindow.Value == windowType)
        {
            return; // If the window is already open, do nothing
        }

        CloseCurrentWindow(); // Ensure only one window is open at a time
        _currentWindow = windowType;

        switch (windowType)
        {
            case UIWindowType.PLAY:
                _playPanel.gameObject.SetActive(true);
                break;
            case UIWindowType.INVENTORY:
                _inventoryPanel.gameObject.SetActive(true);
                break;
            case UIWindowType.TALENT:
                _talentPanel.gameObject.SetActive(true);
                break;
        }
    }

    private void CloseCurrentWindow()
    {
        if (!_currentWindow.HasValue) return; // If no window is open, do nothing

        switch (_currentWindow.Value)
        {
            case UIWindowType.PLAY:
                _playPanel.gameObject.SetActive(false);
                break;
            case UIWindowType.INVENTORY:
                _inventoryPanel.gameObject.SetActive(false);
                break;
            case UIWindowType.TALENT:
                _talentPanel.gameObject.SetActive(false);
                break;
        }
        _currentWindow = null; // Reset the current window state
    }

    public void LoadMultiPlayerMode()
    {
        SaveGame.SaveGameLevel.currentLevel = 0;
        GlobalController.CurrentModeGame = GlobalController.ModeGame.ModeOnline;
        SceneManager.LoadScene("RoomScene");
    }

    public void OnMusicVolumeChange(float value)
    {
        AudioManager.instance.SetVolumeMusic();
        SaveGame.SaveSettings.unmuteMusicVolume = value;
        if (!SaveGame.SaveSettings.musicMute)
        {
            SaveGame.SaveSettings.musicVolume = value;
        }
    }

    public void OnSoundEffectVolumeChange(float value)
    {
        SaveGame.SaveSettings.sfxVolume = value;
        AudioManager.instance.SetVolumeEffect();
    }
    public void MusicMute()
    {
        AudioManager.instance.PlaySoundEffect("UIClick");
        if (SaveGame.SaveSettings.musicMute)
        {
            _musicOffIcon.SetActive(false);
            _musicOnIcon.SetActive(true);
            SaveGame.SaveSettings.musicMute = false;
            SaveGame.SaveSettings.musicVolume = SaveGame.SaveSettings.unmuteMusicVolume;
        }
        else
        {
            _musicOffIcon.SetActive(true);
            _musicOnIcon.SetActive(false);
            SaveGame.SaveSettings.musicMute = true;
            SaveGame.SaveSettings.musicVolume = 0;
        }
        AudioManager.instance.SetVolumeMusic();
        SaveGame.SaveSettings.Save();
    }
    public void SoundEffectMute()
    {
        AudioManager.instance.PlaySoundEffect("UIClick");
        if (SaveGame.SaveSettings.sfxMute)
        {
            _sfxOffIcon.SetActive(false);
            _sfxOnIcon.SetActive(true);
            SaveGame.SaveSettings.sfxMute = false;
        }
        else
        {
            _sfxOffIcon.SetActive(true);
            _sfxOnIcon.SetActive(false);
            SaveGame.SaveSettings.sfxMute = true;
        }
        SaveGame.SaveSettings.Save();
    }

    public void SettingButton()
    {
        AudioManager.instance.PlaySoundEffect("UIClick");
        _settingsPanel.SetActive(true);
        StartCoroutine(FeelingTools.FadeInCoroutine(_settingBlur, 0.15f, 0.8f));
        StartCoroutine(FeelingTools.ZoomInUI(_settingItem, 0.5f, 1f, 0.3f, 1.3f));
    }

    public void CloseSetting()
    {
        AudioManager.instance.PlaySoundEffect("UIClick");
        SaveGame.SaveSettings.Save();
        _settingsPanel.SetActive(false);
    }
}