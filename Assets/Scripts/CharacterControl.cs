using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public bool facingRight = true; // For determining which way the player is currently facing.

    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private float maxSpeed = 10f; // The fastest the player can travel in the x axis.
    [SerializeField] private float jumpForce = 400f; // Amount of force added when the player jumps.	
    [SerializeField] private bool airControl = false; // Whether or not a player can steer while jumping;
    [SerializeField] private int maxJump = 2;


    private CapsuleCollider2D capsuleCollider2d;
    public Animator animator;
    private bool jump;
    public int jumpCount;

    private float timeRemaining = 3f;

    public bool grounded;
    public bool disabled;

    private void Awake()
    {
        // Setting up references
        capsuleCollider2d = transform.GetComponent<CapsuleCollider2D>();
        jumpCount = maxJump;
        disabled = false;
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(h));
        animator.SetFloat("Vertical", GetComponent<Rigidbody2D>().velocity.y);
        if(disabled)
        {
            animator.SetBool("Shocked", true);
            DisabledTimer();
        }
        else
        {
            animator.SetBool("Shocked", false);
        }
        if(!jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            jump = Input.GetButtonDown("Jump");
        }
        // Pass all parameters to the character control script.
        this.Move(h, jump);
        jump = false;
    }

    public void Move(float move, bool jump)
    {
        grounded = IsGrounded();
        //only control the player if grounded or airControl is turned on
        if ((grounded || airControl) && !disabled)
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
        if (jump && IsGrounded())
        {
            // Add a vertical force to the player.
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
            jumpCount -= 1;
        }
        else if (jump && !grounded && jumpCount > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
            jumpCount -= 1;
        }
        //if (grounded) jumpCount = maxJump;
    }
    public void SetDisabled(bool disable) 
    {
        disabled = disable;
    }
    public void DisabledTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else 
        {
            timeRemaining = 3f;
            SetDisabled(false);
        }
    }
    private bool IsGrounded()
    {
        float extraHeightHit = 1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider2d.bounds.center, new Vector2(capsuleCollider2d.bounds.size.x/2, capsuleCollider2d.bounds.size.y/2), 0f, Vector2.down, extraHeightHit, platformLayerMask);
        return raycastHit.collider != null;
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