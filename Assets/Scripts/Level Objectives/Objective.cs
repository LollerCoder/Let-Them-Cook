using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour, IObjective
{
    [SerializeField] Toggle toggle;
    [SerializeField] private string toggleMessage;
    private Text toggleText;
    private bool cleared = false;
    // Start is called before the first frame update
    void Start()
    {
        this.toggle.interactable = false;
        this.toggle.isOn = false;
        this.toggleText = toggle.GetComponentInChildren<Text>();
        this.toggleText.text = toggleMessage;
    }

    // Update is called once per frame
    void Update()
    {
        this.clearCondition();
    }

    
    
    public void clearCondition()
    {
        Unit someBS = UnitActionManager.Instance.GetFirstUnit() as Unit;
        GameObject toCheck = someBS.gameObject;
        if (toCheck.transform.position.x == 3 && toCheck.transform.position.z == 4)
        {
            toggle.isOn = true;
            cleared = true;
        }

    }
    public void onConditionClear()
    {
        
    }

    public bool getIfCleared()
    {
        return cleared;
    }
}
