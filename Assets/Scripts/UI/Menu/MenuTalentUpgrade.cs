using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuTalentType
{
    RABBIT,
    CROC,
    FOX,
    PENGUIN,
    DUCK,
    THUNDERBIRD,
    BADGER,
    SPIKE,
    FIRE,
    SWAMP
}

public class MenuTalentUpgrade : MonoBehaviour
{
    private int _currentLevel;
    [SerializeField] private MenuTalentType _talentType;
    [SerializeField] private List<GameObject> _talentLevelBar;

    public void Init()
    {
        switch (_talentType)
        {
            case (MenuTalentType.RABBIT):
                _currentLevel = SaveGame.SaveMenuTalent.rabbitLevel;
                break;
            case (MenuTalentType.CROC):
                _currentLevel = SaveGame.SaveMenuTalent.crocLevel;
                break;
            case (MenuTalentType.FOX):
                _currentLevel = SaveGame.SaveMenuTalent.foxLevel;
                break;
            case (MenuTalentType.PENGUIN):
                _currentLevel = SaveGame.SaveMenuTalent.penguinLevel;
                break;
            case (MenuTalentType.DUCK):
                _currentLevel = SaveGame.SaveMenuTalent.duckLevel;
                break;
            case (MenuTalentType.THUNDERBIRD):
                _currentLevel = SaveGame.SaveMenuTalent.thunderbirdLevel;
                break;
            case (MenuTalentType.BADGER):
                _currentLevel = SaveGame.SaveMenuTalent.badgerLevel;
                break;
            case (MenuTalentType.SPIKE):
                _currentLevel = SaveGame.SaveMenuTalent.spikeLevel;
                break;
            case (MenuTalentType.FIRE):
                _currentLevel = SaveGame.SaveMenuTalent.fireLevel;
                break;
            case (MenuTalentType.SWAMP):
                _currentLevel = SaveGame.SaveMenuTalent.swampLevel;
                break;
            default:
                _currentLevel = 0;
                break;

        }

        UpdateTalentLevelBar();
    }

    private void Start()
    {
        Init();
    }

    public void OnUpgradeMenuTalent()
    {
        if (_currentLevel >= 5 || SaveGame.SaveTalentPoint.TalentPoint <= 0) return;
        _currentLevel++;
        SaveGame.SaveTalentPoint.AddTalentPoint(-1);
        ButtonUI.instance.UpdateHeaderText();
        SaveData();
        UpdateTalentLevelBar();
    }

    public void UpdateTalentLevelBar()
    {
        for (int i = 0; i < _talentLevelBar.Count; i++)
        {
            if (i < _currentLevel)
            {
                _talentLevelBar[i].SetActive(true);
            }
            else
            {
                _talentLevelBar[i].SetActive(false);
            }
        }
    }

    public void SaveData()
    {
        switch (_talentType)
        {
            case (MenuTalentType.RABBIT):
                SaveGame.SaveMenuTalent.rabbitLevel = _currentLevel;
                SaveGame.SaveMenuTalent.Save();
                break;
            case (MenuTalentType.CROC):
                SaveGame.SaveMenuTalent.crocLevel = _currentLevel;
                SaveGame.SaveMenuTalent.Save();
                break;
            case (MenuTalentType.FOX):
                SaveGame.SaveMenuTalent.foxLevel = _currentLevel;
                SaveGame.SaveMenuTalent.Save();
                break;
            case (MenuTalentType.PENGUIN):
                SaveGame.SaveMenuTalent.penguinLevel = _currentLevel;
                SaveGame.SaveMenuTalent.Save();
                break;
            case (MenuTalentType.DUCK):
                SaveGame.SaveMenuTalent.duckLevel = _currentLevel;
                SaveGame.SaveMenuTalent.Save();
                break;
            case (MenuTalentType.THUNDERBIRD):
                SaveGame.SaveMenuTalent.thunderbirdLevel = _currentLevel;
                SaveGame.SaveMenuTalent.Save();
                break;
            case (MenuTalentType.BADGER):
                SaveGame.SaveMenuTalent.badgerLevel = _currentLevel;
                SaveGame.SaveMenuTalent.Save();
                break;
            case (MenuTalentType.SPIKE):
                SaveGame.SaveMenuTalent.spikeLevel = _currentLevel;
                SaveGame.SaveMenuTalent.Save();
                break;
            case (MenuTalentType.FIRE):
                SaveGame.SaveMenuTalent.fireLevel = _currentLevel;
                SaveGame.SaveMenuTalent.Save();
                break;
            case (MenuTalentType.SWAMP):
                SaveGame.SaveMenuTalent.swampLevel = _currentLevel;
                SaveGame.SaveMenuTalent.Save();
                break;
            default:
                _currentLevel = 0;
                break;
        }
    }
}


