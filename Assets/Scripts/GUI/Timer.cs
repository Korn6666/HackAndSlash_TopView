using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static float time;

    [SerializeField] private TMPro.TextMeshProUGUI waveText;
    [SerializeField] private TMPro.TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        waveText.text = "Wave" + WaveManager.currentWave.ToString();

        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);
        timeText.text = minutes.ToString("F0") + ":" + seconds.ToString("F0");
    }
}
