using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZappyPanel : MonoBehaviour
{
    public bool zappy;
    public Sprite OffSprite;
    public Sprite OnSprite;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = OffSprite;
    }

    void Awake() 
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void makeZappy()
    {
        zappy = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = OnSprite;
    }

    public void makeNotZappy()
    {
        zappy = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = OffSprite;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        // When target is hit
        if (other.gameObject.tag == "Player")
        {
            if (zappy)
            {
                // Oof ouch owie
            }
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