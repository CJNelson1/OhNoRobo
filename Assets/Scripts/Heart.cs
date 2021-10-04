using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private bool collidingWithCharacter;
    private Rigidbody2D rb;
    [SerializeField] public float torqueDelta = 40f;
    private float timeSinceTouch = 0;
    public GoodGuyDisplay ggd;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceTouch += Time.deltaTime;
        if (timeSinceTouch > 0.5)
        {
            if (!collidingWithCharacter) 
            {
                torqueUp();
            }
        }

        // Reset player choice on the FightView
        checkPlayerChoice();
    }

    private void checkPlayerChoice()
    {
        float rotation = transform.eulerAngles.z;

        if (rotation >= 45f && rotation < 135f)
        {
            FightView.fightInstance.nextGoodAction = FightView.RobotAction.PunchRightArm;
            ggd.UpdateSprite(2);
        }
        else if (rotation >= 135f && rotation < 225f)
        {
            FightView.fightInstance.nextGoodAction = FightView.RobotAction.LaserHead;
            ggd.UpdateSprite(1);
        }
        else if (rotation >= 225f && rotation < 315f)
        {
            FightView.fightInstance.nextGoodAction = FightView.RobotAction.SpecialLeftArm;
            ggd.UpdateSprite(3);
        }
        else if (rotation >= 315f || rotation < 45f)
        {
            FightView.fightInstance.nextGoodAction = FightView.RobotAction.NullPosition;
            ggd.UpdateSprite(0);
        }
    }

    private void torqueUp()
    {
        float rotation = transform.eulerAngles.z;

        if (rotation > 3f && rotation < 45f)
        {
            rb.AddTorque(-torqueDelta);
        }
        else if (rotation > 45f && rotation < 87f)
        {
            rb.AddTorque(torqueDelta);
        }
        else if (rotation > 93f && rotation < 135f)
        {
            rb.AddTorque(-torqueDelta);
        }
        else if (rotation > 135f && rotation < 177f)
        {
            rb.AddTorque(torqueDelta);
        }
        else if (rotation > 183f && rotation < 225f)
        {
            rb.AddTorque(-torqueDelta);
        }
        else if (rotation > 225f && rotation < 267f)
        {
            rb.AddTorque(torqueDelta);
        }
        else if (rotation > 273f && rotation < 315f)
        {
            rb.AddTorque(-torqueDelta);
        }
        else if (rotation > 315f && rotation < 357f)
        {
            rb.AddTorque(torqueDelta);
        }
        else 
        {
            // Slow down
            rb.AddTorque(rb.angularVelocity * -0.25f);
        }
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        // When target is hit
        if(col.gameObject.tag == "Player")
        {
            collidingWithCharacter = true;
        }
    }

    void OnCollisionExit2D(Collision2D col) 
    {
        timeSinceTouch = 0;

        // When target is stop hit
        if(col.gameObject.tag == "Player")
        {
            collidingWithCharacter = false;
        }
    }
}
