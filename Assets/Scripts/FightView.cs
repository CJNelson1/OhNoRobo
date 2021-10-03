using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FightView : MonoBehaviour
{
    private System.Random seed;
    public static FightView fightInstance;

    // Timer stuff
    public float timeRemaining = 120;
    public bool timerIsRunning = false;
    public Text timeText;
    public float secondTicker = 0;
    public int numSeconds = 0;

    // Felt cute, might delete later
    public enum RobotAction
    {
        NullPosition,
        LaserHead,
        SpecialLeftArm,
        PunchRightArm
    }

    // Enemy robot stuff
    public RobotAction nextEnemyAction;
    public int enemyHealth;

    // Good robot stuff
    public RobotAction nextGoodAction;
    public int goodHealth;

    // This is a persistant manager so we're using awake instead of start
    void Awake()
    {
        // I can't remember why we had to do all this nonsense, but this is all from VikingManager 
        if (!fightInstance)
        {
            fightInstance = this;
            seed = new System.Random();

            // Start the timer
            timerIsRunning = true;

            // Instantiate robos
            nextEnemyAction = RobotAction.LaserHead;
            enemyHealth = 100;
            nextGoodAction = RobotAction.LaserHead;
            goodHealth = 100;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
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

                secondTicker += Time.deltaTime;
                if (secondTicker > 1)
                {
                    // Debug print every second for test purposes
                    Debug.Log("It's been " + numSeconds + " seconds :)");
                    secondTicker = 0;
                    numSeconds++;

                    // Update timer every second
                    DisplayTime(timeRemaining);
                }
            }
            else 
            {
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

    #region Fighting Words
    public void DoAHit()
    {
        timeRemaining = 0;
        timerIsRunning = false;
        ResolveRound();
    }

    private void ResolveRound()
    {
        Debug.Log("Do a hit.");
        
        if (nextGoodAction == RobotAction.NullPosition)
        {
            // Always lose
            Debug.Log("Boi watchu doin?");
            ResolveRoundLoss();
        }

        // Punch beats head, head beats special, special beats punch
        if (nextGoodAction == nextEnemyAction) 
        {
            // Tie
            Debug.Log("It's a draw");
            ResolveRoundTie();
        }
        else
        {
            // Not tie
            if (nextGoodAction == RobotAction.PunchRightArm)
            {
                Debug.Log("Good boi punch");

                if (nextEnemyAction == RobotAction.LaserHead)
                {
                    Debug.Log("Bad boi laser");
                    ResolveRoundWin();
                }
                else if (nextEnemyAction == RobotAction.SpecialLeftArm)
                {
                    Debug.Log("Bad boi special");
                    ResolveRoundLoss();
                }
                else
                {
                    Debug.Log("wat");
                    ResolveRoundWat();
                }
            }
            else if (nextGoodAction == RobotAction.LaserHead)
            {
                Debug.Log("Good boi laser");

                if (nextEnemyAction == RobotAction.PunchRightArm)
                {
                    Debug.Log("Bad boi punch");
                    ResolveRoundLoss();
                }
                else if (nextEnemyAction == RobotAction.SpecialLeftArm)
                {
                    Debug.Log("Bad boi special");
                    ResolveRoundWin();
                }
                else
                {
                    Debug.Log("wat");
                    ResolveRoundWat();
                }
            }
            else if (nextGoodAction == RobotAction.SpecialLeftArm)
            {
                Debug.Log("Good boi special");

                if (nextEnemyAction == RobotAction.LaserHead)
                {
                    Debug.Log("Bad boi laser");
                    ResolveRoundLoss();
                }
                else if (nextEnemyAction == RobotAction.PunchRightArm)
                {
                    Debug.Log("Bad boi punch");
                    ResolveRoundWin();
                }
                else
                {
                    Debug.Log("wat");
                    ResolveRoundWat();
                }
            }
            else
            {
                Debug.Log("wat");
                ResolveRoundWat();
            }
        }

        // Reset timer
        timeRemaining = 120;
        timerIsRunning = true;
    }

    private void ResolveRoundTie()
    {
        Debug.Log("Resolve Round Tie");

        goodHealth -= 10;
        enemyHealth -= 10;

        FinishRound();
    }

    private void ResolveRoundWin()
    {
        Debug.Log("Resolve Round Win");

        enemyHealth -= 20;

        FinishRound();
    }

    private void ResolveRoundLoss()
    {
        Debug.Log("Resolve Round Loss");

        goodHealth -= 20;

        FinishRound();
    }

    private void ResolveRoundWat()
    {
        // Display Scene Bugsplat lol
    }

    private void FinishRound()
    {
        // Make something unstable???
        /* Working on it in the morning -- 
            make (object) unstable (things at random?) if - good robot lost the action
            every other action that occurs
            notify unstable chances to the Map to keep a track

            
        */

        CheckGameOver();

        PickNextEnemyAction();
    }

    private void CheckGameOver()
    {
        // Big winner?
        if (enemyHealth <= 0)
        {
            GameOverWin();
        }

        if (goodHealth <= 0)
        {
            GameOverLose();
        }
    }

    private void GameOverWin()
    {
        // Go to game over scene
    }

    private void GameOverLose()
    {
        // Go to game over scene
    }
    #endregion

    private void PickNextEnemyAction()
    {
        // How many licks does it take to get to the center of the tootsie pop?
        int temp = seed.Next(1, 4);

        if (temp == 1)
        {
            nextEnemyAction = RobotAction.LaserHead;
        }
        else if (temp == 2)
        {
            nextEnemyAction = RobotAction.SpecialLeftArm;
        }
        else 
        {
            nextEnemyAction = RobotAction.PunchRightArm;
        }
    }
}
