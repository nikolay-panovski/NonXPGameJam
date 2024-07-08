using UnityEngine;
using Prime31;

public class PlayerMove : MonoBehaviour
{
    [Header("Horizontal movement")]
    [SerializeField] private KeyCode slowDownKey = KeyCode.LeftControl;
    [Tooltip("The usual player horizontal movement speed. See also Speed H Slow.")]
    [SerializeField] private float speedH;
    [Tooltip("The slower player horizontal movement speed when the player holds a specified button, for precise positioning.")]
    [SerializeField] private float speedHSlow;
    [Header("Vertical movement")]
    [SerializeField] private float targetJumpHeight;    // not defined in tiles or anything meaningful!
    [SerializeField] private float gravityY;
    //private Rigidbody2D rb;

    private CharacterController2D controller;

    private SpriteRenderer playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<CharacterController2D>(out controller);
        TryGetComponent<SpriteRenderer>(out playerSprite);
    }

    private void Update()
    {
        Vector2 velocity = controller.velocity;

        if (controller.isGrounded)
            velocity.y = 0;

        if (IsPressingLeft())
        {
            velocity.x = -UsedSpeed;
            // we can do simpler than scale with only the sprite - assume default sprite facing RIGHT:
            playerSprite.flipX = true;
        }
        if (IsPressingRight())
        {
            velocity.x = UsedSpeed;
            // assume default sprite facing RIGHT:
            playerSprite.flipX = false;
        }
        if (!IsPressingLeft() && !IsPressingRight())
        {
            velocity.x = 0;     // unnecessary explicit given that velocity is re-created locally every frame
                                // only useful if we put more (animation) code here
        }

        if (controller.isGrounded && PressedJump())
        {
            velocity.y = Mathf.Sqrt(2f * targetJumpHeight * -gravityY);
        }

        velocity.y += gravityY * Time.deltaTime;

        controller.move(velocity * Time.deltaTime);
    }

    private bool IsPressingLeft()
    {
        return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
    }
    private bool IsPressingRight()
    {
        return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
    }
    private bool PressedJump()
    {
        return Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
    }
    private float UsedSpeed
    {
        get
        {
            return Input.GetKey(slowDownKey) ? speedHSlow : speedH;
        }
    }

    /**
    void FixedUpdate()
    {
        Vector2 finalMove = new Vector2();

        // movement without any collisions; collision response would be implemented in the receiving Collision2D callbacks
        // horizontal
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            finalMove += new Vector2(-speedH, 0) * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            finalMove += new Vector2(speedH, 0) * Time.fixedDeltaTime;
        }

        // vertical (gravity)
        finalMove += new Vector2(0, -gravityY) * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + finalMove);
    }
    /**/
}
