using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaindropMove : MonoBehaviour
{
    private Rigidbody2D rb;

    private float yVelocity;
    private float yAcceleration;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<Rigidbody2D>(out rb);
    }

    private void FixedUpdate()
    {
        yVelocity += yAcceleration;
        rb.MovePosition(rb.position + new Vector2(0, yVelocity));
    }

    // TODO: ON COLLISION: DESTROY + COUNTERS + EFFECTS

    public void InitSetVelocity(float velocity)
    {
        yVelocity = velocity;
    }

    public void InitSetAcceleration(float acceleration)
    {
        yAcceleration = acceleration;
    }
}
