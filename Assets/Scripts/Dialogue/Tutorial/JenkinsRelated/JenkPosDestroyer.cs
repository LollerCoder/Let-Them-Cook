using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JenkPosDestroyer : MonoBehaviour
{
    [SerializeField] GameObject[] jenkPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void killChecks()
    {
        foreach (var j in jenkPos) Destroy(j);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
