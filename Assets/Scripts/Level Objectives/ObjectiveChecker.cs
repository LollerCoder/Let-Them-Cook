using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectiveChecker : MonoBehaviour
{
    [SerializeField] private List<Objective> Objectives; // buttons 
    [SerializeField] private int totalLevelObjectives = 0;
    private bool sent = false;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Objective conv in Objectives)
        {
            if (!conv.getIfOptional())
            {
               totalLevelObjectives++;
            }


        }
    }

    // Update is called once per frame
    void Update()
    {
        checkObjectives();
    }

    void checkObjectives()
    {
        float count = 0;
        foreach (Objective conv in Objectives)
        {
            if (conv.getIfCleared() && !conv.getIfOptional())
            {
                count++;
            }
            
            
        }
        

        if (totalLevelObjectives == count)
        {
            if(!sent)
            {
                Parameters param = new Parameters();
                param.PutExtra("Level_Complete", true);
                
                EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.CHECK_END_CONDITION, param);
                EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.CHECK_END_CONDITION);
                sent = true;
            }
           
        }
        
    }
}
