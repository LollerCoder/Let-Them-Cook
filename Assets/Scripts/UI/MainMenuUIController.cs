using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonPressed()
    {
        SceneManager.LoadScene("Map");
    }

    public void CookingButtonPressed()
    {

    }

    public void CookbookButtonPressed()
    {

    }

    public void ExitButtonPressed()
    {
        Application.Quit();
    }
}
