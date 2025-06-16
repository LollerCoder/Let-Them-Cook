using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTile : Tile
{
    [SerializeField]
    private GameObject arrow_template;
    private GameObject arrow;

    private new void Start()
    {
        //Debug.Log("Hi i am here");
        EventBroadcaster.Instance.AddObserver(EventNames.Tile_Events.GOAL_ARROW_HIDE, this.HideArrow);
        EventBroadcaster.Instance.AddObserver(EventNames.Tile_Events.GOAL_ARROW_UNHIDE, this.ShowArrow);

        this.arrow = Instantiate(arrow_template, transform.position + Vector3.up * 0.25f, new Quaternion(0, 0, 0, 0));
        this.arrow.SetActive(false);

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

    public override void ApplyEffect(Unit unit)
    {
        if (unit.GetEffect("Captured_Hostage").EffectName != null ||
            unit.GetEffect("Key Holder").EffectName != null)
        {
            Parameters param = new Parameters();

            param.PutExtra("End_Text", "Hostage Freed");
            param.PutExtra("Ally_Win", false);

            param.PutExtra("Level_Complete", true);

            EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.CHECK_END_CONDITION, param);
        }
    }
}
