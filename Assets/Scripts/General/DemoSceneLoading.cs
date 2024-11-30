using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoSceneLoading : MonoBehaviour
{
    [SerializeField]
    private string _screenName;

    public void GoToScene()
    {
        SceneManager.LoadScene(_screenName);
    }
}
