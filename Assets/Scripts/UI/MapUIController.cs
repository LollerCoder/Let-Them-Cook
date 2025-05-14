using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapUIController : MonoBehaviour
{
    [SerializeField] Button lvl2;
    [SerializeField] Button lvl3;
    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Lvl1ButtonClicked()
    {
        //SceneManager.LoadScene("TempBattleScene");
        //SceneManager.LoadScene("Tutorial-1");
        SceneManager.LoadScene("Level-1");
    }

    public void Lvl2ButtonClicked()
    {
        SceneManager.LoadScene("Level-2");
    }

     public void Lvl3ButtonClicked()
    {
        SceneManager.LoadScene("Level-3");
    }
}
