using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private GameObject _settingPanel;
    [SerializeField] private GameObject _confirmQuitPanel;
    public void SettingButton()
    {
        _settingPanel.SetActive(true);
    }

    public void CloseSetting()
    {
        _settingPanel.SetActive(false);
    }

    public void BackHome()
    {
        _confirmQuitPanel.SetActive(true);
    }

    public void ConfirmBackHome()
    {
        SceneManager.LoadScene("Menu");
    }
    public void CancelBackHome()
    {
        _confirmQuitPanel.SetActive(false);
    }
}
