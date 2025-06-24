using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//https://www.youtube.com/watch?v=9dYDBomQpBQ

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPause = false;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                resumeGame();
            }
            else pauseGame();

        }
    }

    public void pauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
    }

    public void resumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;

    }

    public void OnResumeButton()
    {
        resumeGame();
    }

    public void OnRestartButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void OnLobbyButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
