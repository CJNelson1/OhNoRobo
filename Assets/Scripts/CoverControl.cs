using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        // When target is hit
        if (other.gameObject.tag == "Player")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        // When target is stop hit
        if (other.gameObject.tag == "Player")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
