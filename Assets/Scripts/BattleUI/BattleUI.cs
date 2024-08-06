using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour {
    [SerializeField]
    private Image characterAvatar;
    [SerializeField]
    private Image SkillBox;
    [SerializeField]
    private List<Sprite> skillTemplate;

    private bool skillShow = false;
    private void Start() {

    }
    public void ToggleSkillBox() {
        this.skillShow = !this.skillShow;
        this.SkillBox.GetComponent<Animator>().SetBool("Show", this.skillShow);
    }
    public void OnAttack() {
        if (!UnitActionManager.Instance.hadAttacked) {
            if (UnitActionManager.Instance.OnAttack) {
                UnitActionManager.Instance.OnAttack = false;
            }

            if (UnitActionManager.Instance.OnHeal) {
                UnitActionManager.Instance.OnHeal = false;
            }

            if (UnitActionManager.Instance.OnMove) {
                UnitActionManager.Instance.OnMove = false;
            }

            if (UnitActionManager.Instance.OnDefend) {
                UnitActionManager.Instance.OnDefend = false;
            }


            this.ToggleSkillBox();
        }
    }
    public void OnHeal() {
        if (!UnitActionManager.Instance.hadHealed) {
            UnitActionManager.Instance.OnHeal = !UnitActionManager.Instance.OnHeal;
            UnitActionManager.Instance.UnitHeal();
            if (UnitActionManager.Instance.OnAttack) {
                UnitActionManager.Instance.OnAttack = false;

            }

            if (UnitActionManager.Instance.OnMove) {
                UnitActionManager.Instance.OnMove = false;
            }
        }
    }
    public void OnDefend() {
        if (!UnitActionManager.Instance.hadMoved) {
            UnitActionManager.Instance.OnMove = !UnitActionManager.Instance.OnMove;

            if (UnitActionManager.Instance.OnAttack) {
                UnitActionManager.Instance.OnAttack = false;
            }

            if (UnitActionManager.Instance.OnHeal) {
                UnitActionManager.Instance.OnHeal = false;
            }
        }
    }

    public void OnEndTurn() {
        if (!UnitActionManager.Instance.OnAttack &&
            !UnitActionManager.Instance.OnHeal &&
            !UnitActionManager.Instance.OnMove) {

            UnitActionManager.Instance.OnAttack = false;
            UnitActionManager.Instance.OnHeal = false;
            UnitActionManager.Instance.OnMove = false;

            UnitActionManager.Instance.NextUnitTurn();
        }
    }

    public void NextCharacterAvatar(Unit unit) {
        this.characterAvatar.sprite = unit.GetComponent<SpriteRenderer>().sprite;
    }

    public void OnBasicAttack() {
        UnitActionManager.Instance.OnAttack = !UnitActionManager.Instance.OnAttack;
        UnitActionManager.Instance.numAttack = 0;
    }
    public void OnSkill1() {
        UnitActionManager.Instance.OnAttack = !UnitActionManager.Instance.OnAttack;
        UnitActionManager.Instance.numAttack = 1;
    }
    public void OnSkill2() {
        UnitActionManager.Instance.OnAttack = !UnitActionManager.Instance.OnAttack;
        UnitActionManager.Instance.numAttack = 2;
    }
    public void OnSkill3() {
        UnitActionManager.Instance.OnAttack = !UnitActionManager.Instance.OnAttack;
        UnitActionManager.Instance.numAttack = 3;
    }
    public void OnSkill4() {
        UnitActionManager.Instance.OnAttack = !UnitActionManager.Instance.OnAttack;
        UnitActionManager.Instance.numAttack = 4;
    }
    public void OnCancel() {
        //this.DisableSkillBoxClick();
        //this.EnableActionBoxClick();

        UnitActionManager.Instance.OnAttack = false;
    }

    public void EndScreen(int scenario) {
        //this.DisableActionBoxClick();
        //this.DisableSkillBoxClick();

        //switch (scenario) {
        //    case 1:
        //        this.Text.text = "Defeat";
        //        break;
        //    case 2:
        //        this.Text.text = "Level Cleared!";
        //        break;
        //    default: break;
        //}

        //this.Text.style.display = DisplayStyle.Flex;
        //this.End.style.display = DisplayStyle.Flex;

        //Debug.Log("Text: " + this.Text.visible);
        //Debug.Log("End: " + this.End.visible);

        //this.End.clicked += this.OnEndBattle;
    }
}
