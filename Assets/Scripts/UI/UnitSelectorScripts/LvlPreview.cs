using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LvlPreview : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] List<GameObject> levelMapPrefabs = new List<GameObject>();

    private GameObject _generatedLevel;

    private Vector3 _center;

    private GameScript lvlIndicator;

    private void OnEnable()
    {
        int levelToGenerate = 1;//lvlIndicator.getCurrLvl();
        this._generatedLevel = GameObject.Instantiate(levelMapPrefabs[levelToGenerate], this.gameObject.transform);

        Vector3 sumVector = new Vector3(0f, 0f, 0f);

        foreach (Transform child in _generatedLevel.transform)
        {
            sumVector += child.position;
        }

        this._center = sumVector / _generatedLevel.transform.childCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.RotateAround(this._center, Vector3.up, 20 * Time.deltaTime);
        cam.transform.LookAt(this._center);
    }
}
