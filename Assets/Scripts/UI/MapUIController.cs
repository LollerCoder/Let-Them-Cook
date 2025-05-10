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
        /*Load Game here*/
        GameScript.LoadGame();
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
        SceneManager.LoadScene("TempBattleScene");
    }

    public void Lvl2ButtonClicked()
    {
        //SceneManager.LoadScene("");
    }
}
