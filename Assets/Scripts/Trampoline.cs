using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float bounceForce = 2000f; // Amount of force added when the player jumps on trampoline.
    public bool collidingWithCharacter;
    public EdgeCollider2D primaryCollider;
    public EdgeCollider2D triggerCollider;

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
        
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        // When target is hit
        if(col.gameObject.tag == "Player")
        {
            // Only respond to edge collider
            if (col.otherCollider == primaryCollider)
            {
                collidingWithCharacter = true;

                if (col.relativeVelocity.y <= 0) 
                {
                    jumpAround(col.rigidbody);  // If coming in from above... jump up, jump up, and get down
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D col) 
    {
        // When target is stop hit
        if(col.gameObject.tag == "Player")
        {
            // Only respond to edge collider
            if (col.otherCollider == primaryCollider)
            {
                collidingWithCharacter = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.attachedRigidbody.velocity.y > 0)
        {
            primaryCollider.enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        primaryCollider.enabled = true;
    }

    void jumpAround(Rigidbody2D character)
    {
        character.AddForce(new Vector2(0f, bounceForce));
    }
}