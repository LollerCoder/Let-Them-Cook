using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class CameraMovement : MonoBehaviour
{    //https://www.youtube.com/watch?v=rDJOilo4Xrg&t=316s

    [Range(1, 200)]
    public int cameraSensitivity = 100;

    private Vector3 previousPosition;
    private Camera cam;
    private float rotationX = 0f;

    public float Speed = 7.0f;
    private float movementSpeed;
    private float shiftSpeed;
    
    public const string POS = "POS";

    private bool reset = false;
    [SerializeField]
    private Vector3 targetPosition;

    private Vector3 previousCamPos;
    private Quaternion previousCamRot;

    [SerializeField, Range(4, 12)]
    private float heightOffset = 8; // height above the tiles

    public static bool inCutscene = false;
    void Start()
    {
        cam = Camera.main;
        EventBroadcaster.Instance.AddObserver(EventNames.BattleCamera_Events.CURRENT_FOCUS, this.ResetPosition);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleCamera_Events.ENEMY_FOCUS, this.EnemyPosition);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.CUTSCENE_PLAY, this.battleCutsceneCamMove);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.CUTSCENE_END, this.battleCutsceneReset);

        this.movementSpeed = this.Speed;
        this.shiftSpeed = this.movementSpeed * 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inCutscene) {
            this.CameraMove();
            this.CameraLook();
        }

        if (Input.GetKeyUp(KeyCode.C) /*&& !UnitActionManager.Instance.OnAttack*/) {
            this.ResetPosition();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            this.movementSpeed = this.shiftSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            this.movementSpeed = this.Speed;
        }
        if (this.reset) {
            this.cam.transform.position = Vector3.Lerp(this.cam.transform.position, this.targetPosition, Time.deltaTime * 10f);
            Vector3 pos = this.cam.transform.position;
            pos.y = Mathf.MoveTowards(this.cam.transform.position.y, this.targetPosition.y, Time.deltaTime * 10f);
            this.cam.transform.position = pos;
            if (Vector3.Distance(this.cam.transform.position, this.targetPosition) < 0.01f) {
                this.cam.transform.position = this.targetPosition;
                this.reset = false;
                this.targetPosition = Vector3.zero;
            }
        }
        //if (Input.GetKeyUp(KeyCode.Escape))
        //{
        //    SceneManager.LoadScene("Lobby");
        //}
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.BattleCamera_Events.CURRENT_FOCUS);
        EventBroadcaster.Instance.RemoveObserver(EventNames.BattleCamera_Events.ENEMY_FOCUS);
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

        //this.heightOffset = 6;

        /*this.cam.fieldOfView = 71.0f; // reset to original FoV*/
    }

    private void ResetPosition() {
        //Debug.Log("Cam Reset");

        Vector3 characterPos = Vector3.zero;

        if (UnitActionManager.Instance.GetFirstUnit() is Unit unit) {
            characterPos = unit.transform.position;
            characterPos.y = unit.Tile.transform.position.y;
        }
        if (UnitActionManager.Instance.GetFirstUnit() is SpecialUnits sUnit) {
            characterPos = sUnit.location.position;
        }
        Vector3 cameraPosition = characterPos;

        cameraPosition.y = characterPos.y + heightOffset;

        this.reset = true;
        this.targetPosition = cameraPosition;
        this.targetPosition.z = cameraPosition.z - 2;

        // set the camera's x rotation to 89 instead of exactly looking at the character (90)
        Quaternion targetRotation = Quaternion.Euler(63f, 0f, 0f);
        this.cam.transform.rotation = targetRotation;

        //this.cam.transform.LookAt(this.targetPosition.normalized);

        // override the rotationX with the new rotation so that it wont go back to the original rotation before the reset
        this.rotationX = cam.transform.localEulerAngles.x;

        //this.cam.fieldOfView = 71.0f; // reset to original FoV
    }

    private void CameraLook() {
        if (Input.GetMouseButtonDown(1)) {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1)) {

            Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

            //cam.transform.position = transform.position;

            rotationX += direction.y * this.cameraSensitivity;
            rotationX = Mathf.Clamp(rotationX, 36, 89);

            cam.transform.localEulerAngles = new Vector3(rotationX, cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z);
            cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * this.cameraSensitivity, Space.World);

            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);

            //cam.transform.Rotate(new Vector3(1,0,0), direction.y * 180);
            //cam.transform.Rotate(new Vector3(0,1,0), -direction.x * 180,Space.World);
            //cam.transform.Translate(new Vector3(0,0,-10));

            //previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.mouseScrollDelta.y > 0 && this.heightOffset > 4) {
            this.heightOffset--;
        }
        if (Input.mouseScrollDelta.y < 0 && this.heightOffset < 12 ) {
            this.heightOffset++;
        }
    }
    private void CameraMove() {
        this.AdjustHeightToTerrain();

        float speed = Time.deltaTime * this.movementSpeed;

        if (!this.reset && !UnitActionManager.Instance.bEnemy) {
            Vector3 moveDir = Vector3.zero;
            if (Input.GetKey(KeyCode.W)) {
                Vector3 forward = this.cam.transform.forward;
                forward.y = 0; // discard y/vertical component so that it wont go up or down
                moveDir += forward.normalized; //normalize to maintain consistent speed
            }
            if (Input.GetKey(KeyCode.A)) {
                Vector3 left = this.cam.transform.right * -1.0f;
                left.y = 0;
                moveDir += left.normalized;
            }
            if (Input.GetKey(KeyCode.S)) {
                Vector3 back = this.cam.transform.forward * -1.0f;
                back.y = 0;
                moveDir += back.normalized;
            }
            if (Input.GetKey(KeyCode.D)) {
                Vector3 right = this.cam.transform.right;
                right.y = 0;
                moveDir += right.normalized;
            }

            float checkDistance = speed + 0.1f;

            if (!Physics.Raycast(this.cam.transform.position, moveDir.normalized, checkDistance, LayerMask.GetMask("Border"))) {
                this.cam.transform.Translate(moveDir.normalized * speed, Space.World);
            }
            //this.cam.transform.Translate(moveDir.normalized * speed, Space.World);

        }
    }
    private void AdjustHeightToTerrain() {
        Ray ray = new Ray(this.cam.transform.position, Vector3.down);
        //Debug.DrawRay(this.transform.position, Vector3.down * 1000f, Color.red, 5f);
        if (Physics.Raycast(ray, out RaycastHit hit, 20f, LayerMask.GetMask("Tiles", "Border"))) {
            float targetY = hit.point.y + this.heightOffset;
            Vector3 pos = cam.transform.position;
            pos.y = Mathf.Lerp(pos.y, targetY, Time.deltaTime * 5.0f);
            cam.transform.position = pos;
            //Debug.Log(hit.point.y);
        }
    }
    private void battleCutsceneCamMove()
    {
        GameObject battleCutscene = GameObject.FindWithTag("BattleCutscene");
        previousCamPos = cam.transform.position;
        previousCamRot = cam.transform.rotation;
        cam.gameObject.transform.position = battleCutscene.transform.position;
        cam.gameObject.transform.rotation = battleCutscene.transform.rotation;
    }

    private void battleCutsceneReset()
    {
        cam.gameObject.transform.position = previousCamPos;
        cam.gameObject.transform.rotation = previousCamRot;
    }

}
