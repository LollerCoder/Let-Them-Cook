using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectiveChecker : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> Objectives; // buttons 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkObjectives();
    }

    void checkObjectives()
    {
        float count = 0;
        foreach (MonoBehaviour conv in Objectives)
        {
            if(conv is IObjective)
            {
                IObjective obj = (IObjective) conv;
                if (obj.getIfCleared())
                {
                    count++;
                }
            }
            
        }
        

        if (Objectives.Count == count)
        {
            Parameters param = new Parameters();
            param.PutExtra("Level_Complete", true);
            EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.CHECK_END_CONDITION, param);
        }
        
    }
}
