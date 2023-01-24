using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private GameObject endMenuCanvas;
    [SerializeField] private GameObject backgroundCanvas;

    [SerializeField] private GameObject victory;
    [SerializeField] private GameObject gameOver;

    [SerializeField] private TMPro.TextMeshProUGUI waveReached;
    [SerializeField] private TMPro.TextMeshProUGUI timePlayed;

    private GameObject player;

    private void Start()
    {
        endMenuCanvas.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if(WaveManager.currentWave > 10) { SetEndMenuActive("victory"); }
        if(player == null) { SetEndMenuActive("gameover"); }
    }

    public void SetEndMenuActive(string winCondition)
    {
        Time.timeScale = 0;
        GameManager.instance.isPaused = true;

        Debug.Log(winCondition);
        endMenuCanvas.SetActive(true);
        backgroundCanvas.SetActive(true);

        if (winCondition == "gameover")
        {
            victory.SetActive(false);
            gameOver.SetActive(true);
        }

        if (winCondition == "victory")
        {
            victory.SetActive(true);
            gameOver.SetActive(false);
        }

        //timePlayed.text = 
        waveReached.text = "Wave reached " + WaveManager.currentWave.ToString();

        float minutes = Mathf.Floor(Timer.time / 60);
        float seconds = Mathf.RoundToInt(Timer.time % 60);
        timePlayed.text = "Time Played " + minutes.ToString("F0") + ":" + seconds.ToString("F0");
    }
}
