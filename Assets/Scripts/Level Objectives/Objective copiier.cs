using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectivecopiier : MonoBehaviour
{
    [SerializeField] private GameObject toCopy;
    [SerializeField] private GameObject copyTo;
    [SerializeField] private GameObject gameEnd;


    // Start is called before the first frame update
    void Start()
    {
       
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.CHECK_END_CONDITION, CreateObjectiveCopy);
    }

    void CreateObjectiveCopy()
    {
        RectTransform toCopyRT = toCopy.GetComponent<RectTransform>();
        RectTransform copyToRT = copyTo.GetComponent<RectTransform>();

        toCopyRT.SetParent(copyToRT,false);

        toCopyRT.anchoredPosition = new  Vector2(0, -120);
        BattleUI.Instance.TurnOffTurn();


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
