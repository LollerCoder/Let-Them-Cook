using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{    //https://www.youtube.com/watch?v=rDJOilo4Xrg&t=316s

    private Vector3 previousPosition;
    private Camera cam;
    private float rotationX = 0f;

    public float movementSpeed = 5.0f; 
    
    public const string POS = "POS";

    private bool reset = false;
    private Vector3 targetPosition;
    
    void Start()
    {
        cam = Camera.main;
        EventBroadcaster.Instance.AddObserver(EventNames.BattleCamera_Events.CURRENT_FOCUS, this.ResetPosition);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleCamera_Events.ENEMY_FOCUS, this.EnemyPosition);
    }

    // Update is called once per frame
    void Update()
    {
        this.CameraMove();
        this.CameraLook();

        if (Input.GetKeyUp(KeyCode.Space) && !UnitActionManager.Instance.OnAttack) {
            this.ResetPosition();
        }
        if (Input.GetKeyUp(KeyCode.Q) && UnitActionManager.Instance.numAttack >= 0) {
            UnitAttackActions.CycleEnemy(UnitActionManager.Instance.numAttack, 0); // 0 for Q/Left
        }
        if (Input.GetKeyUp(KeyCode.E) && UnitActionManager.Instance.numAttack >= 0) {
            UnitAttackActions.CycleEnemy(UnitActionManager.Instance.numAttack, 1); // 1 for E/Right
        }
        if (this.reset) {
            this.cam.transform.position = Vector3.Lerp(this.cam.transform.position, this.targetPosition, Time.deltaTime * 5f);
            if(Vector3.Distance(this.cam.transform.position, this.targetPosition) < 0.1f) {
                this.reset = false;
                this.targetPosition = Vector3.zero;
            }
        }
    }

    private void OnDestroy()
    {
        //EventBroadcaster.Instance.RemoveObserver(EventNames.BattleCamera_Events.CURRENT_FOCUS);
        //EventBroadcaster.Instance.RemoveObserver(EventNames.BattleCamera_Events.ENEMY_FOCUS);
    }

    private void EnemyPosition(Parameters param) {
        Unit unit = param.GetUnitExtra("UNIT");
        Vector3 characterPos = unit.transform.position;

        Vector3 cameraPosition = characterPos;

        cameraPosition.y = this.cam.transform.position.y;

        this.reset = true;
        this.targetPosition = cameraPosition;
        this.targetPosition.z = cameraPosition.z - 2;

        // set the camera's x rotation to 89 instead of exactly looking at the character (90)
        Quaternion targetRotation = Quaternion.Euler(63f, 0f, 0f);
        this.cam.transform.rotation = targetRotation;

        //this.cam.transform.LookAt(characterPos);

        // override the rotationX with the new rotation so that it wont go back to the original rotation before the reset
        this.rotationX = cam.transform.localEulerAngles.x;

        this.cam.fieldOfView = 71.0f; // reset to original FoV
    }

    private void ResetPosition() {
        Vector3 characterPos = UnitActionManager.Instance.GetFirstUnit().transform.position;

        Vector3 cameraPosition = characterPos;

        cameraPosition.y = this.cam.transform.position.y;

        this.reset = true;
        this.targetPosition = cameraPosition;
        this.targetPosition.z = cameraPosition.z - 2;

        // set the camera's x rotation to 89 instead of exactly looking at the character (90)
        Quaternion targetRotation = Quaternion.Euler(63f, 0f, 0f);
        this.cam.transform.rotation = targetRotation;

        //this.cam.transform.LookAt(characterPos);

        // override the rotationX with the new rotation so that it wont go back to the original rotation before the reset
        this.rotationX = cam.transform.localEulerAngles.x;

        this.cam.fieldOfView = 71.0f; // reset to original FoV
    }

    private void CameraLook() {
        if (Input.GetMouseButtonDown(1)) {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1)) {

            Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

            //cam.transform.position = transform.position;

            rotationX += direction.y * 180;
            rotationX = Mathf.Clamp(rotationX, 36, 89);

            cam.transform.localEulerAngles = new Vector3(rotationX, cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z);
            cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);

            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);

            //cam.transform.Rotate(new Vector3(1,0,0), direction.y * 180);
            //cam.transform.Rotate(new Vector3(0,1,0), -direction.x * 180,Space.World);
            //cam.transform.Translate(new Vector3(0,0,-10));

            //previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.mouseScrollDelta.y < 0 && cam.fieldOfView < 71) {
            cam.fieldOfView += 1.0f;
        }
        if (Input.mouseScrollDelta.y > 0 && cam.fieldOfView > 29) {
            cam.fieldOfView -= 1.0f;
        }
    }
    private void CameraMove() {
        float speed = Time.deltaTime * this.movementSpeed;
        if (!this.reset) {
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

}
