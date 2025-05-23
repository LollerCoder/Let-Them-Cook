using System.Collections;
using System.Collections.Generic;
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



    [SerializeField] Animator CutsceneAnim;
    
    
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



    float ticks = 0.0f;
    float speed = 25.0f;
    bool moving = false;
    private void MOVE(Parameters param)
    {
        BattleUI.Instance.ToggleActionBox();
        BattleUI.Instance.ToggleTurnOrderUI();
        player = param.GetUnitExtra(currUNIT);
        target = param.GetUnitExtra(TARGET);
        string skillName = param.GetStringExtra(SKILLNAME,"THIS SHOULD NEVER BE USED.");
        //camera = param.GetGameObjectExtra(CAMERA);
        //ESkillType skillAnim = param.GetSkillTypeExtra(SKILLANIM);



        EnemyHP.gameObject.GetComponentInChildren<HpBar>().hpPopUp(EnemyHP, target.MAXHP, target.HP);
        EnemyHP.gameObject.GetComponentInChildren<HpBar>().setColor(EUnitType.Enemy, false);
        EnemyHP.gameObject.GetComponentInChildren<HpBar>().hpHide(EnemyHP);
        findSkillAnim(skillName);
        

        //player.gameObject.GetComponent<SpriteRenderer>().sprite;

        //playerOriginalpos = player.transform.position;
        //targetOriginalpos = target.transform.position;

        //target.transform.position = enemySpawn.transform.position;
        //player.transform.position = playerSpawn.transform.position;

        SpriteRenderer PlayerSprite = player.gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer EnemySprite = target.gameObject.GetComponent<SpriteRenderer>();
        CutscenePlayer.GetComponent<SpriteRenderer>().sprite = PlayerSprite.sprite;
        CutsceneEnemy.GetComponent<SpriteRenderer>().sprite = EnemySprite.sprite;
        moving = true;

        
    }

    private void Start()
    {

        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.CUTSCENE_PLAY, this.MOVE);

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


        //Parameters param = new Parameters();
        //param.PutExtra(UNIT, target);

        UnitActions.applySkill(target, UnitActionManager.Instance.numAttack);
        EnemyHP.gameObject.GetComponentInChildren<HpBar>().hpPopUp(EnemyHP, target.MAXHP, target.HP);
        EnemyHP.gameObject.GetComponentInChildren<HpBar>().setColor(EUnitType.Enemy, false);
       








        Debug.Log("CutsceneOuch");

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
        //player.transform.position = playerOriginalpos;
        //target.transform.position = targetOriginalpos;
        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.CUTSCENE_END);


        EnemyHP.gameObject.GetComponentInChildren<HpBar>().hpHide(EnemyHP);

        //EventBroadcaster.Instance.PostEvent(EventNames.BattleUI_Events.HIDE_HP);

        BattleUI.Instance.ToggleTurnOrderUI();
        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.NEXT_TURN);
    }

    

    private void Update()
    {
        //if (moving)
        //{
        //    //Debug.Log("MOVING");
        //    ticks += Time.deltaTime;
        //    if (ticks < 5.0f)
        //    {
        //        //movingBox.transform.Translate(Vector3.right * speed * Time.deltaTime);

        //    }
        //    else
        //    {
        //        ticks = 0.0f;
        //        moving = false;
                
                
               

        //    }
        //}
    }
}
