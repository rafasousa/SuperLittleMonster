using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public bool jump = false;

    public float jumpForce = 600f;

    private Animator anim;  
    
    private Transform groundCheck;

    private bool grounded = false;
    
    void Awake()
    {
        // Setting up references.
        groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        // If the jump button is pressed and the player is grounded then the player should jump.
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Mouse0)) && grounded)
        {
            jump = true;

            SoundEffectsHelper.Instance.MakeJumpSound();
        }
    }

    void FixedUpdate()
    {
        // If the player should jump...
        if (jump)
        {
            // Set the Jump animator trigger parameter.
            anim.SetTrigger("Jump");

            // Add a vertical force to the player.
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));

            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
        }
    }
}
