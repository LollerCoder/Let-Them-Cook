using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitStats : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Text ATK;    
    [SerializeField]
    private Text SPD;
    [SerializeField]
    private Text DEF;
    [SerializeField]
    private Text ACC;

    [SerializeField]
    private Text EXP;

    public void SetUnitStats(Unit unit) {
        this.SetHealth(unit.MAXHP, unit.HP);
        this.SetATK(unit.Attack);
        this.SetSPD(unit.Speed);
        this.SetDEF(unit.Defense);
        this.SetACC(unit.Accuracy);
        this.SetEXP(unit.Experience);
    }

    private void SetHealth(int UnitMaxHP, int UnitHP) {
        this.slider.maxValue = UnitMaxHP;
        this.slider.value = UnitHP;
    }

    private void SetATK(float atk) {
        this.ATK.text = "ATK: " + atk;
    }
    private void SetSPD(float spd) {
        this.SPD.text = "SPD: " + spd;
    }
    private void SetDEF(float def) {
        this.DEF.text = "DEF: " + def;
    }
    private void SetACC(float acc) {
        this.ACC.text = "ACC: " + acc;
    }

     private void SetEXP(float exp) {
        this.EXP.text = "EXP: " + exp;
    }
}
