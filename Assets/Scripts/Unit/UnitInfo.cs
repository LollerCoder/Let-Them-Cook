using System;
using UnityEngine;

[Serializable]
public class UnitInfo {
    public EUnitAttackType unitType;

    public int movementRange = 2;

    public int attackRange = 1;

    public string name;

    public JobClass jobClass = new JobClass();

    public EUnitType type;
}