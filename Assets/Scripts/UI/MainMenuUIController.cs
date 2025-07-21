using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject _Credits;

    // Start is called before the first frame update
    void Start()
    {
        this._Credits.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonPressed()
    {
        SceneManager.LoadScene("Map");
    }

    public void CreditsButtonPressed()
    {
        this._Credits.SetActive(!this._Credits.activeSelf);
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
    }
}
