using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour {
    private Vector3 cameraDir;

    // Update is called once per frame
    void Update() {
        // store the original position of the sprite
        Vector3 originalPosition = transform.position;

        // get the direction from the camera
        this.cameraDir = Camera.main.transform.position - transform.position;
  
        transform.rotation = Quaternion.LookRotation(cameraDir);

        // keep the original position to remove any displacement
        transform.position = originalPosition;


    }
}
