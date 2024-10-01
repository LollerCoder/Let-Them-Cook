using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapUIController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button lvl2;
    [SerializeField] Button lvl3;
    void Start()
    {
        this.lvl3.interactable = false;
        
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
