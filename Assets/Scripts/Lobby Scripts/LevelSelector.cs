using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [Header("Level Info")]
    public bool canLoad = false;
    [SerializeField] private string sceneToLoad;

    [Header("Material")]
    [SerializeField] private Material unlockedMat;
    private Material lockedMat;

    private void Start()
    {
        lockedMat = GetComponent<Material>();
    }

    private void OnMouseDown()
    {
        LoadLevel();
    }

    public void ToggleLevel(bool isLocked)
    {
        canLoad = isLocked;

        if (canLoad) GetComponent<Renderer>().material = unlockedMat;
        else GetComponent<Renderer>().material = lockedMat;
    }

    public void LoadLevel()
    {
        if (!canLoad) return;

        Debug.Log("Loading " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}
