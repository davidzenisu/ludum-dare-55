using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _timeRemaining;
    private bool _timerIsRunning = false;
    public TextMeshProUGUI timeText;

    private void Awake()
    {
        timeText = timeText ?? GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (_timerIsRunning)
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
                DisplayTime(_timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                _timeRemaining = 0;
                _timerIsRunning = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        if (!timeText)
        {
            return;
        }
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartTimer(float startTime)
    {
        _timerIsRunning = true;
        _timeRemaining = startTime;
    }

    public float GetTimeRemaining()
    {
        return _timeRemaining;
    }
}