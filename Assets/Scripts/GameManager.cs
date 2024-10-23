using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool levelComplete;

    private void Start()
    {
        levelComplete = false;
    }

    public bool getLevelComplete()
    {
        return levelComplete;
    }

    public void GameWon()
    {
        levelComplete = true;
    }
}
