using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class MapOpener : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private GameObject camerajeraObjRef;
    [SerializeField] private GameObject playerCamMarker; //position of where the camera should be when untoggled
    [SerializeField] private Vector3 cameraMapPos; //position of camera when map is toggled on
    private Vector3 cameraTargetPos;

    private bool isPlayerIn = false;

    [Header("Map")]
    [SerializeField] private GameObject mapObj;
    private bool mapToggled = false;

    [Header("Button Prompt")]
    [SerializeField] private GameObject _ButtonPrompt;

    private GameObject playerRef;

    private LevelSelector[] levelSelectors;

    

    // Start is called before the first frame update
    void Start()
    {
        cameraTargetPos = camerajeraObjRef.transform.position;

        //mapObj.GetComponent<GameScript>().LoadGame();
        mapObj.GetComponent<GameScript>().LoadGameV2();

        updateToggles();

        mapObj.SetActive(false);

  
        //load level data (outline code)
        /*
         for (int i = 0; i < levelData.count; i++)
        {
            levelSelectors[i].ToggleLevel(levelData[i])
        }
         */
    }

    private void updateToggles()
    {
        Debug.Log("MAP OPENER");
          //toggling the levels
        levelSelectors = mapObj.GetComponentsInChildren<LevelSelector>();

        this._ButtonPrompt.SetActive(false);

        for (int i = 0; i < mapObj.transform.childCount; i++)
        {
            levelSelectors[i].ToggleLevel(i <= LevelManager.LevelsCompleted);

            //if (mapObj.GetComponent<GameScript>().bData == false) { Debug.Log("LOOP RETURN"); return; }
           
        }

        levelSelectors[0].ToggleLevel(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(prevCameraPos);
        if (Input.GetKeyDown(KeyCode.E) && isPlayerIn)
        {
            updateToggles();
            ToggleMap();
        }

        if (!mapToggled) cameraTargetPos = playerCamMarker.transform.position;

        camerajeraObjRef.transform.position = Vector3.MoveTowards(
            camerajeraObjRef.transform.position,
            cameraTargetPos,
            0.5f
            );

        if (Input.GetKeyDown(KeyCode.F10)) {
            LevelManager.LevelsCompleted = 5;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        this._ButtonPrompt.SetActive(true);
        playerRef = other.gameObject;
        isPlayerIn = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        this._ButtonPrompt.SetActive(false);
        isPlayerIn = false;
    }

    private void ToggleMap()
    {
        //Toggle on
        if (!mapToggled)
        {
            LobbyDialogue.Instance.MapTutorialPlay();
            //Debug.Log("Going to map");
            mapToggled = true;
            cameraTargetPos = cameraMapPos;
            playerRef.GetComponent<PlayerWASDMovement>().NoWalk();
        }

        //Toggle off
        else
        {
            //Debug.Log("Back to player");
            mapToggled = false;
            cameraTargetPos = playerCamMarker.transform.position;
            playerRef.GetComponent<PlayerWASDMovement>().YesWalk();
        }

        GetComponent<BoxCollider>().enabled = !mapToggled;
        this._ButtonPrompt.SetActive(!mapToggled);
        playerRef.GetComponentInChildren<SpriteRenderer>().enabled = !mapToggled;
        playerRef.GetComponent<Collider>().enabled = !mapToggled;
        mapObj.SetActive(mapToggled);
    }
}
