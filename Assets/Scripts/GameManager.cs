using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class GameManager : MonoBehaviour
{
    public bool isGameOver;
    [SerializeField] private Player player;
    public GameManager singleton;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        isGameOver = false;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (player.health <= 0)
            {
                PlayGameOverSequence();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
    }

    public void PlayGameOverSequence()
    {
        isGameOver = true;
        Debug.Log("Game over");
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(1);
        // SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCount);
    }

    public void QuitGame(){
        Application.Quit(); // remember to cater for built version
    }
}
