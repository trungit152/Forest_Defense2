using System;
using System.Collections.Generic;
using UnityEngine;

public class TalentsController : MonoBehaviour
{
    [SerializeField] private TalentBarIcon _talentBarIcon;
    [SerializeField] private GameObject _talentBar;
    [SerializeField] private List<DataTalent> _listDataTalent;
    private List<int> _chosenTalentId;
    private float _addEffectPercent = 100;
    public static TalentsController instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        _chosenTalentId = new List<int>();
        _listDataTalent = DataTalent.GetListData();
    }

    public List<int> GetRandomExistId()
    {
        List<int> existTalentIds = new List<int>();
        List<int> addTalentIDs = new List<int>();
        List<int> res = new List<int>();
        foreach (var talent in DataTalent.listData)
        {
            //check if is Add talent
            if (!CheckAddableTalent(talent.TalentId) && SaveGame.SaveTurret.GetTurretDataByID(talent.TurretId).isEquiped
                && !addTalentIDs.Contains(talent.TalentId))
            {
                addTalentIDs.Add(talent.TalentId);
            }
            else
            {
                foreach (var id in TurretManager.instance.ExistTurretId())
                {
                    if (talent.TurretId == id && !existTalentIds.Contains(talent.TalentId))
                    {
                        existTalentIds.Add(talent.TalentId);
                    }
                }
            }
        }
        if (existTalentIds.Count > 0)
        {
            foreach (var id in addTalentIDs)
            {
                if (!existTalentIds.Contains(id) && FeelingTools.RandomChance(_addEffectPercent))
                {
                    existTalentIds.Add(id);
                }
            }
        }
        else
        {
            foreach (var id in addTalentIDs)
            {
                existTalentIds.Add(id);
            }
        }
        for (int i = 0; i < 3; i++)
        {
            if (existTalentIds.Count <= 0)
            {
                //add gold
                break;
            }

            int index = UnityEngine.Random.Range(0, existTalentIds.Count);
            res.Add(existTalentIds[index]);
            existTalentIds.RemoveAt(index);
        }
        return res;
    }

    public void AddChosenTalent(int id)
    {
        if (!CheckAddableTalent(id)) return;
        if (!_chosenTalentId.Contains(id))
        {
            _chosenTalentId.Add(id);
            AddTalentBarIcon(id);
        }
    }
    private void AddTalentBarIcon(int id)
    {
        var talentBarIcon = Instantiate(_talentBarIcon);
        talentBarIcon.transform.SetParent(_talentBar.transform);
        talentBarIcon.SetTalent(id);
    }
    public List<int> GetChosenTalentsList()
    {
        return _chosenTalentId;
    }

    public bool CheckAddableTalent(int id)
    {
        if (id == IDs.TALENT_ADD_BALISTA || id == IDs.TALENT_ADD_BOW || id == IDs.TALENT_ADD_FIRE || id == IDs.TALENT_ADD_CANNON
            || id == IDs.TALENT_ADD_SPIKE || id == IDs.TALENT_ADD_THUNDER || id == IDs.TALENT_ADD_WATER)
        {
            return false;
        }
        return true;
    }
    public void DecreaseAppearAddPercent()
    {
        _addEffectPercent *= 0.7f;
    }
}
