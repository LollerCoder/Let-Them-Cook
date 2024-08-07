using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PageTurner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _pages = new List<GameObject>();

    int _currentPage = 0;

    public void ShowNextPage()
    {
        //Debug.Log("Page count: " + (_pages.Count - 1) + " Current page: " + (_currentPage + 1));
        if (this._pages.Count - 1 < this._currentPage + 1) return;
        //Debug.Log("Showing next page!");

        this._pages[_currentPage].SetActive(false);

        this._currentPage++;

        this._pages[_currentPage].SetActive(true);
    }

    public void ShowPrevPage()
    {
        if (this._currentPage == 0) return;
        //Debug.Log("Showing prev page!");

        this._pages[_currentPage].SetActive(false);

        this._currentPage--;

        this._pages[_currentPage].SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject page in this._pages)
        {
            page.SetActive(false);
        }

        this._pages[_currentPage].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
