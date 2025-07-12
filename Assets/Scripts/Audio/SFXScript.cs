using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SFXScript : MonoBehaviour
{
    private void Play(string name) {
        SFXManager.Instance.Play("Steps");
    }
    //SPRINGS
    Vector3 tpLoc = Vector3.zero;
    private void setTP(Parameters param)
    {
        Debug.Log("TPLOCATION SET");
        tpLoc = param.GetVector3Extra("pos");
        if (tpLoc == Vector3.zero)
        {
            Debug.Log("NO LOCATION??!");
        }
        
    }

    private void TP()
    {
        if (tpLoc != Vector3.zero)
        {
            Debug.Log("Teleport");
            this.gameObject.GetComponentInParent<Unit>().gameObject.transform.position = tpLoc;
            EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.LAUNCH);
        }
    }

    //SPRINGS

    private void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.SPRING, this.setTP);
    }
}
