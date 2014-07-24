using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public bool jump = false;

    private bool jumping = false;

    private bool jumpDouble = false;

    private float jumpCount = 0f;

    private float jumpLimit = 2f;

    public float jumpForce = 1250f;

    private Animator anim;

    private Transform groundCheck;

    private bool grounded = false;

    public float position = -3.889894f;

    public float gravity = 0f;

    public float gravityMax = 3f;

    public float gravityMin = 1.5f;

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

        if (jumpCount >= jumpLimit && grounded)
            jumpCount = 0;

        jumpDouble = jumpCount < jumpLimit;

        // If the jump button is pressed and the player is grounded then the player should jump.
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Mouse0)) && (grounded || jumpDouble))
        {
            jump = true; jumpCount++;

            if (grounded)
                anim.SetTrigger("Jump"); // Set the Jump animator trigger parameter.

            SoundEffectsHelper.Instance.MakeSound(SoundType.Jump);
        }

        if ((Input.GetButton("Jump") || Input.GetKey(KeyCode.Mouse0)) && gravity >= gravityMin)
        {
            jumping = true;
            gravity -= 0.1f;
        }

        if (Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.Mouse0) || gravity <= gravityMin)
        {
            jumping = false;
            gravity = gravityMax;
        }

        if (transform.position.x < position && grounded && !HUD.pause)
            transform.position = new Vector3(transform.position.x + 0.01f, transform.position.y, transform.position.z);
    }

    void FixedUpdate()
    {
        if (jumping)
            rigidbody2D.gravityScale = gravity;
        else
            rigidbody2D.gravityScale = gravityMax;

        // If the player should jump...
        if (jump)
        {
            // Add a vertical velocity to the player jump.
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce / 100);

            /// Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
        }
    }
}
