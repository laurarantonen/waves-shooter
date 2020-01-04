using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

    [SerializeField] float delayInSeconds = 5f;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        FindObjectOfType<GameSession>().ResetGame(); // resets the game score
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    public void LoadHighScore()
    {
        StartCoroutine(WaitAndLoadHighScore());
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");
    }

    IEnumerator WaitAndLoadHighScore()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("High Score");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene("Settings Menu");
    }
}
