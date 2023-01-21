using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    static public bool isPaused;
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private GameObject backgroundCanvas;

    void Start()
    {
        ResumeGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //test
        {
            if (!isPaused)
            {
                StartPauseMenu();
            }
            else if (isPaused)
            {
                ResumeGame();
            }
        }
    }

    private void StartPauseMenu()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseMenuCanvas.SetActive(true);
        backgroundCanvas.SetActive(true);

    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseMenuCanvas.SetActive(false);
        backgroundCanvas.SetActive(false);
    }
}
