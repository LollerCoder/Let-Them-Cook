using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{
    [SerializeField] protected Toggle toggle;
    [SerializeField] protected string toggleMessage;
    [SerializeField] protected bool optional = false;

    protected Text toggleText;
    protected bool cleared = false;

    // Start is called before the first frame update
    protected void Start()
    {
        this.toggle.interactable = false;
        this.toggle.isOn = false;
        this.toggleText = toggle.GetComponentInChildren<Text>();
        this.toggleText.text = toggleMessage;

        if (optional)
        {
            Image toggleBG = toggle.GetComponentInChildren<Image>();
            toggleBG.color = Color.green;
            //Vector3 pos = this.gameObject.transform.localPosition;
            //pos.x += 10;
            //this.gameObject.transform.localPosition = pos;


            this.toggleText.text = "(Optional) " + toggleMessage;
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        //this.clearCondition();
    }



    protected virtual void clearCondition()
    {
        //Unit someBS = UnitActionManager.Instance.GetFirstUnit() as Unit;
        //GameObject toCheck = someBS.gameObject;
        //if (toCheck.transform.position.x == 3 && toCheck.transform.position.z == 4)
        //{
        //    toggle.isOn = true;
        //    cleared = true;
        //}

    }
    protected virtual void onConditionClear()
    {

    }

    public virtual bool getIfCleared()
    {
        return cleared;
    }

    public virtual bool getIfOptional()
    {
        return optional;
    }
}
