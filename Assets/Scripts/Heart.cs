using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public bool collidingWithCharacter;
    public Rigidbody2D rb;
    public static float torqueDelta = 4f;

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
        if (!collidingWithCharacter) 
        {
            float rotation = transform.eulerAngles.z;

            if (rotation > 10f && rotation < 45f)
            {
                rb.AddTorque(-torqueDelta);
            }
            else if (rotation > 45f && rotation < 80f)
            {
                rb.AddTorque(torqueDelta);
            }
            else if (rotation > 100f && rotation < 135f)
            {
                rb.AddTorque(-torqueDelta);
            }
            else if (rotation > 135f && rotation < 170f)
            {
                rb.AddTorque(torqueDelta);
            }
            else if (rotation > 190f && rotation < 225f)
            {
                rb.AddTorque(-torqueDelta);
            }
            else if (rotation > 225f && rotation < 260f)
            {
                rb.AddTorque(torqueDelta);
            }
            else if (rotation > 280f && rotation < 315f)
            {
                rb.AddTorque(-torqueDelta);
            }
            else if (rotation > 315f && rotation < 350f)
            {
                rb.AddTorque(torqueDelta);
            }
            else 
            {
                // Slow down
                rb.AddTorque(rb.angularVelocity * -0.2f);

                // Reset player choice on the FightView
                // TODO
            }
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
        // When target is stop hit
        if(col.gameObject.tag == "Player")
        {
            collidingWithCharacter = false;
        }
    }
}
