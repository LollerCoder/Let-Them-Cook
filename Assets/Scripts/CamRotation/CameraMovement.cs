using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{    //https://www.youtube.com/watch?v=rDJOilo4Xrg&t=316s

  private Vector3 previousPosition;
  private Camera cam;
  private float rotationX = 0f;
  
  public float movementSpeed = 5.0f;

  public const string POS = "POS";
    void Start()
    {
        cam = Camera.main;
        EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.ON_AVATAR_CLICK, this.ResetPosition);
    }

    // Update is called once per frame
    void Update()
    {
        this.CameraMove();
        this.CameraLook();
    }

    private void ResetPosition(Parameters param) {
        Vector3 characterPos = param.GetVector3Extra(POS);

        Vector3 cameraPosition = characterPos - transform.forward * 5.0f; // change the float to set different distance

        cameraPosition.y = this.cam.transform.position.y;

        this.cam.transform.position = cameraPosition;

        this.cam.transform.LookAt(characterPos);

        // override the rotationX with the new rotation so that it wont go back to the original rotation before the reset
        this.rotationX = cam.transform.localEulerAngles.x;

        this.cam.fieldOfView = 60.0f; // reset to original FoV
    }

    private void CameraLook() {
        if (Input.GetMouseButtonDown(0)) {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0)) {

            Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

            //cam.transform.position = transform.position;

            rotationX += direction.y * 180;
            rotationX = Mathf.Clamp(rotationX, 20, 80);

            cam.transform.localEulerAngles = new Vector3(rotationX, cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z);
            cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);

            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);

            //cam.transform.Rotate(new Vector3(1,0,0), direction.y * 180);
            //cam.transform.Rotate(new Vector3(0,1,0), -direction.x * 180,Space.World);
            //cam.transform.Translate(new Vector3(0,0,-10));

            //previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.mouseScrollDelta.y < 0 && cam.fieldOfView < 91) {
            cam.fieldOfView += 0.5f;
        }
        if (Input.mouseScrollDelta.y > 0 && cam.fieldOfView > 39) {
            cam.fieldOfView -= 0.5f;
        }
    }
    private void CameraMove() {
        float speed = Time.deltaTime * this.movementSpeed;

        if (Input.GetKey(KeyCode.W)) {
            Vector3 forward = this.cam.transform.forward;
            forward.y = 0; // discard y/vertical component so that it wont go up or down
            forward.Normalize(); // normalize to maintain consistent speed
            this.cam.transform.Translate(forward * speed, Space.World);
        }
        else if (Input.GetKey(KeyCode.A)) {
            Vector3 left = this.cam.transform.right * -1.0f;
            left.y = 0;
            left.Normalize(); 
            this.cam.transform.Translate(left * speed, Space.World);
        }
        else if (Input.GetKey(KeyCode.S)) {
            Vector3 back = this.cam.transform.forward * -1.0f;
            back.y = 0;
            back.Normalize(); 
            this.cam.transform.Translate(back * speed, Space.World);
        }
        else if (Input.GetKey(KeyCode.D)) {
            Vector3 right = this.cam.transform.right;
            right.y = 0;
            right.Normalize(); 
            this.cam.transform.Translate(right * speed, Space.World);
        }
    }

}
