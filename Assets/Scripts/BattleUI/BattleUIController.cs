using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BattleUIController : MonoBehaviour {
    private VisualElement root;
    private VisualElement SkillBox;

    private Button Attack;
    private Button Heal;
    private Button Defend;
    private Button EndTurn;
    private Button End;
    private Label Text;

    private Button BasicAttack;
    private Button Skill1;
    private Button Skill2;
    private Button Skill3;
    private Button Skill4;
    private Button Cancel;


    private void Start() {
        this.root = this.GetComponent<UIDocument>().rootVisualElement;

        this.SkillBox =  this.root.Query<VisualElement>("SkillBox");
        this.Attack = this.root.Query<Button>("Attack");                                                                                                                
        this.Heal = this.root.Query<Button>("Heal");
        this.Defend = this.root.Query<Button>("Defend");
        this.EndTurn = this.root.Query<Button>("EndTurn");
        this.End = this.root.Query<Button>("Return");

        this.Text = this.root.Query<Label>("EndText");

        this.BasicAttack = this.root.Query<Button>("Basic Attack");
        this.Skill1 = this.root.Query<Button>("Skill 1");
        this.Skill2 = this.root.Query<Button>("Skill 2");
        this.Skill3 = this.root.Query<Button>("Skill 3");                                     
        this.Skill4 = this.root.Query<Button>("Skill 4");

        this.Cancel = this.root.Query<Button>("CancelAttack");

        this.EnableActionBoxClick();

        EventBroadcaster.Instance.AddObserver(EventNames.UIEvents.ENABLE_CLICKS, this.EnableActionBoxClick);
        EventBroadcaster.Instance.AddObserver(EventNames.UIEvents.DISABLE_CLICKS, this.DisableActionBoxClick);

        this.SkillBox.style.display = DisplayStyle.None;
    }
    private void EnableActionBoxClick() {
        this.Attack.clicked += this.OnAttack;
        this.Heal.clicked += this.OnHeal;
        this.Defend.clicked += this.OnDefend;
        this.EndTurn.clicked += this.OnEndTurn;

        this.Attack.style.display = DisplayStyle.Flex;
        this.Heal.style.display = DisplayStyle.Flex;
        this.Defend.style.display = DisplayStyle.Flex;
        this.EndTurn.style.display = DisplayStyle.Flex;
    }
    private void DisableActionBoxClick() {
        this.Attack.clicked -= this.OnAttack;
        this.Heal.clicked -= this.OnHeal;
        this.Defend.clicked -= this.OnDefend;
        this.EndTurn.clicked -= this.OnEndTurn;

        this.Attack.style.display = DisplayStyle.None;
        this.Heal.style.display = DisplayStyle.None;
        this.Defend.style.display = DisplayStyle.None;
        this.EndTurn.style.display = DisplayStyle.None;
    }
    private void EnableSkillBoxClick() {
        this.BasicAttack.clicked += this.OnBasicAttack;
        this.Skill1.clicked += this.OnSkill1;
        this.Skill2.clicked += this.OnSkill2;
        this.Skill3.clicked += this.OnSkill3;
        this.Skill4.clicked += this.OnSkill4;
        this.Cancel.clicked += this.OnCancel;

        this.SkillBox.style.display = DisplayStyle.Flex;
    }
    private void DisableSkillBoxClick() {
        this.BasicAttack.clicked -= this.OnBasicAttack;
        this.Skill1.clicked -= this.OnSkill1;
        this.Skill2.clicked -= this.OnSkill2;
        this.Skill3.clicked -= this.OnSkill3;
        this.Skill4.clicked -= this.OnSkill4;
        this.Cancel.clicked -= this.OnCancel;

        this.SkillBox.style.display = DisplayStyle.None;
    }
    private void OnAttack() {
        if(!UnitActionManager.Instance.hadAttacked) {
            UnitActionManager.Instance.OnAttack = !UnitActionManager.Instance.OnAttack;
            if (UnitActionManager.Instance.OnHeal) {
                UnitActionManager.Instance.OnHeal = false;
            }

            if (UnitActionManager.Instance.OnMove) {
                UnitActionManager.Instance.OnMove = false;
            }

            this.EnableSkillBoxClick();
            this.DisableActionBoxClick();
        }
    }
    private void OnHeal() {
        if(!UnitActionManager.Instance.hadHealed) {
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

    private void OnDefend() {
        if(!UnitActionManager.Instance.hadMoved) {
            UnitActionManager.Instance.OnMove = !UnitActionManager.Instance.OnMove;

            if (UnitActionManager.Instance.OnAttack) {
                UnitActionManager.Instance.OnAttack = false;
            }

            if (UnitActionManager.Instance.OnHeal) {
                UnitActionManager.Instance.OnHeal = false;
            }
        }
    }

    private void OnEndTurn() {
        if( !UnitActionManager.Instance.OnAttack && 
            !UnitActionManager.Instance.OnHeal &&
            !UnitActionManager.Instance.OnMove ) {

            UnitActionManager.Instance.OnAttack = false;
            UnitActionManager.Instance.OnHeal = false;
            UnitActionManager.Instance.OnMove = false;

            UnitActionManager.Instance.NextUnitTurn();
        }

    }

    private void OnBasicAttack() {

    }
    private void OnSkill1() {

    }
    private void OnSkill2() {

    }
    private void OnSkill3() {

    }
    private void OnSkill4() {

    }
    private void OnCancel() {
        this.DisableSkillBoxClick();
        this.EnableActionBoxClick();
    }


    public void EndScreen(int scenario) {
        this.DisableActionBoxClick();


        switch(scenario) {
            case 1:
                this.Text.text = "Defeat";
                break;
            case 2: this.Text.text = "Level Cleared!";
                break;
            default: break;
        }

        this.Text.style.display = DisplayStyle.Flex;
        this.End.style.display = DisplayStyle.Flex;

        Debug.Log("Text: " + this.Text.visible);
        Debug.Log("End: " + this.End.visible);

        this.End.clicked += this.OnEndBattle;
    }

    private void OnEndBattle() {
        

    }
}
