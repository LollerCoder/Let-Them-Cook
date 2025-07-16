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
    [SerializeField] GameObject cutsceneIsland;
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
    [SerializeField] GameObject[] PowFxSpin;

    [Header("Projectile")]
    [SerializeField] SpriteRenderer ProjectileSpriteRenderer;
    [SerializeField] Sprite FoilAxeSprite;
    [SerializeField] Sprite RottenSprite;


    
    private Dictionary<Unit, Vector3> DummiesData = new Dictionary<Unit, Vector3>();



    
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



    private void SETUP(Parameters param)
    {
        CameraMovement.inCutscene = true;
        BattleUI.Instance.ToggleTurnOrderUI();
        player = param.GetUnitExtra(currUNIT);
        target = param.GetUnitExtra(TARGET);

        if (player.Type == EUnitType.Ally) {
            BattleUI.Instance.ToggleActionBox();
        }

        string numTARGETS = param.GetStringExtra("TARGETS", "THIS SHOULD NEVER BE USED.");

        string skillName = param.GetStringExtra(SKILLNAME, "THIS SHOULD NEVER BE USED.");

        switch (numTARGETS)
        {
            case "SINGLE":
                this.SINGLE();
                break;
            case "MULTIPLE":
                this.MULTIPLE(param);
                break;
        }
        findSkillAnim(skillName);
    }


    private void SINGLE()
    {
        if(player.Type != EUnitType.Ally)
        {
            this.cutsceneIsland.transform.localRotation = Quaternion.Euler(5f, 180f, 0f);
            this.cutsceneIsland.transform.localPosition = this.cutsceneIsland.transform.localPosition + new Vector3(6.0f, 0.0f, 0.0f);
        }

        SpriteRenderer PlayerSprite = player.spriteRenderer;
        SpriteRenderer EnemySprite = target.spriteRenderer;
        CutscenePlayer.GetComponent<SpriteRenderer>().sprite = PlayerSprite.sprite;
        CutsceneEnemy.GetComponent<SpriteRenderer>().sprite = EnemySprite.sprite;


        EnemyHP.gameObject.GetComponentInChildren<HpBar>().hpPopUp(EnemyHP, target.MAXHP, target.HP);
        EnemyHP.gameObject.GetComponentInChildren<HpBar>().setColor(EUnitType.Enemy, false);
        EnemyHP.gameObject.GetComponentInChildren<HpBar>().hpHide(EnemyHP);
       
    }
    private void MULTIPLE(Parameters param)
    {
        CutsceneEnemy.SetActive(false);

        //Setting the attacking unit
        SpriteRenderer PlayerSprite = player.spriteRenderer;
        CutscenePlayer.GetComponent<SpriteRenderer>().sprite = PlayerSprite.sprite;

        //Debug.Log("AOE DUMMIES");
        int dummycount = param.GetIntExtra("DummyCount",0);
        Debug.Log("AOE DUMMIES: " + dummycount);
        //Debug.Log("Dummies were: " + dummycount);


        //This should just log the stuff
        for (int i = 0; i < dummycount; i++)
        {
            //Dummies[i].SetActive(true);
            //SpriteRenderer DummySprite = Dummies[i].gameObject.GetComponent<SpriteRenderer>();
            Unit dummySent = param.GetUnitExtra("Dummy" + i);
            Vector3 dir = param.GetVector3Extra("Direction" + i);

            


            DummiesData.Add(dummySent,dir);

            //DummySprite.sprite = dummySent.gameObject.GetComponent<SpriteRenderer>().sprite;
            //HpBar DummyHp = Dummies[i].gameObject.GetComponentInChildren<HpBar>(true);
           
            //DummyHp.hpPopUp(DummyHp.gameObject, dummySent.MAXHP, dummySent.HP);
            //if (dummySent.Type == EUnitType.Enemy)
            //{
            //    DummyHp.setColor(EUnitType.Enemy, false);
            //}
            //else
            //{
            //    DummyHp.setColor(EUnitType.Ally, false);
            //}
            //DummyHp.hpHide(DummyHp.gameObject);
            
            //if(DummyHp == null) {
            //    Debug.Log("Dummy hp NULL");
            //}

            //Debug.Log("MaxHp" + dummySent.MAXHP);
            //Debug.Log("Hp" + dummySent.HP);



        }

        //this will set the set them up to be seen
        foreach (var Dummy in DummiesData)
        {
            Sprite _sprite = Dummy.Key.gameObject.GetComponentsInChildren<SpriteRenderer>().FirstOrDefault(sr => sr.gameObject.CompareTag("Unit Sprite")).sprite;

            if (Dummy.Value == Vector3.left)
            {
                CutsceneEnemy.SetActive(true);
                Debug.Log(Dummy.Key.gameObject.GetComponentInChildren<SpriteRenderer>().sprite.name);
                CutsceneEnemy.GetComponentInChildren<SpriteRenderer>().sprite = _sprite;
                PowFxSpin[3].SetActive(true);
            }
            if (Dummy.Value == Vector3.right)
            {
               
                Dummies[0].SetActive(true);
                Dummies[0].GetComponentInChildren<SpriteRenderer>().sprite = _sprite;
                PowFxSpin[1].SetActive(true);
            }
            if (Dummy.Value == Vector3.forward)
            {
                Dummies[1].SetActive(true);
                Dummies[1].GetComponentInChildren<SpriteRenderer>().sprite = _sprite;
                PowFxSpin[0].SetActive(true);
            }
            if (Dummy.Value == Vector3.back)
            {
                Dummies[2].SetActive(true);
                Dummies[2].GetComponentInChildren<SpriteRenderer>().sprite = _sprite;
                PowFxSpin[2].SetActive(true);
            }



            //Hpbar show
            HpBar DummyHp = Dummy.Key.gameObject.GetComponentInChildren<HpBar>(true);

            DummyHp.hpPopUp(DummyHp.gameObject, Dummy.Key.MAXHP, Dummy.Key.HP);
            if (Dummy.Key.Type == EUnitType.Enemy)
            {
                DummyHp.setColor(EUnitType.Enemy, false);
            }
            else
            {
                DummyHp.setColor(EUnitType.Ally, false);
            }
            DummyHp.hpHide(DummyHp.gameObject);

            
        }


    }

    private SpriteRenderer GetRendererInDummy(SpriteRenderer[] _sr)
    {
        return _sr.FirstOrDefault(sr => sr.gameObject.CompareTag("Unit Sprite"));
    }

    private void Start()
    {

        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.CUTSCENE_PLAY, this.SETUP);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.CUTSCENE_RESET, this.ResetCutscene);
        //EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.CUTSCENE_AOE, this.MULTIPLE);

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
                //Debug.Log("BOO BOO");
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
            case ESkillType.RANGE:
                this.ChangeProjectileSprite(name);
                CutsceneAnim.SetTrigger("Throw");
                break;
        }
    }

    private void ChangeProjectileSprite(string name)
    {
        switch (name)
        {
            case "Rotten":
                this.ProjectileSpriteRenderer.sprite = this.RottenSprite;
                break;
            case "Foil Throw":
                this.ProjectileSpriteRenderer.sprite = this.FoilAxeSprite;
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
            foreach(var Dummy in DummiesData)
            {
                if (Dummy.Value == Vector3.left)
                {

                    //CutscenePlayer.GetComponent<SpriteRenderer>().sprite = pl.gameObject.GetComponent<SpriteRenderer>().sprite;
                    UnitActions.applySkill(Dummy.Key, UnitActionManager.Instance.numAttack);
                    HpBar DummyHp = EnemyHP.gameObject.GetComponentInChildren<HpBar>(true);
                    Unit dummySent = Dummy.Key;

                    DummyHp.hpPopUp(DummyHp.gameObject, dummySent.MAXHP, dummySent.HP);
                    //DummyHp.hpHide(DummyHp.gameObject);
                    DummyHp.setColor(EUnitType.Enemy, false);
                }
                if (Dummy.Value == Vector3.right)
                {
                    UnitActions.applySkill(Dummy.Key, UnitActionManager.Instance.numAttack);
                    HpBar DummyHp = Dummies[0].gameObject.GetComponentInChildren<HpBar>(true);
                    Unit dummySent = Dummy.Key;

                    DummyHp.hpPopUp(DummyHp.gameObject, dummySent.MAXHP, dummySent.HP);
                    //DummyHp.hpHide(DummyHp.gameObject);
                    DummyHp.setColor(EUnitType.Enemy, false);
                }
                if (Dummy.Value == Vector3.forward)
                {
                    UnitActions.applySkill(Dummy.Key, UnitActionManager.Instance.numAttack);
                    HpBar DummyHp = Dummies[1].gameObject.GetComponentInChildren<HpBar>(true);
                    Unit dummySent = Dummy.Key;

                    DummyHp.hpPopUp(DummyHp.gameObject, dummySent.MAXHP, dummySent.HP);
                    //DummyHp.hpHide(DummyHp.gameObject);
                    DummyHp.setColor(EUnitType.Enemy, false);
                }
                if (Dummy.Value == Vector3.back)
                {
                    UnitActions.applySkill(Dummy.Key, UnitActionManager.Instance.numAttack);
                    HpBar DummyHp = Dummies[2].gameObject.GetComponentInChildren<HpBar>(true);
                    Unit dummySent = Dummy.Key;

                    DummyHp.hpPopUp(DummyHp.gameObject, dummySent.MAXHP, dummySent.HP);
                    //DummyHp.hpHide(DummyHp.gameObject);
                    DummyHp.setColor(EUnitType.Enemy, false);
                }
                if (Dummy.Key)
                {
                    
                    //UnitActions.applySkill(DummiesData[i], UnitActionManager.Instance.numAttack);
                    //HpBar DummyHp = Dummies[i].gameObject.GetComponentInChildren<HpBar>(true);
                    //Unit dummySent = DummiesData[i];

                    //DummyHp.hpPopUp(DummyHp.gameObject, dummySent.MAXHP, dummySent.HP);
                    ////DummyHp.hpHide(DummyHp.gameObject);
                    //DummyHp.setColor(EUnitType.Enemy, false);
                }
            }
            //for (int i = 0; i < Dummies.Count(); i++)
            //{
            //    if (Dummies[i].activeSelf)
            //    {
            //        UnitActions.applySkill(DummiesData[i], UnitActionManager.Instance.numAttack);
            //        HpBar DummyHp = Dummies[i].gameObject.GetComponentInChildren<HpBar>(true);
            //        Unit dummySent = DummiesData[i];

            //        DummyHp.hpPopUp(DummyHp.gameObject, dummySent.MAXHP, dummySent.HP);
            //        //DummyHp.hpHide(DummyHp.gameObject);
            //        DummyHp.setColor(EUnitType.Enemy, false);
            //    }




            //}
        }











    }

    IEnumerator CutsceneDeadCheck()
    {
        //Debug.Log("Checcking dead");
        //this.ResetCutscene();
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
    private void ResetCutscene()
    {
        CutsceneEnemy.SetActive(true);
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
                PowFxSpin[i].gameObject.SetActive(false);
                //DummyHp.setColor(EUnitType.Enemy, false);
            }




        }
        DummiesData.Clear();
        
        this.cutsceneIsland.transform.localPosition = new Vector3(-3f, -1.738886f, 7.414598f);
        this.cutsceneIsland.transform.localRotation = Quaternion.Euler(-10f, 0f, 0f);
    }

    private void CutsceneEnd()
    {
        StartCoroutine(CutsceneCoroutine());
    }

    private IEnumerator CutsceneCoroutine()
    {
        //Debug.Log("Cutscene is Ending!");
        CameraMovement.inCutscene = false;



        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.CUTSCENE_END);
        


        BattleUI.Instance.ToggleTurnOrderUI();
        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.NEXT_TURN);
        yield return new WaitForSeconds(0.8f);
        this.ResetCutscene();
    }


    private void HealingParticles()
    {
        HealParticle.Play();
    }
    private void PlaySFX(string name) {
        SFXManager.Instance.Play(name);
    }
    private void Update()
    {
        
    }
}
