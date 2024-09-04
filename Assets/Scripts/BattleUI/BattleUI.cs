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

    [SerializeField]
    private List<Button> Attacks;

    private UnitStats _unitStats;

    private bool skillShow = false;
    private bool actionShow = false;

    public bool OnAttack0 = false;
    public bool OnAttack1 = false;
    public bool OnAttack2 = false;
    public bool OnAttack3 = false;
    public bool OnAttack4 = false;

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
            if (UnitActionManager.Instance.OnAttack) {
                UnitActionManager.Instance.OnAttack = false;

            }

            if (UnitActionManager.Instance.OnMove) {
                UnitActionManager.Instance.OnMove = false;
            }
        }
    }
    public void OnDefend() {
        if (!UnitActionManager.Instance.hadDefend) {
            UnitActionManager.Instance.OnDefend = !UnitActionManager.Instance.OnDefend;
            UnitActionManager.Instance.UnitDefend();

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

    public void OnBasicAttack() {
        if (this.skillSlots[0] == true) {
            if (this.OnAttack1 ||
                this.OnAttack2 ||
                this.OnAttack3 ||
                this.OnAttack4) {

                this.OnAttack0 = true;
                this.OnAttack1 = false;
                this.OnAttack2 = false;
                this.OnAttack3 = false;
                this.OnAttack4 = false;
                UnitActionManager.Instance.OnAttack = true;
            }
            else if (!this.OnAttack0) {
                this.OnAttack0 = true;
                UnitActionManager.Instance.OnAttack = true;
            }
            else {
                this.OnAttack0 = false;
                UnitActionManager.Instance.OnAttack = false;
            }

            UnitActionManager.Instance.numAttack = 0;
        }

    }
    public void OnSkill1() {
        if (this.skillSlots[1] == true) {
            if (this.OnAttack0 ||
                this.OnAttack2 ||
                this.OnAttack3 || 
                this.OnAttack4) {

                this.OnAttack0 = false;
                this.OnAttack1 = true;
                this.OnAttack2 = false;
                this.OnAttack3 = false;
                this.OnAttack4 = false;
                UnitActionManager.Instance.OnAttack = true;
            }
            else if(!this.OnAttack1){
                this.OnAttack1 = true;
                UnitActionManager.Instance.OnAttack = true;
            }
            else {
                this.OnAttack1 = false;
                UnitActionManager.Instance.OnAttack = false;
            }
            
            UnitActionManager.Instance.numAttack = 1;
        }
    }
    public void OnSkill2() {
        if (this.skillSlots[2] == true) {
            if (this.OnAttack0 ||
                this.OnAttack1 ||
                this.OnAttack3 ||
                this.OnAttack4) {

                this.OnAttack0 = false;
                this.OnAttack1 = false;
                this.OnAttack2 = true;
                this.OnAttack3 = false;
                this.OnAttack4 = false;
                UnitActionManager.Instance.OnAttack = true;
            }
            else if (!this.OnAttack2) {
                this.OnAttack2 = true;
                UnitActionManager.Instance.OnAttack = true;
            }
            else {
                this.OnAttack2 = false;
                UnitActionManager.Instance.OnAttack = false;
            }
            UnitActionManager.Instance.numAttack = 2;
        }
    }
    public void OnSkill3() {
        if (this.skillSlots[3] == true) {
            if (this.OnAttack0 ||
                this.OnAttack1 ||
                this.OnAttack2 ||
                this.OnAttack4) {

                this.OnAttack0 = false;
                this.OnAttack1 = false;
                this.OnAttack2 = false;
                this.OnAttack3 = true;
                this.OnAttack4 = false;
                UnitActionManager.Instance.OnAttack = true;
            }
            else if (!this.OnAttack3) {
                this.OnAttack3 = true;
                UnitActionManager.Instance.OnAttack = true;
            }
            else {
                this.OnAttack3 = false;
                UnitActionManager.Instance.OnAttack = false;
            }
            
            UnitActionManager.Instance.numAttack = 3;
        }
    }
    public void OnSkill4() {
        if (this.skillSlots[4] == true) {
            if (this.OnAttack0 ||
                this.OnAttack1 ||
                this.OnAttack2 ||
                this.OnAttack3) {


                this.OnAttack0 = false;
                this.OnAttack1 = false;
                this.OnAttack2 = false;
                this.OnAttack3 = false;
                this.OnAttack4 = true;
                UnitActionManager.Instance.OnAttack = true;
            }
            else if (!this.OnAttack4) {
                this.OnAttack4 = true;
                UnitActionManager.Instance.OnAttack = true;
            }
            else {
                this.OnAttack4 = false;
                UnitActionManager.Instance.OnAttack = false;
            }
            UnitActionManager.Instance.numAttack = 4;
        }
    }
    public void OnCancel() {
        //this.DisableSkillBoxClick();
        //this.EnableActionBoxClick();

        UnitActionManager.Instance.OnAttack = false;
    }

    public void EndScreen(int scenario) {

    }
}
