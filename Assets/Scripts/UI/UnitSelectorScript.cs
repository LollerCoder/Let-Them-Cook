using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UnitSelectorScript : MonoBehaviour
{
    GameObject unitPanel;

    public Button Tbtn;
    void Start()
    {
        /*Pause thegame and choose your units first*/
        Time.timeScale = 0.0f;

        unitPanel = this.gameObject;

        unitPanel.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void buttonReader()
    {
        UnitManager.Instance.bTomato = true;
        Debug.Log("Tomato selected!");

    }

    public void startGame()
    {
        Time.timeScale = 1.0f;

        unitPanel.SetActive(false);

        UnitManager.Instance.manageParty();
    }
}
