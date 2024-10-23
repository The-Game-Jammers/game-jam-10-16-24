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
    [SerializeField] GameObject gameManager;
    GameManager gameManagerLink;

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
        gameManagerLink = gameManager.GetComponent<GameManager>();
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

    public void Restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void hideMenu(GameObject Menu)
    {
        Menu.gameObject.SetActive(false);
        if (gameManagerLink.getLevelComplete())
        {
            showMenu(winMenuUI);
            Time.timeScale = 0f;
        }
    }

    public void showMenu(GameObject Menu) 
    {
        Menu.gameObject.SetActive(true);
    }

    public void FreezeTime(bool freezTime)
    {
        if (freezTime)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }
}
