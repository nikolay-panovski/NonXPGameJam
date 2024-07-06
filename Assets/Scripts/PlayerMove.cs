using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speedH;
    [SerializeField] private float gravityY;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        // movement without any collisions; collision response would be implemented in the receiving Collision2D callbacks
        // horizontal
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.MovePosition(rb.position + new Vector2(-speedH, 0) * Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.MovePosition(rb.position + new Vector2(speedH, 0) * Time.fixedDeltaTime);
        }

        // vertical (gravity)
        rb.MovePosition(rb.position + new Vector2(0, -gravityY) * Time.fixedDeltaTime);
    }
}
