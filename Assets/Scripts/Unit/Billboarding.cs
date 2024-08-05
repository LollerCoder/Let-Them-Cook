using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour {
    private Vector3 cameraDir;

    // Update is called once per frame
    void Update() {
        this.cameraDir = Camera.main.transform.forward;
        transform.rotation = Quaternion.LookRotation(cameraDir);
    }
}
