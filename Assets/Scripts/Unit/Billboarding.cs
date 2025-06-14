using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour {
    //private Vector3 cameraDir;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();

        //this.FixOffset();
    }

    // Update is called once per frame
    void Update() {
        // store the original position of the sprite
        Vector3 originalPosition = transform.position;

        //// get the direction from the camera
        //this.cameraDir = Camera.main.transform.position - transform.position;

        //transform.rotation = Quaternion.LookRotation(cameraDir);

        //// keep the original position to remove any displacement
        

        transform.LookAt(transform.position + Camera.main.transform.forward);
        transform.position = new Vector3(originalPosition.x,this.transform.position.y,originalPosition.z);

        
    }

    //fix the offset of the sprites of some units
    private void FixOffset()
    {
        if (gameObject.tag != "Unit" ||
            GetComponent<Potato>() != null ||
            GetComponent<Carrot>() != null)
        {
            return;
        }

        spriteRenderer.transform.localPosition += new Vector3(0, 0.3f, 0);
    }
}
