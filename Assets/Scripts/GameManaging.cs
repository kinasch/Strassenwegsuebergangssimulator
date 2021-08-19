using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManaging : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private FloorTileSpawner floorTileSpawner;
    [SerializeField] private GameObject player;

    private bool paused = false, input = true;
    public int playerOnLeaf = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && input)
        {
            input = false;
            paused = !paused;

            PauseOrUnpause();
        }

        if (Input.GetKeyUp(KeyCode.Escape) && !input)
        {
            input = true;
        }
    }

    private void FixedUpdate()
    {
        if (floorTileSpawner.waterRows.Contains(player.transform.position.x) && playerOnLeaf == 0)
        {
            LoseGame();
        }
    }

    private void PauseOrUnpause()
    {
            if (paused)
            {
                Time.timeScale = 0;
                pauseScreen.SetActive(true);
            } else if (!paused)
            {
                Time.timeScale = 1;
                pauseScreen.SetActive(false);
            }
    }
    
    public void LoseGame()
    {
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
