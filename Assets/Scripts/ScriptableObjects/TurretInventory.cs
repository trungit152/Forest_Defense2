using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretInventory", menuName = "ScriptableObjects/TurretInventory", order = 1)]
public class TurretInventory : ScriptableObject
{
    public List<TurretInventStatus> turretInventList;
}