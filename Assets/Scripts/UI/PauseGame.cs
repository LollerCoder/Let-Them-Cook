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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnLobbyButton()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
