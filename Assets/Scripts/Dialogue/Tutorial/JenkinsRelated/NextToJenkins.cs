using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NextToJenkins : MonoBehaviour
{
    [SerializeField] UnityEvent playerEntered;
    [SerializeField] UnityEvent destroyPos;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        if (player.gameObject.transform.position.x == this.transform.position.x 
            && player.gameObject.transform.position.z == this.transform.position.z
            && UnitActionManager.Instance.OnMove == false)
        {
            playerEntered.Invoke();
            destroyPos.Invoke();
        }
    }
}
