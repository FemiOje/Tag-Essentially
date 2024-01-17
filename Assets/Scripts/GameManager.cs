using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameOver;
    public Player player;


    private void Start()
    {
        isGameOver = false;
    }

    private void Update()
    {
        if (player.health <= 0)
        {
            PlayGameOverSequence();
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
}
