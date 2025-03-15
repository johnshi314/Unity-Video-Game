using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the pause menu UI GameObject

    private bool isPaused = true; // Flag to track whether the game is currently paused

    void Start()
    {
        // Ensure the pause menu UI is hidden at the start
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        // Check for the "Esc" key press
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Esc key pressed");
            // Toggle the pause state
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        Debug.Log("Resuming game");
        // Hide the pause menu
        pauseMenuUI.SetActive(false);

        // Set the time scale back to 1 (normal speed)
        Time.timeScale = 1f;

        // Set the isPaused flag to false
        isPaused = false;
    }

    public void PauseGame()
    {
        Debug.Log("Pausing game");
        // Show the pause menu
        pauseMenuUI.SetActive(true);

        // Set the time scale to 0 to stop all game activity
        Time.timeScale = 0f;

        // Set the isPaused flag to true
        isPaused = true;
    }
}
