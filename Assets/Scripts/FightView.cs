using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightView : MonoBehaviour
{
    public float timeRemaining = 120;
    public bool timerIsRunning = false;
    public Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        // Start the timer
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning) 
        {
            // It's the final count down...
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else 
            {
                timeRemaining = 0;
                timerIsRunning = false;
                DoAHit();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);  
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void DoAHit()
    {
        Debug.Log("Time to do a fight action.");
        timeRemaining = 120;
        timerIsRunning = true;
    }
}
