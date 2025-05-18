using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static EventNames;
public class CutsceneManager : MonoBehaviour
{
    [SerializeField] GameObject movingBox;
    [SerializeField] GameObject enemySpawn;
    [SerializeField] GameObject playerSpawn;
    [SerializeField] SpriteRenderer CutscenePlayerSprite;
    [SerializeField] SpriteRenderer CutsceneEnemySprite;
    [SerializeField] Animator CutsceneAnim;
    Unit player;
    Unit target;
    GameObject camera;

    Vector3 playerOriginalpos;
    Vector3 targetOriginalpos;

    public const string currUNIT = "CURRUNIT";
    public const string TARGET = "TARGET";
    public const string CAMERA = "CAMERA";
    public const string SKILLANIM = "SKILLANIM";

    float ticks = 0.0f;
    float speed = 25.0f;
    bool moving = false;
    private void MOVE(Parameters param)
    {
        BattleUI.Instance.ToggleActionBox();
        BattleUI.Instance.ToggleTurnOrderUI();
        player = param.GetUnitExtra(currUNIT);
        target = param.GetUnitExtra(TARGET);
        camera = param.GetGameObjectExtra(CAMERA);
        ESkillAnim skillAnim = param.GetSkillAnimExtra(SKILLANIM);

        findSkillAnim(skillAnim);
        

        //player.gameObject.GetComponent<SpriteRenderer>().sprite;

        //playerOriginalpos = player.transform.position;
        //targetOriginalpos = target.transform.position;

        //target.transform.position = enemySpawn.transform.position;
        //player.transform.position = playerSpawn.transform.position;

        SpriteRenderer PlayerSprite = player.gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer EnemySprite = target.gameObject.GetComponent<SpriteRenderer>();
        CutscenePlayerSprite.sprite = PlayerSprite.sprite;
        CutsceneEnemySprite.sprite = EnemySprite.sprite;
        moving = true;

        CutsceneAnim.SetTrigger("Attack");
    }

    private void Start()
    {

        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.CUTSCENE_PLAY, this.MOVE);

    }

    private void findSkillAnim(ESkillAnim skillAnim)
    {
        switch (skillAnim)
        {
            case ESkillAnim.NONE:
                Debug.Log("No skill");
                break;
            case ESkillAnim.MELEE:
                Debug.Log("MELEE");
                break;
            case ESkillAnim.AOEMELEE:
                Debug.Log("Spinner");
                break;
            case ESkillAnim.RANGE:
                Debug.Log("Snipe");
                break;
            case ESkillAnim.AOERANGE:
                Debug.Log("Molotov");
                break;
            case ESkillAnim.HEAL:
                Debug.Log("Boo Boo");
                break;
        }
    }

    private void CutsceneEnd()
    {
        //player.transform.position = playerOriginalpos;
        //target.transform.position = targetOriginalpos;
        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.CUTSCENE_END);
        BattleUI.Instance.ToggleTurnOrderUI();
        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.NEXT_TURN);
    }

    private void Update()
    {
        if (moving)
        {
            Debug.Log("MOVING");
            ticks += Time.deltaTime;
            if (ticks < 5.0f)
            {
                movingBox.transform.Translate(Vector3.right * speed * Time.deltaTime);

            }
            else
            {
                ticks = 0.0f;
                moving = false;
                this.CutsceneEnd();
                
               

            }
        }
    }
}
