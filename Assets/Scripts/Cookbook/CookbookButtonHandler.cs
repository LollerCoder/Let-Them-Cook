using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookbookButtonHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _cookbook = null;

    [SerializeField]
    private GameObject _mainCam = null;

    [SerializeField]
    private GameObject _pages;

    private PageTurner _pageTurner;

    public void OnMouseDown()
    {
        //Debug.Log("Clicked " + this.gameObject.name);
        switch (this.gameObject.name)
        {
            case "Next":
                _pageTurner.ShowNextPage(); 
                break;
            case "Back":
                _pageTurner.ShowPrevPage(); 
                break;
            case "GoToCookbook":
                this.ShowCookbook(true); 
                break;
            case "GoBack":
                this.ShowCookbook(false); 
                break;
        }
    }

    private void ShowCookbook(bool show)
    {
        if (this._mainCam != null) this._mainCam.SetActive(!show);
        if (this._cookbook != null) this._cookbook.SetActive(show);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (this._pages != null)
        this._pageTurner = this._pages.GetComponent<PageTurner>();

        //if (this._cookbook != null) this._cookbook.SetActive(show);

        if (this._cookbook != null)
        {
            if (this._cookbook.activeSelf)
                this._cookbook.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
