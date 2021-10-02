using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    public float timeRemaining = 120;
    public bool timerIsRunning = false;

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
                Debug.Log("Time to do a fight action.");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    static float getMinutes() 
    {
        return Mathf.FloorToInt(timeRemaining / 60);
    }

    static float getSeconds()
    {
        return Mathf.FloorToInt(timeRemaining % 60);
    }
}
