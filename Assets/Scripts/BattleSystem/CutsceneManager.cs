using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UIElements;
using static EventNames;
public class CutsceneManager : MonoBehaviour
{
    [Header("Spawns")]
    [SerializeField] GameObject enemySpawn;
    [SerializeField] GameObject playerSpawn;
    [Header("OriginalCutsceneStuff")]
    [SerializeField] GameObject CutscenePlayer;
    [SerializeField] GameObject CutsceneEnemy;
    [Header("HPBars")]
    [SerializeField] GameObject PlayerHP;
    [SerializeField] GameObject EnemyHP;


    [Header("Animator")]
    [SerializeField] Animator CutsceneAnim;
    [Header("ParticeSystems")]
    [SerializeField] ParticleSystem HealParticle;
    [Header("Dummies")]
    [SerializeField] GameObject[] Dummies;
    private List<Unit> DummiesData = new List<Unit>();



    
    Unit player;
    Unit target;
    //GameObject camera;

    Vector3 playerOriginalpos;
    Vector3 targetOriginalpos;


    public const string UNIT = "UNIT";

    public const string currUNIT = "CURRUNIT";
    public const string TARGET = "TARGET";
    public const string CAMERA = "CAMERA";
    //public const string SKILLANIM = "SKILLANIM";
    public const string SKILLNAME = "SKILLNAME";



   // float ticks = 0.0f;
    //float speed = 25.0f;
   // bool moving = false;
    private void MOVE(Parameters param)
    {
        BattleUI.Instance.ToggleActionBox();
        BattleUI.Instance.ToggleTurnOrderUI();
        player = param.GetUnitExtra(currUNIT);
        target = param.GetUnitExtra(TARGET);
        string skillName = param.GetStringExtra(SKILLNAME,"THIS SHOULD NEVER BE USED.");

        SpriteRenderer PlayerSprite = player.gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer EnemySprite = target.gameObject.GetComponent<SpriteRenderer>();
        CutscenePlayer.GetComponent<SpriteRenderer>().sprite = PlayerSprite.sprite;
        CutsceneEnemy.GetComponent<SpriteRenderer>().sprite = EnemySprite.sprite;


        EnemyHP.gameObject.GetComponentInChildren<HpBar>().hpPopUp(EnemyHP, target.MAXHP, target.HP);
        EnemyHP.gameObject.GetComponentInChildren<HpBar>().setColor(EUnitType.Enemy, false);
        EnemyHP.gameObject.GetComponentInChildren<HpBar>().hpHide(EnemyHP);
        findSkillAnim(skillName);
        


        
      
        
    }
    private void AOEMOVE(Parameters param)
    {
        //Debug.Log("AOE DUMMIES");
        int dummycount = param.GetIntExtra("DummyCount",0);
        Debug.Log("AOE DUMMIES: " + dummycount);
        //Debug.Log("Dummies were: " + dummycount);
        for (int i = 0; i < dummycount; i++)
        {
            Dummies[i].SetActive(true);
            SpriteRenderer DummySprite = Dummies[i].gameObject.GetComponent<SpriteRenderer>();
            Unit dummySent = param.GetUnitExtra("Dummy" + i);
            
            DummiesData.Add(dummySent);

            DummySprite.sprite = dummySent.gameObject.GetComponent<SpriteRenderer>().sprite;
            HpBar DummyHp = Dummies[i].gameObject.GetComponentInChildren<HpBar>(true);
           
            DummyHp.hpPopUp(DummyHp.gameObject, dummySent.MAXHP, dummySent.HP);
            if (dummySent.Type == EUnitType.Enemy)
            {
                DummyHp.setColor(EUnitType.Enemy, false);
            }
            else
            {
                DummyHp.setColor(EUnitType.Ally, false);
            }
            DummyHp.hpHide(DummyHp.gameObject);
            
            //if(DummyHp == null) {
            //    Debug.Log("Dummy hp NULL");
            //}

            //Debug.Log("MaxHp" + dummySent.MAXHP);
            //Debug.Log("Hp" + dummySent.HP);



        }







    }

    private void Start()
    {

        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.CUTSCENE_PLAY, this.MOVE);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.CUTSCENE_AOE, this.AOEMOVE);

    }

    private void findSkillAnim(string name)
    {
        ESkillType skillAnim = SkillDatabase.Instance.findSkill(name).SKILLTYPE;
        switch (skillAnim)
        {
            case ESkillType.NONE:
                Debug.Log("No skill");
                 
                break;
            case ESkillType.BASIC:
                Debug.Log("MELEE");
                CutsceneAnim.SetTrigger("Attack");
                break;
            case ESkillType.BUFFDEBUFF:
                Debug.Log("BuffDebuff");
                
                break;
            case ESkillType.HEAL:
                Debug.Log("BOO BOO");
                EnemyHP.gameObject.GetComponentInChildren<HpBar>().hpPopUp(EnemyHP, target.MAXHP, target.HP);
                EnemyHP.gameObject.GetComponentInChildren<HpBar>().setColor(EUnitType.Ally, false);
                EnemyHP.gameObject.GetComponentInChildren<HpBar>().hpHide(EnemyHP);
                CutsceneAnim.SetTrigger("Heal");
                break;
            case ESkillType.DEFEND:
                Debug.Log("Parry");
                break;
            case ESkillType.AOE:
                Debug.Log("SPUN");
                CutsceneAnim.SetTrigger("Spin");
                break;
        }
    }

    private void CutsceneTakeDamage()
    {

        
        
        UnitActions.applySkill(target, UnitActionManager.Instance.numAttack);
        EnemyHP.gameObject.GetComponentInChildren<HpBar>().hpPopUp(EnemyHP, target.MAXHP, target.HP);
        EnemyHP.gameObject.GetComponentInChildren<HpBar>().setColor(EUnitType.Enemy, false);


        if (DummiesData.Count() != 0)
        {
            for (int i = 0; i < Dummies.Count(); i++)
            {
                if (Dummies[i].activeSelf)
                {
                    UnitActions.applySkill(DummiesData[i], UnitActionManager.Instance.numAttack);
                    HpBar DummyHp = Dummies[i].gameObject.GetComponentInChildren<HpBar>(true);
                    Unit dummySent = DummiesData[i];

                    DummyHp.hpPopUp(DummyHp.gameObject, dummySent.MAXHP, dummySent.HP);
                    //DummyHp.hpHide(DummyHp.gameObject);
                    DummyHp.setColor(EUnitType.Enemy, false);
                }




            }
        }











    }

    IEnumerator CutsceneDeadCheck()
    {
        if (target.HP <= 0)
        {
            yield return new WaitForSeconds(1);
            CutsceneAnim.SetTrigger("DedEnemy");
        }
        else
        {
            yield return new WaitForSeconds(1);
            this.CutsceneEnd();
        }   
    }

    private void CutsceneEnd()
    {
    
        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.CUTSCENE_END);


        EnemyHP.gameObject.GetComponentInChildren<HpBar>().hpHide(EnemyHP);

        //EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.HIDE_HP);

        for (int i = 0; i < Dummies.Count(); i++)
        {
            if (Dummies[i].activeSelf)
            {
                HpBar DummyHp = Dummies[i].gameObject.GetComponentInChildren<HpBar>(true);
                //Unit dummySent = DummiesData[i];

                //dummySent.gameObject.SetActive(false);
                
                //DummyHp.hpPopUp(DummyHp.gameObject, dummySent.MAXHP, dummySent.HP);
                DummyHp.hpHide(DummyHp.gameObject);
                Dummies[i].gameObject.SetActive(false);
                //DummyHp.setColor(EUnitType.Enemy, false);
            }




        }
        DummiesData.Clear();

        BattleUI.Instance.ToggleTurnOrderUI();
        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.NEXT_TURN);
    }

    private void HealingParticles()
    {
        HealParticle.Play();
    }

    private void Update()
    {
        
    }
}
