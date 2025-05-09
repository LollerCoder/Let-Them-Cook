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
    Unit player;
    Unit target;
    GameObject camera;

    Vector3 playerOriginalpos;
    Vector3 targetOriginalpos;

    public const string currUNIT = "CURRUNIT";
    public const string TARGET = "TARGET";
    public const string CAMERA = "CAMERA";

    float ticks = 0.0f;
    float speed = 25.0f;
    bool moving = false;
    private void MOVE(Parameters param)
    {
        
        player = param.GetUnitExtra(currUNIT);
        target = param.GetUnitExtra(TARGET);
        camera = param.GetGameObjectExtra(CAMERA);

        //player.gameObject.GetComponent<SpriteRenderer>().sprite;

        //playerOriginalpos = player.transform.position;
        //targetOriginalpos = target.transform.position;

        //target.transform.position = enemySpawn.transform.position;
        //player.transform.position = playerSpawn.transform.position;

        moving = true;
    }

    private void Start()
    {

        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.CUTSCENE_PLAY, this.MOVE);

    }

    private void CutsceneEnd()
    {
        //player.transform.position = playerOriginalpos;
        //target.transform.position = targetOriginalpos;
    }

    private void Update()
    {
        if (moving)
        {
            Debug.Log("MOVING");
            ticks += Time.deltaTime;
            if (ticks < 1.0f)
            {
                movingBox.transform.Translate(Vector3.right * speed * Time.deltaTime);

            }
            else
            {
                ticks = 0.0f;
                moving = false;
                this.CutsceneEnd();
                EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.CUTSCENE_END);
                EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.NEXT_TURN);
               

            }
        }
    }
}
