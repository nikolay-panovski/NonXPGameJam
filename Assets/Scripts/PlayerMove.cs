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
        TryGetComponent<Rigidbody2D>(out rb);
    }


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
}
