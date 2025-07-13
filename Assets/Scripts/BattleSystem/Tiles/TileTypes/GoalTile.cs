using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTile : Tile
{
    [Header("Arrow")]
    [SerializeField]
    private GameObject arrow_template;
    private GameObject arrow;

    [Header("Flags")]
    [SerializeField]
    private bool goalMet = false;

    private new void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.Level3_Objectives.KEY_FOUND, this.GoalComplete);

        if (this.arrow_template != null)
        {
            Debug.Log("Arrow added!");
            //arrow observer
            EventBroadcaster.Instance.AddObserver(EventNames.Tile_Events.GOAL_ARROW_HIDE, this.HideArrow);
            EventBroadcaster.Instance.AddObserver(EventNames.Tile_Events.GOAL_ARROW_UNHIDE, this.ShowArrow);

            this.arrow = Instantiate(arrow_template, transform.position + Vector3.up * 0.25f, new Quaternion(0, 0, 0, 0));
            this.arrow.SetActive(false);
        }
        else
        {
            Debug.Log("Arrow not added!");
        }

        base.Start();
    }

    public void ShowArrow()
    {
        this.arrow.SetActive(true);
    }

    public void HideArrow()
    {
        this.arrow.SetActive(false);
    }

    public void GoalComplete()
    {
        this.goalMet = true;
    }

    public override void ApplyEffect(Unit unit)
    {
        Parameters param = new Parameters();

        if (unit.GetEffect("Captured_Hostage") != null)
        {
            param.PutExtra("End_Text", "Hostage Freed");
            param.PutExtra("Ally_Win", false);

            param.PutExtra("Level_Complete", true);

            //EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.CHECK_END_CONDITION, param);
            //EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.CHECK_END_CONDITION); //objective copier just needs it to be an empty call
            EventBroadcaster.Instance.PostEvent(EventNames.Level3_Objectives.ESCAPED);
        }

        if (unit.GetEffect("Key Holder") != null)
        {
            param.PutExtra("End_Text", "Key Obtained!");
            param.PutExtra("Ally_Win", false);

            param.PutExtra("Level_Complete", true);

            //EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.CHECK_END_CONDITION, param);
            //EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.CHECK_END_CONDITION);//objective copier just needs it to be an empty call
            EventBroadcaster.Instance.PostEvent(EventNames.Level3_Objectives.ESCAPED);
        }
    }
}
