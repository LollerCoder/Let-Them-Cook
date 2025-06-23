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

    private GameObject playerRef;

    private LevelSelector[] levelSelectors;

    

    // Start is called before the first frame update
    void Start()
    {
        cameraTargetPos = camerajeraObjRef.transform.position;

        mapObj.GetComponent<GameScript>().LoadGame();

        mapObj.SetActive(false);
        
        updateToggles();

  
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


        for (int i = 0; i < mapObj.transform.childCount; i++)
        {

            if (levelSelectors[i].canLoad || i == 0) levelSelectors[i].ToggleLevel(true);

            if (mapObj.GetComponent<GameScript>().bData == false) { Debug.Log("LOOP RETURN"); return; }
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(prevCameraPos);
        if (Input.GetKeyDown(KeyCode.E) && isPlayerIn)
        {
            ToggleMap();
        }

        if (!mapToggled) cameraTargetPos = playerCamMarker.transform.position;

        camerajeraObjRef.transform.position = Vector3.MoveTowards(
            camerajeraObjRef.transform.position,
            cameraTargetPos,
            0.5f
            );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        playerRef = other.gameObject;
        isPlayerIn = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        isPlayerIn = false;
    }

    private void ToggleMap()
    {
        //Toggle on
        if (!mapToggled)
        {
            //Debug.Log("Going to map");
            mapToggled = true;
            cameraTargetPos = cameraMapPos;
            playerRef.GetComponent<PlayerWASDMovement>().SetRunSpeed(0.0f);
        }

        //Toggle off
        else
        {
            //Debug.Log("Back to player");
            mapToggled = false;
            cameraTargetPos = playerCamMarker.transform.position;
            playerRef.GetComponent<PlayerWASDMovement>().SetRunSpeed(10.0f);
        }

        GetComponent<BoxCollider>().enabled = !mapToggled;
        playerRef.GetComponentInChildren<SpriteRenderer>().enabled = !mapToggled;
        playerRef.GetComponent<Collider>().enabled = !mapToggled;
        mapObj.SetActive(mapToggled);
    }
}
