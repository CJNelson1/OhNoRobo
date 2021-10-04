using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class FightView : MonoBehaviour
{
    public static FightView fightInstance;
    public HealthBar mrGoodBar;
    public HealthBar mrBadBar;
    private System.Random seed;

    // Timer stuff
    public TextMeshProUGUI timeText;
    private float timeRemaining = 120;
    private bool timerIsRunning = false;
    private float secondTicker = 0;
    private int numSeconds = 0;

    // Trouble on every other action/move
    private bool alternateActionFlag = false;

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
    public int enemyHealth = 3;

    // Good robot stuff
    public RobotAction nextGoodAction;
    public int goodHealth = 3;

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
            enemyHealth = 3;
            nextGoodAction = RobotAction.LaserHead;
            goodHealth = 3;
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

        timeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
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
            // The only way to lose is to not play
            Debug.Log("Boi watchu doin?");
            ResolveRoundLoss();
        }
        else if (nextGoodAction == nextEnemyAction) 
        {
            // Tie
            Debug.Log("It's a draw");
            ResolveRoundTie();
        }
        else
        {
            // Punch beats head, head beats special, special beats punch
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
                    ResolveRoundWat();
                }
            }
            else
            {
                ResolveRoundWat();
            }
        }

        // Reset timer and other new round stuff
        timeRemaining = 120;
        timerIsRunning = true;
        alternateActionFlag = !alternateActionFlag;
    }

    private void ResolveRoundTie()
    {
        Debug.Log("Resolve Round Tie");

        FinishRound();
    }

    private void ResolveRoundWin()
    {
        Debug.Log("Resolve Round Win");

        enemyHealth -= 1;

        mrBadBar.SetHealth(enemyHealth);
        FinishRound();
    }

    private void ResolveRoundLoss()
    {
        Debug.Log("Resolve Round Loss");

        goodHealth -= 1;

        mrGoodBar.SetHealth(goodHealth);
        FinishRound();
    }

    private void ResolveRoundWat()
    {
        Debug.Log("wat " + nextGoodAction + " " + nextEnemyAction);
        // SceneManager.LoadScene("Bugsplat_lmao");
    }

    private void FinishRound()
    {
        MakeUnstable(); //if lost make something unstable or every alternate action, make something unstable
            
        if(alternateActionFlag == true){
            alternateActionFlag = false; //Set action flag back to false, continue game.
        }

        CheckGameOver();

        PickNextEnemyAction();
    }

    private void CheckGameOver()
    {
        // Big winner?
        if (enemyHealth <= 0)
        {
            GameOverWin();
            // SceneManager.LoadScene("Win");
        }

        if (goodHealth <= 0)
        {
            GameOverLose();
            // SceneManager.LoadScene("Lose");
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

        Debug.Log("Next enemy action is " + nextEnemyAction);
    }

    private void MakeUnstable() 
    {
        // Get MapView Instance to keep track of instable parts
        MapView mapInstance = MapView.mapInstance;
        
        // Randomly choose a part to go unstable
        // Head=1, LeftArm=2, RightArm=3
        int randomChoice = seed.Next(1,4);

        Debug.Log("Random instability value is " + randomChoice);

        // Ben says: Sayali, I moved your code into MapView
        switch (randomChoice)
        {
            case 1: // Head
                mapInstance.makeHeadUnstable();
                break;

            case 2: // Left Arm
                mapInstance.makeLeftArmUnstable();     
                break;

            case 3: // Right Arm
                mapInstance.makeRightArmUnstable();
                break;

            default: break;
        }
    }
}