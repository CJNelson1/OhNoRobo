using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private float timeSincePressed = 0;
    public Sprite UpSprite;
    public Sprite DownSprite;
    private bool currentlyDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake() 
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSincePressed += Time.deltaTime;
        if (currentlyDown && timeSincePressed > 5f)
        {
            currentlyDown = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = UpSprite;
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        // When target is hit
        if (other.gameObject.tag == "Player")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = DownSprite;

            // Only allow the button to be pressed once every 5 seconds
            if (timeSincePressed > 5f)
            {
                FightView.fightInstance.DoAHit();
                timeSincePressed = 0;
                currentlyDown = true;
            }
        }
    }

    void OnCollisionStay2D(Collision2D other) 
    {
        // When target is stop hit
        if(other.gameObject.tag == "Player")
        {
            timeSincePressed = 0;
            currentlyDown = true;
        }
    }

    void OnCollisionExit2D(Collision2D other) 
    {
        // When target is stop hit
        if (other.gameObject.tag == "Player")
        {
            
        }
    }
}