using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [Header("UI WIN")]
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private Image _winPanelBlur;
    [SerializeField] private RectTransform _winPanelImage;
    [SerializeField] private List<RectTransform> _stars;
    [SerializeField] private Sprite _starOn;
    [SerializeField] private Sprite _starOff;
    [SerializeField] private TextMeshProUGUI _talentPointText;
    [SerializeField] private TextMeshProUGUI _coinText;
    [Header("UI Setting")]
    [SerializeField] private GameObject _settingPanel;
    [SerializeField] private Image _settingBlur;
    [SerializeField] private RectTransform _settingItem;
    [SerializeField] private RectTransform _confirmQuitPanel;
    [SerializeField] private RectTransform _confirmReplay;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundEffectSlider;
    [SerializeField] private GameObject _musicOffIcon;
    [SerializeField] private GameObject _musicOnIcon;
    [SerializeField] private GameObject _sfxOffIcon;
    [SerializeField] private GameObject _sfxOnIcon;
    [Header("UI Online")]
    [SerializeField] private GameObject _onlineWinPanel;
    [SerializeField] private Image _onlineWinBlur;
    [SerializeField] private TextMeshProUGUI _winScore;
    [SerializeField] private TextMeshProUGUI _winCoin;
    [SerializeField] private TextMeshProUGUI _winTalentPoint;
    [SerializeField] private RectTransform _onlineWinPanelImage;
    [SerializeField] private GameObject _onlineLosePanel;
    [SerializeField] private Image _onlineLoseBlur;
    [SerializeField] private TextMeshProUGUI _loseScore;
    [SerializeField] private TextMeshProUGUI _loseCoin;
    [SerializeField] private TextMeshProUGUI _loseTalentPoint;
    [SerializeField] private RectTransform _onlineLosePanelImage;
    [SerializeField] private GameObject _onlineDrawPanel;

    public static InGameUI instance;
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
    private void Start()
    {
        _musicSlider.onValueChanged.AddListener(OnMusicVolumeChange);
        _soundEffectSlider.onValueChanged.AddListener(OnSoundEffectVolumeChange);
        _musicSlider.value = SaveGame.SaveSettings.unmuteMusicVolume;
        _soundEffectSlider.value = SaveGame.SaveSettings.sfxVolume;
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
    }
    public void SettingButton()
    {
        AudioManager.instance.PlaySoundEffect("UIClick");
        GameStat.ChangeGameTimeScale(0);
        EnemiesAnimator.instance.ChangeAnimatorSpeed(GameStat.gameTimeScale);
        _settingPanel.gameObject.SetActive(true);
        StartCoroutine(FeelingTools.FadeInCoroutine(_settingBlur, 0.15f, 0.8f));
        StartCoroutine(FeelingTools.ZoomInUI(_settingItem, 0.5f, 1f, 0.3f, 1.3f));
    }

    public void CloseSetting()
    {
        AudioManager.instance.PlaySoundEffect("UIClick");
        SaveGame.SaveSettings.Save();
        _settingPanel.SetActive(false);
        GameStat.ChangeGameTimeScale(1);
        EnemiesAnimator.instance.ChangeAnimatorSpeed(GameStat.gameTimeScale);
    }

    public void Replay()
    {
        AudioManager.instance.PlaySoundEffect("UIClick");
        StartCoroutine(FeelingTools.ZoomInUI(_confirmReplay, 0.5f, 1f, 0.3f, 1.3f));
    }
    public void ConfirmReplay()
    {
        AudioManager.instance.PlaySoundEffect("UIClick");
        SaveGame.SaveSettings.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void CancelReplay()
    {
        AudioManager.instance.PlaySoundEffect("UIClick");
        _confirmReplay.gameObject.SetActive(false);
    }
    public void BackHomeClick()
    {
        AudioManager.instance.PlaySoundEffect("UIClick");
        StartCoroutine(FeelingTools.ZoomInUI(_confirmQuitPanel, 0.5f, 1f, 0.3f, 1.3f));
    }

    public void ConfirmBackHome()
    {
        AudioManager.instance.PlaySoundEffect("UIClick");
        SaveGame.SaveSettings.Save();
        AudioManager.instance.FadeOutMusic("CombatMusic", 1f);
        if(GlobalController.CurrentModeGame == GlobalController.ModeGame.ModeOnline)
        {
            StartCoroutine(IEBackHome());
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }

    private IEnumerator IEBackHome()
    {
        PhotonNetwork.LeaveRoom();
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene("Menu");
    }
    
    public void CancelBackHome()
    {
        AudioManager.instance.PlaySoundEffect("UIClick");
        _confirmQuitPanel.gameObject.SetActive(false);
    }

    public void ShowWinPanel(int star = 3)
    {
        if(_winPanel.activeSelf)
        {
            return;
        }
        AudioManager.instance.FadeOutMusic("CombatMusic");
        AudioManager.instance.PlaySoundEffect("Win");
        //Set info off
        foreach (var starItem in _stars)
        {
            starItem.gameObject.SetActive(false);
        }
        _talentPointText.gameObject.SetActive(false);
        _coinText.gameObject.SetActive(false);
        //Start show
        _winPanel.SetActive(true);
        StartCoroutine(FeelingTools.FadeInCoroutine(_winPanelBlur, 0.15f, 0.8f));
        StartCoroutine(FeelingTools.ZoomInUI(_winPanelImage, 0.5f, 1f, 0.3f, 1.3f));
        StartCoroutine(WaitForSecond(0.7f, () => ShowStar(star)));

    }

    public void ShowStar(int star)
    {
        StartCoroutine(ShowStarCoroutine(star));
    }

    private IEnumerator ShowStarCoroutine(int star)
    {
        for (int i = 0; i < _stars.Count; i++)
        {
            if (i < star)
            {
                _stars[i].GetComponent<Image>().sprite = _starOn;
            }
            else
            {
                _stars[i].GetComponent<Image>().sprite = _starOff;
            }

            StartCoroutine(FeelingTools.ZoomInUI(_stars[i], 0.2f, 1f, 0.25f, 1.5f));

            yield return new WaitForSeconds(0.25f);
        }
        //show talent point
        StartCoroutine(FeelingTools.ShowNumberText(_talentPointText, 0, star, 1f));
        SaveGame.SaveTalentPoint.AddTalentPoint(star);
        // show coin
        StartCoroutine(FeelingTools.ShowNumberText(_coinText, 0, star*582, 1f));
        SaveGame.SaveTalentPoint.AddCoin(star * 582);
    }
    private IEnumerator WaitForSecond(float second, Action callBack = null)
    {
        yield return new WaitForSeconds(second);
        callBack?.Invoke();
    }

    public void ShowOnlineWinPanel()
    {
        if (_onlineWinPanel.activeSelf)
        {
            return;
        }
        //Audio
        AudioManager.instance.FadeOutMusic("CombatMusic", 0.4f);
        AudioManager.instance.PlaySoundEffect("Win");
        //Set info off
        _winTalentPoint.gameObject.SetActive(false);
        _winCoin.gameObject.SetActive(false);
        //Start show
        _onlineWinPanel.SetActive(true);
        StartCoroutine(FeelingTools.FadeInCoroutine(_onlineWinBlur, 0.15f, 0.8f));
        StartCoroutine(FeelingTools.ZoomInUI(_onlineWinPanelImage, 0.5f, 1f, 0.3f, 1.3f));
        //show score
        StartCoroutine(FeelingTools.ShowNumberText(_winScore, 0, 3, 1f));
        SaveGame.SaveTalentPoint.AddTalentPoint(3);
        //show talent point
        StartCoroutine(FeelingTools.ShowNumberText(_winTalentPoint, 0, 3, 1f));
        SaveGame.SaveTalentPoint.AddTalentPoint(3);
        // show coin
        AudioManager.instance.PlaySoundEffect("CoinCollect");
        StartCoroutine(FeelingTools.ShowNumberText(_winCoin, 0, GameController.instance.Point, 1f));
        SaveGame.SaveTalentPoint.AddCoin(GameController.instance.Point);
    }

    public void ShowLosePanel()
    { 
        if (_onlineLosePanel.gameObject.activeSelf)
        {
            Debug.Log("Show Lose Panellllll");

            return;
        }
        Debug.Log("Show Lose Panel");
        GameStat.ChangeGameTimeScale(0);
        EnemiesAnimator.instance.ChangeAnimatorSpeed(GameStat.gameTimeScale);
        AudioManager.instance.FadeOutMusic("CombatMusic");
        //Set info off
        _loseTalentPoint.gameObject.SetActive(false);
        _loseCoin.gameObject.SetActive(false);
        //Start show
        _onlineLosePanel.SetActive(true);
        StartCoroutine(FeelingTools.FadeInCoroutine(_onlineLoseBlur, 0.15f, 0.8f));
        StartCoroutine(FeelingTools.ZoomInUI(_onlineLosePanelImage, 0.5f, 1f, 0.3f, 1.3f));
        //show score
        StartCoroutine(FeelingTools.ShowNumberText(_loseScore, 0, 3, 1f));
        SaveGame.SaveTalentPoint.AddTalentPoint(3);
        //show talent point
        StartCoroutine(FeelingTools.ShowNumberText(_loseTalentPoint, 0, WallControl.instance.Star, 1f));
        SaveGame.SaveTalentPoint.AddTalentPoint(3);
        // show coin
        StartCoroutine(FeelingTools.ShowNumberText(_loseCoin, 0, GameController.instance.Point, 1f));
        SaveGame.SaveTalentPoint.AddCoin(GameController.instance.Point);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ShowOnlineWinPanel();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            ShowLosePanel();
        }
    }

    public void LoadMenu()
    {
        AudioManager.instance.FadeOutMusic("CombatMusic", 1f);
        if(GlobalController.CurrentModeGame == GlobalController.ModeGame.ModeOnline)
        {
            StartCoroutine(IEBackHome());
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
        StartCoroutine(IEBackHome());
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
}