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
    public System.Random seed;

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
    public BadGuyDisplay bgd;

    // Good robot stuff
    public RobotAction nextGoodAction;
    public int goodHealth = 3;
    public GoodGuyDisplay ggd;

    // Charge Levels
    public int HeadCharge = 0;
    public int HeadChargeMax = 8;
    public BigBrain bb;
    public ToggleBox[] headToggles = new ToggleBox[8];
    public int LeftArmCharge = 0;
    public int LeftArmChargeMax = 3;
    public BigLeft left;
    public LeftArmToggles[] leftToggles = new LeftArmToggles[3];
    public int RightArmCharge = 0;
    public int RightArmChargeMax = 2;
    public BigRight right;
    public RightArmToggles[] rightToggles = new RightArmToggles[2];

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
            bgd.UpdateSprite(1);
            enemyHealth = 3;
            nextGoodAction = RobotAction.LaserHead;
            goodHealth = 3;
        }
        else
        {
            Destroy(gameObject);
        }

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
        if (nextGoodAction == RobotAction.NullPosition)
        {
            ResolveRoundWat();
        }
        else if (nextGoodAction == nextEnemyAction) 
        {
            if(CheckCharge(nextGoodAction))
            {
                switch (nextGoodAction)
                {
                    case RobotAction.LaserHead:
                        ResetHeadToggles();
                        break;
                    case RobotAction.SpecialLeftArm:
                        ResetLeftArmToggles();
                        break;
                    case RobotAction.PunchRightArm:
                        ResetRightArmToggles();
                        break;
                }
                ResolveRoundTie();
            }
            else
            {
                ResolveRoundLoss();
            }
        }
        else
        {
            // Punch beats head, head beats special, special beats punch
            if (nextGoodAction == RobotAction.PunchRightArm)
            {
                if (nextEnemyAction == RobotAction.LaserHead)
                {
                    if(CheckCharge(RobotAction.PunchRightArm))
                    {
                        ResetRightArmToggles();
                        ResolveRoundWin();
                    }
                    else
                    {
                        ResolveRoundLoss();
                    }
                }
                else
                {
                    if(CheckCharge(RobotAction.PunchRightArm))
                    {
                        ResetRightArmToggles();
                    }
                    ResolveRoundLoss();
                }
            }
            else if (nextGoodAction == RobotAction.LaserHead)
            {
                if (nextEnemyAction == RobotAction.SpecialLeftArm)
                {
                    if(CheckCharge(RobotAction.LaserHead))
                    {
                        ResetHeadToggles();
                        ResolveRoundWin();
                    }
                    else
                    {
                        ResolveRoundLoss();
                    }
                }
                else
                {
                    if(CheckCharge(RobotAction.LaserHead))
                    {
                        ResetHeadToggles();
                    }
                    ResolveRoundLoss();
                }
            }
            else if (nextGoodAction == RobotAction.SpecialLeftArm)
            {
                if (nextEnemyAction == RobotAction.PunchRightArm)
                {
                    if(CheckCharge(RobotAction.SpecialLeftArm))
                    {
                        ResetLeftArmToggles();
                        ResolveRoundWin();
                    }
                    else
                    {
                        ResolveRoundLoss();
                    }
                }
                else
                {
                    if(CheckCharge(RobotAction.SpecialLeftArm))
                    {
                        ResetLeftArmToggles();
                    }
                    ResolveRoundLoss();
                }
            }
            else
            {
                ResolveRoundWat();
            }
        }
        if(nextGoodAction == RobotAction.LaserHead)
        {
            bb.ResetLevel();
        }
        else if (nextGoodAction == RobotAction.PunchRightArm)
        {

        }
        else if (nextGoodAction == RobotAction.SpecialLeftArm)
        {

        }
        // Reset timer and other new round stuff
        timeRemaining = 120;
        timerIsRunning = true;
        alternateActionFlag = !alternateActionFlag;
    }

    private bool CheckCharge(RobotAction roboAction)
    {
        switch (roboAction)
        {
            case RobotAction.LaserHead:
                if (HeadCharge / HeadChargeMax == 1)
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            case RobotAction.SpecialLeftArm:
                if (LeftArmCharge / LeftArmChargeMax == 1)
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            case RobotAction.PunchRightArm:
                if (RightArmCharge / RightArmChargeMax == 1)
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            default:
                return true;
        }
    }
    private void ResolveRoundTie()
    {
        Debug.Log("Resolve Round Tie");

        bgd.TiePose();
        FinishRound();
    }

    private void ResolveRoundWin()
    {
        Debug.Log("Resolve Round Win");

        bgd.DamagePose();
        enemyHealth -= 1;

        mrBadBar.SetHealth(enemyHealth);
        FinishRound();
    }

    private void ResolveRoundLoss()
    {
        Debug.Log("Resolve Round Loss");
        
        ggd.DamagePose();
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
            SceneManager.LoadScene(sceneName:"Victory");
        }

        if (goodHealth <= 0)
        {
            SceneManager.LoadScene(sceneName:"Defeat");
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
            bgd.UpdateSprite(temp);
        }
        else if (temp == 2)
        {
            nextEnemyAction = RobotAction.SpecialLeftArm;
            bgd.UpdateSprite(temp);
        }
        else 
        {
            nextEnemyAction = RobotAction.PunchRightArm;
            bgd.UpdateSprite(3);
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
    private void ResetHeadToggles()
    {
        for(int i = 0; i < 8; i++)
        {
            headToggles[i].makeBad();
        }
        bb.ResetLevel();
    }
    private void ResetRightArmToggles()
    {
        foreach(RightArmToggles toggles in rightToggles)
        {
            toggles.makeBad();
        }
        right.ResetLevel();
    }
    private void ResetLeftArmToggles()
    {
        foreach(LeftArmToggles toggles in leftToggles)
        {
            toggles.makeBad();
        }
        left.ResetLevel();
    }
}