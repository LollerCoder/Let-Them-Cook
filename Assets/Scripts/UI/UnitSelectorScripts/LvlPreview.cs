using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class LvlPreview : MonoBehaviour
{
    [SerializeField] Camera cam;

    private GameObject _generatedLevel;

    private Vector3 _center;

    private void OnEnable()
    {
        this._generatedLevel = GameObject.Find(SceneManager.GetActiveScene().name);

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
         Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.RotateAround(this._center, Vector3.up, 20 * Time.deltaTime);
        cam.transform.LookAt(this._center);
    }
}
