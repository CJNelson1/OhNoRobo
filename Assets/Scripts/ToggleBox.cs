using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBox : MonoBehaviour
{
    public bool bad;
    public Sprite GoodSprite;
    public Sprite BadSprite;
    public BigBrain brain;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = BadSprite;
        bad = true;
    }

    void Awake() 
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            brain.IncreaseLevel();
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