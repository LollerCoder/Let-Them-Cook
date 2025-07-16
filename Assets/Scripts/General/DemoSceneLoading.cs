using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoSceneLoading : MonoBehaviour
{
    [SerializeField]
    private string _screenName;

    public string ScreenName
    {
        get { return _screenName; }
        set { this._screenName = value; }
    }

    public void GoToScene()
    {
        SceneManager.LoadScene(_screenName);
    }
}
