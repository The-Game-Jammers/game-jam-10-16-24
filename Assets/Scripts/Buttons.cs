using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons: MonoBehaviour
{
    public void loadLevel(string levelName)
    {
        SceneManager.LoadSceneAsync(levelName);
    }

    public void quit()
    {
        Application.Quit();
        Debug.Log("Application Quit");
    }
}
