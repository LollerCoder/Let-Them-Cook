using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMarkerHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObj;

    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.HostageRescue_Events.GOAL_ARROW_UNHIDE, this.ShowArrow);

        //hiding the arrow
        EventBroadcaster.Instance.AddObserver(EventNames.HostageRescue_Events.GOAL_ARROW_HIDE, this.HideArrow);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.CUTSCENE_PLAY, this.HideArrow);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.CHECK_END_CONDITION, this.HideArrowP);

        EventBroadcaster.Instance.AddObserver(EventNames.HostageRescue_Events.ARROW_SHOWED, this.AddTarget);

        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObj != null) this.transform.LookAt(targetObj.transform.position);
    }

    public void AddTarget()
    {
        targetObj = GameObject.FindGameObjectWithTag("Goal");
    }

    public void ShowArrow()
    {
        this.gameObject.SetActive(true);
    }

    public void HideArrow()
    {
        this.gameObject.SetActive(false);
    }
    public void HideArrowP(Parameters param)
    {
        this.gameObject.SetActive(false);
    }
}
