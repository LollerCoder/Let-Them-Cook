using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour {
    public static BattleUI Instance;

    [SerializeField]
    private List<Sprite> lids;

    [SerializeField] 
    private GameObject turnOrderCard;

    [SerializeField]
    private GameObject turnOrderField;

    [SerializeField]
    private Image AttackBox;

    [SerializeField]
    private List<Sprite> attackSprites;

    public List<Image> Turn;

    [SerializeField]
    private List<Image> BuffetContainers;

    [SerializeField]
    private List<Button> Attacks; // buttons 

    [SerializeField]
    private Text gameEndText;

    [SerializeField]
    private Text gameEndButtonText;

    [SerializeField]
    private Image gameEndScreen;

    private UnitStats _unitStats;

    private bool actionShow = false;

    public bool[] attackNum = { false, false, false, false, false }; // which skill was pressed 
    public bool[] skillSlots = { false, false, false, false, false }; // which skill is usable

    

    private void Start() {
        //this._unitStats = this.GetComponentInChildren<UnitStats>();

        //if(this._unitStats == null) {
        //    Debug.Log("ERROR: UNITSTATS CANNOT BE FOUND (BATTLEUI.CS, START() )");
        //}

        EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.TOGGLE_ACTION_BOX, this.ToggleActionBox);
        
    }
    public void ToggleActionBox() {
        this.actionShow = !this.actionShow;
        this.AttackBox.GetComponent<Animator>().SetBool("Show", this.actionShow);
    }

    public void OnEndTurn() {
        if(UnitActionManager.Instance.GetFirstUnit().Type == EUnitType.Ally) {
            this.ToggleActionBox();
        }

        for(int i = 0; i < this.attackNum.Count(); i++) {
            this.attackNum[i] = false;
        }
        UnitActionManager.Instance.ResetCurrentUnit();
        this.StartCoroutine(this.CloseUI(0.2f));
    }

    private IEnumerator CloseUI(float seconds) {
        yield return new WaitForSeconds(seconds);
        UnitActions.HideInRangeHPBar(UnitActionManager.Instance.numAttack);

        UnitActionManager.Instance.OnAttack = false;
        UnitActionManager.Instance.OnMove = false;
 
        UnitActionManager.Instance.UnitTurn();
    }

    public void NextUnitSkills(Unit unit) {
        

        // reset the values in the array

        for (int i = 0; i < this.skillSlots.Length; i++) {
            this.skillSlots[i] = false;
        }

        int options = unit.SKILLLIST.Count;

        for (int i = 0; i < options; i++) {
            this.skillSlots[i] = true;
            
        }
        this.AssignSprites(unit);

        //this._unitStats.SetUnitStats(unit);
            
    }

    private void AssignSprites(Unit unit) {   // also where gettng the name of the skills
        // set the sprite for the basic attack already
        this.Attacks[0].GetComponent<Image>().sprite = this.attackSprites[0]; // basic attack

        for (int i = 1; i < this.skillSlots.Length; i++) {
            if (this.skillSlots[i] == true) {
                this.Attacks[i].GetComponent<Image>().sprite = this.attackSprites[1]; // skills

                //this.Attacks[i].transform.Find("Nameplate").transform.Find("Text (Legacy)").GetComponentInChildren<Text>().text = unit.SKILLLIST[i];
                this.Attacks[i].GetComponentInChildren<Text>().text = unit.SKILLLIST[i];
               
            }
            else {
                this.Attacks[i].GetComponent<Image>().sprite = this.attackSprites[2]; // none
                this.Attacks[i].GetComponentInChildren<Text>().text = "";
            }
        }
    }
    public void UpdateButtonState(int i, bool active) {
        if (!active) {
            this.skillSlots[i] = false;

            this.Attacks[i].GetComponentInParent<Image>().enabled = false;

            Color color = this.Attacks[i].GetComponent<Image>().color;

            this.BuffetContainers[i].sprite = lids[0];

            color.r = 0.3f;
            color.g = 0.3f;
            color.b = 0.3f;

            this.Attacks[i].GetComponent<Image>().color = color;
        }
        else{
            this.skillSlots[i] = true;

            this.Attacks[i].GetComponentInParent<Image>().enabled = true;

            this.BuffetContainers[i].sprite = lids[1];

            Color color = this.Attacks[i].GetComponent<Image>().color;

            color.r = 1f;
            color.g = 1f;
            color.b = 1f;

            this.Attacks[i].GetComponent<Image>().color = color;
        }
    }

    private void AttackState(int num) {
        UnitAttackActions.EnemyListed = false;
        if(num < 0) {
            return;
        }

        if (this.skillSlots[num] == true) {
            if (this.attackNum[num] == true) {   // if the same skill is selected twice, unselect it
                this.attackNum[num] = false;
                UnitActionManager.Instance.OnAttack = false;
                UnitActionManager.Instance.numAttack = -1;  // default value (no skill is selected)

                if (num != 0) {
                    this.Attacks[num].GetComponent<Image>().sprite = this.attackSprites[1]; // skills
                }
                else {
                    this.Attacks[num].GetComponent<Image>().sprite = this.attackSprites[0]; // basic attack
                }

                UnitActions.HideInRangeHPBar(num);
                return;
            }

            for(int i = 0; i < this.attackNum.Length; i++) {   // reset everything
                this.attackNum[i] = false;

                if (i != 0) {
                    this.Attacks[i].GetComponent<Image>().sprite = this.attackSprites[1]; // skills
                }
                else {
                    this.Attacks[i].GetComponent<Image>().sprite = this.attackSprites[0]; // basic attack
                }

            }

            if(num != 0) {
                this.Attacks[num].GetComponent<Image>().sprite = this.attackSprites[4]; // skills
            }
            else {
                this.Attacks[num].GetComponent<Image>().sprite = this.attackSprites[3];// basic
            }

            this.attackNum[num] = true;
            UnitActionManager.Instance.OnAttack = true;
            UnitActionManager.Instance.numAttack = num;

            UnitAttackActions.CycleEnemy(num, 0);


        }
        else {
            UnitActionManager.Instance.OnAttack = false;
        }
    }

    public void ResetButtonState(int i) {
        this.AttackState(i);
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
        int max_queue= 9;
        int curr_count = 0; //for making sure portraits are slapped onto the queue
        int curr_created = 0; //counting how many veggies are made currently
        
        while(this.Turn.Count < max_queue)
        {
           GameObject newCard = Instantiate(turnOrderCard);
           this.Turn.Add(newCard.transform.Find("1").GetComponent<Image>());
           newCard.transform.SetParent(this.turnOrderField.transform, false);

        }

        EventBroadcaster.Instance.PostEvent(EventNames.BattleCamera_Events.CURRENT_FOCUS);

        do
        {
            Debug.Log(curr_created);
            
            if (curr_created < unitOrder.Count)
            {
                this.Turn[curr_count].sprite = unitOrder[curr_created].GetComponent<SpriteRenderer>().sprite;
                curr_created++;
            }
            else
            {
                curr_created = 0;
                this.Turn[curr_count].sprite = unitOrder[curr_created].GetComponent<SpriteRenderer>().sprite;
                curr_created++;
            }
            curr_count++;

        } while(curr_count < max_queue);
        


    }

    public void OnCancel() {
        //this.DisableSkillBoxClick();
        //this.EnableActionBoxClick();

        UnitActionManager.Instance.OnAttack = false;
    }

    public void EndScreen(EUnitType type) {
        switch (type) {
            case EUnitType.Ally:
                this.gameEndText.text = "Stage Complete";
                this.gameEndButtonText.text = "Continue";
                break;
            case EUnitType.Enemy:
                this.gameEndText.text = "Game Over";
                this.gameEndButtonText.text = "Restock";
                break;
        }
    }
    public void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != null) {
            Destroy(this.gameObject);
        }
    }
}
