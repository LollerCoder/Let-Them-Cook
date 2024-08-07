using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMCBController : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainCamera;

    [SerializeField]
    private GameObject _cookBook;

    [SerializeField]
    private GameObject _canvaElements;

    public void GoToCookingScene()
    {
        SceneManager.LoadScene("Cooking");
    }

    public void ShowCookbook()
    {
        this.ToggleDisplay(true);
    }

    private void OnMouseDown()
    {
        this.ToggleDisplay(false);
    }

    private void ToggleDisplay(bool show)
    {
        this._mainCamera.SetActive(!show);
        this._canvaElements.SetActive(!show);
        this._cookBook.SetActive(show);
    }

    // Start is called before the first frame update
    void Start()
    {
        this._cookBook.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
