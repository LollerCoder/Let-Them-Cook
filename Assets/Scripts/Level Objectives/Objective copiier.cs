using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectivecopiier : MonoBehaviour
{
    [SerializeField] private GameObject toCopy;
    [SerializeField] private Transform copyTo;
    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.CHECK_END_CONDITION, CreateObjectiveCopy);
    }

    void CreateObjectiveCopy()
    {
        GameObject clone = Instantiate(toCopy, copyTo);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
