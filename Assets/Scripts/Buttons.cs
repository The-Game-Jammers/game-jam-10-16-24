using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class Buttons: MonoBehaviour
{
    [SerializeField] GameObject pauseMenuUI;
    public bool GameIsPaused;
    [SerializeField] GameObject winMenuUI;

    public void loadLevel(string levelName)
    {
        SceneManager.LoadSceneAsync(levelName);
    }

    public void quit()
    {
        Application.Quit();
        Debug.Log("Application Quit");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Start()
    {
        pauseMenuUI.SetActive(false);
        winMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Resume()
    {
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Pause()
    {
        GameIsPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameWin()
    {
        StopAllCoroutines();
        Time.timeScale = 0f;
        winMenuUI.SetActive(true);
    }
}
