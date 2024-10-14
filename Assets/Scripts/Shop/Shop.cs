using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public void BuyCarrot()
    {
        UnitDatabaseManager.Instance.addDatabase("CARROT");
        Debug.Log("bout");
    }
}
