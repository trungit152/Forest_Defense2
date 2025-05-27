using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    private Dictionary<int, int> _turretExist;
    [SerializeField] private List<Turrets> _turretPrefabs;
    [SerializeField] private List<Traps> _trapsPrefabs;
    [SerializeField] private List<DataTurret> _listDataTurret;
    [SerializeField] private List<Turrets> _turretExistObject;
    [SerializeField] private List<Traps> _trapExistbject;
    public static TurretManager instance;
    private int _lastTurretId = 7;
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
        _turretExist = new Dictionary<int, int>();
    }
    void Start()
    {
        if (DataTurret.listData == null)
        {
            _listDataTurret = DataTurret.GetListData();
        }
        else
        {
            _listDataTurret = DataTurret.listData;
        }

        foreach (var turret in _turretPrefabs)
        {
            turret.ResetValue();
            foreach (var data in _listDataTurret)
            {
                if (turret.ID() == data.TurretId)
                {
                    int mergeLevel = turret.MergeLevel;
                    turret.Init(data.Range, data.ATK[mergeLevel - 1], data.CD[mergeLevel - 1], data.MergeLevel[mergeLevel - 1]);
                }
            }
        }
    }
    public void AddTurret(int id)
    {
        if (_turretExist.ContainsKey(id))
        {
            _turretExist[id]++;
        }
        else
        {
            _turretExist.Add(id, 0);
        }
    }
    public void AddTurretObject(Turrets turret)
    {
        _turretExistObject.Add(turret);
    }
    public void AddTrapObject(Traps trap)
    {
        _trapExistbject.Add(trap);
    }
    public void RemoveTurret(int id)
    {
        if (_turretExist[id] > 1)
        {
            _turretExist[id]--;
        }
        else
        {
            _turretExist.Remove(id);
        }
    }
    public List<int> ExistTurretId()
    {
        List<int> res = new List<int>();
        foreach (var turret in _turretExist)
        {
            res.Add(turret.Key);
        }
        return res;
    }

    public void IncreaseATK(int id, float percent)
    {
        //if id is turret
        if (id <= _lastTurretId)
        {
            //add damage for exist turret
            foreach (var turret in _turretExistObject)
            {
                if (turret.ID() == id)
                {
                    turret.IncreaseATK(percent);
                }
            }
            //add damage for turret prefabs
            foreach (var turret in _turretPrefabs)
            {
                if (turret.ID() == id)
                {
                    turret.IncreaseATK(percent);
                    break;
                }
            }
        }
        //if id is trap
        else
        {
            //add damage for exist trap
            foreach (var trap in _trapExistbject)
            {
                if (trap.ID() == id)
                {
                    trap.IncreaseATK(percent);
                }
            }
            //add damage for trap prefabs
            foreach (var turret in _turretPrefabs)
            {
                if (turret.ID() == id)
                {
                    turret.IncreaseATK(percent);
                    break;
                }
            }
        }
    }
    public void IncreaseATKS(int id, float percent)
    {
        //if id is turret
        if (id <= _lastTurretId)
        {
            //add ATKS for exist turret
            foreach (var turret in _turretExistObject)
            {
                if (turret.ID() == id)
                {
                    turret.IncreaseATKS(percent);
                }
            }
            //add ATKS for turret prefabs
            foreach (var turret in _turretPrefabs)
            {
                if (turret.ID() == id)
                {
                    turret.IncreaseATKS(percent);
                    break;
                }
            }
        }
        //if id is trap
        else
        {
            //add ATKS for exist trap
            foreach (var trap in _trapExistbject)
            {
                if (trap.ID() == id)
                {
                    trap.IncreaseATKS(percent);
                }
            }
            //add ATKS for trap prefabs 
            foreach (var trap in _trapsPrefabs)
            {
                if (trap.ID() == id)
                {
                    trap.IncreaseATKS(percent);
                    break;
                }
            }
        }

    }
    public void IncreaseRange(int id, float percent)
    {
        foreach (var turret in _turretExistObject)
        {
            if (turret.ID() == id)
            {
                turret.IncreaseRange(percent);
            }
        }

        foreach (var turret in _turretPrefabs)
        {
            if (turret.ID() == id)
            {
                turret.IncreaseRange(percent);
                break;
            }
        }
    }
    public void AddBulletAmount(int id)
    {
        foreach (var turret in _turretExistObject)
        {
            if (turret.ID() == id)
            {
                turret.AddBulletAmount();
            }
        }

        foreach (var turret in _turretPrefabs)
        {
            if (turret.ID() == id)
            {
                turret.AddBulletAmount();
                break;
            }
        }
    }
    public void AddMultiShootPercent(float percent)
    {
        foreach (var turret in _turretExistObject)
        {
            if (turret.ID() == IDs.TURRET_CANON)
            {
                turret.AddMultishootPercent(percent);
            }
        }
        foreach (var turret in _turretPrefabs)
        {
            if (turret.ID() == IDs.TURRET_CANON)
            {
                turret.AddMultishootPercent(percent);
            }
        }
    }
    public void AddBounceAmount(float amount)
    {
        foreach (var turret in _turretExistObject)
        {
            if (turret.ID() == IDs.TURRET_THUNDER)
            {
                turret.AddBounceTime(amount);
            }
        }
        foreach (var turret in _turretPrefabs)
        {
            if (turret.ID() == IDs.TURRET_THUNDER)
            {
                turret.AddBounceTime(amount);
            }
        }
    }
    public void AddTrapSpecialEffect1(int id, float percent)
    {
        foreach (var trap in _trapExistbject)
        {
            if (trap.ID() == id)
            {
                trap.SpecialEffect1(percent);
            }
        }
        //add ATKS for trap prefabs 
        foreach (var trap in _trapsPrefabs)
        {
            if (trap.ID() == id)
            {
                trap.SpecialEffect1(percent);
                break;
            }
        }
    }
    public void AddStunEffect(int id, float percent, float time)
    {
        foreach (var turret in _turretExistObject)
        {
            if (turret.ID() == id)
            {
                turret.AddStunStat(percent, time);
            }
        }
        foreach (var turret in _turretPrefabs)
        {
            if (turret.ID() == id)
            {
                turret.AddStunStat(percent, time);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            foreach (var turret in _turretExistObject)
            {
                turret.Merge();
            }
        }
    }

    public void ShowMergeArrow(int id)
    {
        foreach (var turret in _turretExistObject)
        {
            if(turret.ID() == id)
            {
                turret.ShowMergeArrow();
            }
        }
    }
    public void HideMergeArrow()
    {
        foreach (var turret in _turretExistObject)
        {
            turret.HideMergeArrow();
        }
    }
}
