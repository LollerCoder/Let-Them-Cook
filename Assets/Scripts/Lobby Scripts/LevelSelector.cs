using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [Header("Level Info")]
    public bool canLoad = false;
    [SerializeField] private string sceneToLoad;

    [Header("Toggle Indicator")]
    [SerializeField] private Renderer Indicator;
    [SerializeField] private Material unlockedMat;
    [SerializeField] private Material lockedMat;

    private void Start()
    {

    }

    private void OnMouseDown()
    {
        LoadLevel();
    }

    public void ToggleLevel(bool isLocked)
    {
        canLoad = isLocked;

        if (canLoad) this.Indicator.material = unlockedMat;
        else this.Indicator.material = lockedMat;
    }

    public void LoadLevel()
    {
        if (!canLoad) return;

        Debug.Log("Loading " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}
