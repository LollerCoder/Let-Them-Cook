using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookedMover : MonoBehaviour
{
    private Vector3 _originalPos;
    public Vector3 TargetPos;

    public void GoHome()
    {
        this.transform.position = this._originalPos;
        this.TargetPos = this._originalPos;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.TargetPos = this.gameObject.transform.position;
        this._originalPos = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = Vector3.MoveTowards(
            this.gameObject.transform.position,
            this.TargetPos,
            5 * Time.deltaTime
            );
    }
}
