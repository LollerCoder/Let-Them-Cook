using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BattleUIController : MonoBehaviour {
    private VisualElement root;

    private Button Attack;
    private Button Heal;
    private Button Move;
    private Button EndTurn;
    private Button End;
    private Label Text;


    private void Start() {
        this.root = this.GetComponent<UIDocument>().rootVisualElement;

        this.Attack = this.root.Query<Button>("Attack");
        this.Heal = this.root.Query<Button>("Heal");
        this.Move = this.root.Query<Button>("Move");
        this.EndTurn = this.root.Query<Button>("EndTurn");
        this.EndTurn = this.root.Query<Button>("EndTurn");
        this.End = this.root.Query<Button>("Return");

        this.Text = this.root.Query<Label>("EndText");

        this.EnableClick();

        EventBroadcaster.Instance.AddObserver(EventNames.UIEvents.ENABLE_CLICKS, this.EnableClick);
        EventBroadcaster.Instance.AddObserver(EventNames.UIEvents.DISABLE_CLICKS, this.DisableClick);
    }
    private void EnableClick() {
        this.Attack.clicked += this.OnAttack;
        this.Heal.clicked += this.OnHeal;
        this.Move.clicked += this.OnMove;
        this.EndTurn.clicked += this.OnEndTurn;
    }

    private void DisableClick() {
        this.Attack.clicked -= this.OnAttack;
        this.Heal.clicked -= this.OnHeal;
        this.Move.clicked -= this.OnMove;
        this.EndTurn.clicked -= this.OnEndTurn;
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

    private void OnMove() {
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

    public void EndScreen(int scenario) {
        this.DisableClick();
        this.Attack.style.display = DisplayStyle.None;
        this.Heal.style.display = DisplayStyle.None;
        this.Move.style.display = DisplayStyle.None;
        this.EndTurn.style.display = DisplayStyle.None;

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
