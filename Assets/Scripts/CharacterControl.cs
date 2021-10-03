using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public bool facingRight = true; // For determining which way the player is currently facing.

    [SerializeField] private float maxSpeed = 10f; // The fastest the player can travel in the x axis.
    [SerializeField] private float jumpForce = 400f; // Amount of force added when the player jumps.	

    [SerializeField] private bool airControl = false; // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask whatIsGround; // A mask determining what is ground to the character

    private Transform groundCheck; // A position marking where to check if the player is grounded.
    private float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool grounded = false; // Whether or not the player is grounded.
    private Transform ceilingCheck; // A position marking where to check for ceilings
    private float ceilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    

    private bool jump;

    private void Awake()
    {
        // Setting up references.
        groundCheck = transform.Find("GroundCheck");
        ceilingCheck = transform.Find("CeilingCheck");
    }


    private void Update()
    {
        if(!jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            jump = Input.GetButtonDown("Jump");
        }

    }

    private void FixedUpdate() 
    {
        float h = Input.GetAxis("Horizontal");
        // Pass all parameters to the character control script.
        this.Move(h, jump);
        jump = false;
    }

    public void Move(float move, bool jump)
    {
        //only control the player if grounded or airControl is turned on
        if (grounded || airControl)
        {
            // Move the character
            GetComponent<Rigidbody2D>().velocity = new Vector2(move*maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !facingRight)
                // ... flip the player.
                Flip();
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && facingRight)
                // ... flip the player.
                Flip();
        }
        // If the player should jump...
        if (jump)
        {
            // Add a vertical force to the player.
            grounded = false;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}