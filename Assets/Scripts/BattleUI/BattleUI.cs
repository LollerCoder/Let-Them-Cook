using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour {
    [SerializeField]
    private Button characterAvatar;
    [SerializeField]
    private Image SkillBox;
    [SerializeField]
    private GameObject ActionBox;
    [SerializeField]
    private List<Sprite> attackSprites;

    public List<Image> Turn;

    [SerializeField]
    private List<Button> Attacks;

    private UnitStats _unitStats;

    private bool skillShow = false;
    private bool actionShow = false;

    private bool[] attackNum = { false, false, false, false, false };
    private bool[] skillSlots = { false, false, false, false, false };
    private void Start() {
        this._unitStats = this.GetComponentInChildren<UnitStats>();

        if(this._unitStats == null) {
            Debug.Log("ERROR: UNITSTATS CANNOT BE FOUND (BATTLEUI.CS, START() )");
        }

        
    }

    public void AvatarClick() {
        Parameters param = new Parameters();
        
        param.PutExtra("POS", UnitActionManager.Instance.GetUnit().transform.position);

        EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.ON_AVATAR_CLICK, param);
    }
    public void ToggleSkillBox() {
        this.skillShow = !this.skillShow;
        this.SkillBox.GetComponent<Animator>().SetBool("Show", this.skillShow);
    }

    public void ToggleActionBox() {
        this.actionShow = !this.actionShow;
        this.ActionBox.GetComponent<Animator>().SetBool("Show", this.actionShow);
    }
    public void OnAttack() {
        if (!UnitActionManager.Instance.hadAttacked) {
            if (UnitActionManager.Instance.OnAttack) {
                UnitActionManager.Instance.OnAttack = false;
            }
            else {
                this.ResetActions();
                UnitActionManager.Instance.OnAttack = true;
            }
            this.ToggleSkillBox();
        }
    }
    public void OnHeal() {
        if (!UnitActionManager.Instance.hadHealed) {
            if (UnitActionManager.Instance.OnHeal) {
                UnitActionManager.Instance.OnHeal = false;
            }
            else {
                this.ResetActions();
                UnitActionManager.Instance.OnHeal = true;
            }
        }
    }
    public void OnDefend() {
        if (!UnitActionManager.Instance.hadDefend) {
            if (UnitActionManager.Instance.OnDefend) {
                UnitActionManager.Instance.OnDefend = false;
            }
            else {
                this.ResetActions();
                UnitActionManager.Instance.OnDefend = true;
            }
            //UnitActionManager.Instance.UnitDefend();
            UnitActions.UnitDefend();
        }
    }

    public void OnEndTurn() {
        if (!UnitActionManager.Instance.OnAttack &&
            !UnitActionManager.Instance.OnHeal &&
            !UnitActionManager.Instance.OnMove) {
            this.ToggleActionBox();

            if (this.skillShow) {
                this.ToggleSkillBox();
            }
            
            this.StartCoroutine(this.CloseUI(1.5f));

        }
    }

    private IEnumerator CloseUI(float seconds) {
        yield return new WaitForSeconds(seconds);

        UnitActionManager.Instance.OnAttack = false;
        UnitActionManager.Instance.OnHeal = false;
        UnitActionManager.Instance.OnMove = false;

        UnitActionManager.Instance.NextUnitTurn();
    }

    public void NextCharacterAvatar(Unit unit) {
        Parameters param = new Parameters();
        param.PutExtra("POS", unit.transform.position);

        EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.ON_AVATAR_CLICK, param); // remember to change the name for this event --

        this.characterAvatar.image.sprite = unit.GetComponent<SpriteRenderer>().sprite;

        // reset the values in the array

        for (int i = 0; i < this.skillSlots.Length; i++) {
            this.skillSlots[i] = false;
        }

        int options = unit.SKILLLIST.Count;

        for (int i = 0; i < options; i++) {
            this.skillSlots[i] = true;
            
        }
        this.AssignSprites(unit);

        this._unitStats.SetUnitStats(unit);

        if(unit.Type == EUnitType.Ally) {
            this.ToggleActionBox();
        }
    }

    private void AssignSprites(Unit unit) {   // also where gettng the name of the skills
        // set the sprite for the basic attack already
        this.Attacks[0].GetComponent<Image>().sprite = this.attackSprites[0];

        for (int i = 1; i < this.skillSlots.Length; i++) {
            if (this.skillSlots[i] == true) {
                this.Attacks[i].GetComponent<Image>().sprite = this.attackSprites[1];
                this.Attacks[i].GetComponentInChildren<Text>().text = unit.SKILLLIST[i];
            }
            else {
                this.Attacks[i].GetComponent<Image>().sprite = this.attackSprites[2];
            }
        }
    }

    private void AttackState(int num) {
        if (this.skillSlots[num] == true) {
            if (this.attackNum[num] == true) {   // if the same skill is selected twice, unselect it
                this.attackNum[num] = false;
                UnitActionManager.Instance.numAttack = -1;  // default value (no skill is selected)
                return;
            }

            for(int i = 0; i < this.attackNum.Length; i++) {   // reset everything
                this.attackNum[num] = false;          
            }

            this.attackNum[num] = true;

            UnitActionManager.Instance.OnAttack = true;
            UnitActionManager.Instance.numAttack = num;
        }
        else {
            UnitActionManager.Instance.OnAttack = false;
        }

    }

    public void OnBasicAttack() {
        this.AttackState(0);
    }
    public void OnSkill1() {
        this.AttackState(1);
    }
    public void OnSkill2() {
        this.AttackState(2);
    }
    public void OnSkill3() {
        this.AttackState(3);
    }
    public void OnSkill4() {
        this.AttackState(4);
    }

    public void UpdateTurnOrder(List<Unit> unitOrder) {

        for(int i = 0; i < 3; i++) {
            this.Turn[i].sprite = unitOrder[i].GetComponent<SpriteRenderer>().sprite;
        }


    }

    public void OnCancel() {
        //this.DisableSkillBoxClick();
        //this.EnableActionBoxClick();

        UnitActionManager.Instance.OnAttack = false;
    }

    public void EndScreen(int scenario) {

    }

    private void ResetActions() {
        UnitActionManager.Instance.OnAttack = false;
        UnitActionManager.Instance.OnDefend = false;
        UnitActionManager.Instance.OnHeal = false;
        UnitActionManager.Instance.OnMove = false;
    }

}
