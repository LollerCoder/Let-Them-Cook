
using System;
using UnityEngine;

[Serializable]
public class JobClass {
    public EJobClass className;
    public int strength;
    public int dexterity;
    public int constitution;
    public int intelligence;
    public int wisdom;
    public int charisma;
    public EJobClass Name {
        set { this.className = value; }
        get { return this.className; }
    }

    private int rand() {
        return UnityEngine.Random.Range(1, 3);
    }
    public void GenerateStats(Unit unit) {

        if (unit.Info.type == EUnitType.Boss) {
            int rand = this.rand();
            Debug.Log(rand);
            switch (rand) {
                case 1:
                    unit.Info.jobClass.Name = EJobClass.Mage;
                    break;
                case 2:
                    unit.Info.jobClass.Name = EJobClass.Bruiser;
                    break;
                case 3:
                    unit.Info.jobClass.Name = EJobClass.Sabre;
                    break;
            }
        }

        switch (unit.Info.jobClass.Name) {
            case EJobClass.Sage: this.GenerateSageStats(unit);
                break;
            case EJobClass.Mage: this.GenerateMageStats(unit);
                break;
            case EJobClass.Bruiser: this.GenerateBruiserStats(unit);
                break;
            case EJobClass.Sabre: this.GenerateSabreStats(unit);
                break;
        }
    }

    private void GenerateSageStats(Unit unit) {
        unit.Info.jobClass.strength = 14;
        unit.Info.jobClass.dexterity = 14;
        unit.Info.jobClass.constitution = 13;
        unit.Info.jobClass.intelligence = 13;
        unit.Info.jobClass.wisdom = 15;
        unit.Info.jobClass.charisma = 15;

        unit.Info.movementRange = 2;

    }
    private void GenerateMageStats(Unit unit) {
        if (unit.Info.type == EUnitType.Ally) {
            unit.Info.jobClass.strength = 8;
            unit.Info.jobClass.dexterity = 13;
            unit.Info.jobClass.constitution = 12;
            unit.Info.jobClass.intelligence = 18;
            unit.Info.jobClass.wisdom = 17;
            unit.Info.jobClass.charisma = 12;

            unit.Info.movementRange = 2;
        }
        else {
            if (unit.Info.type == EUnitType.Enemy) {
                unit.Info.jobClass.strength = 8;
                unit.Info.jobClass.dexterity = UnityEngine.Random.Range(10, 13);
                unit.Info.jobClass.constitution = UnityEngine.Random.Range(8, 13);
                unit.Info.jobClass.intelligence = UnityEngine.Random.Range(8, 15);
                unit.Info.jobClass.wisdom = UnityEngine.Random.Range(8, 12);
                unit.Info.jobClass.charisma = UnityEngine.Random.Range(8, 10);
            }
            if (unit.Info.type == EUnitType.Boss) {
                unit.Info.jobClass.strength = 10;
                unit.Info.jobClass.dexterity = 15;
                unit.Info.jobClass.constitution = 15;
                unit.Info.jobClass.intelligence = 19; // mage boss
                unit.Info.jobClass.wisdom = 17;
                unit.Info.jobClass.charisma = 15;
            }
        }
    }
    private void GenerateBruiserStats(Unit unit) {
        if (unit.Info.type == EUnitType.Ally) {
            unit.Info.jobClass.strength = 11;
            unit.Info.jobClass.dexterity = 12;
            unit.Info.jobClass.constitution = 18;
            unit.Info.jobClass.intelligence = 8;
            unit.Info.jobClass.wisdom = 13;
            unit.Info.jobClass.charisma = 15;

            unit.Info.movementRange = 2;
        }
        else {
            if (unit.Info.type == EUnitType.Enemy) {
                unit.Info.jobClass.strength = UnityEngine.Random.Range(8, 10);
                unit.Info.jobClass.dexterity = UnityEngine.Random.Range(10, 12);
                unit.Info.jobClass.constitution = UnityEngine.Random.Range(13, 16);
                unit.Info.jobClass.intelligence = UnityEngine.Random.Range(8, 10);
                unit.Info.jobClass.wisdom = UnityEngine.Random.Range(8, 12);
                unit.Info.jobClass.charisma = UnityEngine.Random.Range(8, 10);
            }
            if (unit.Info.type == EUnitType.Boss) {
                unit.Info.jobClass.strength = 11;
                unit.Info.jobClass.dexterity = 15;
                unit.Info.jobClass.constitution = 19;
                unit.Info.jobClass.intelligence = 8;
                unit.Info.jobClass.wisdom = 13;
                unit.Info.jobClass.charisma = 15;
            }
        }
    }
    private void GenerateSabreStats(Unit unit) {
        if (unit.Info.type == EUnitType.Ally) {
            unit.Info.jobClass.strength = 12;
            unit.Info.jobClass.dexterity = 18;
            unit.Info.jobClass.constitution = 12;
            unit.Info.jobClass.intelligence = 10;
            unit.Info.jobClass.wisdom = 14;
            unit.Info.jobClass.charisma = 13;

            unit.Info.movementRange = 4;
        }
        else {
            if (unit.Info.type == EUnitType.Enemy) {
                unit.Info.jobClass.strength = UnityEngine.Random.Range(8, 11);
                unit.Info.jobClass.dexterity = UnityEngine.Random.Range(13, 16);
                unit.Info.jobClass.constitution = UnityEngine.Random.Range(8, 13);
                unit.Info.jobClass.intelligence = UnityEngine.Random.Range(8, 10);
                unit.Info.jobClass.wisdom = UnityEngine.Random.Range(8, 12); ;
                unit.Info.jobClass.charisma = UnityEngine.Random.Range(8, 10); ;
            }
            if (unit.Info.type == EUnitType.Boss) {
                unit.Info.jobClass.strength = 12;
                unit.Info.jobClass.dexterity = 19;
                unit.Info.jobClass.constitution = 13;
                unit.Info.jobClass.intelligence = 13;
                unit.Info.jobClass.wisdom = 13;
                unit.Info.jobClass.charisma = 13;
            }
        }
    }
}