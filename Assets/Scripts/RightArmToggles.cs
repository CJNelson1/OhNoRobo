using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightArmToggles : MonoBehaviour
{
    public bool bad;
    public Sprite GoodSprite;
    public Sprite BadSprite;
    public BigRight right;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = BadSprite;
        bad = true;
    }

    public void makeBad()
    {
        bad = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = BadSprite;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        // When target is hit
        if (other.gameObject.tag == "Player" && bad)
        {
            bad = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = GoodSprite;
            right.IncreaseLevel();
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        // When target is stop hit
        if (other.gameObject.tag == "Player")
        {
            
        }
    }
}
