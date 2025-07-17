using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneLevelTransitioner
{
    [Header("Scene names")]
    [SerializeField]
    private string NextSceneToLoad;

    public void LoadScene()
    {
        SceneManager.LoadScene(NextSceneToLoad);
    }
}
