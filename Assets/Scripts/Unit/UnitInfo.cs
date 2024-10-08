using System;
using UnityEngine;

[Serializable]
public class UnitInfo {
    public EUnitAttackType unitType;

    public int SPD;

    public int movementRange = 2;

    public int attackRange = 1;

    public string name;

    public int ATK;

    public int HP;

    public EUnitType type;
}