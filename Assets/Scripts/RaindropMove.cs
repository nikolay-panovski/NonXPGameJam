using System;
using UnityEngine;

public class RaindropMove : MonoBehaviour
{
    private Rigidbody2D rb;

    private float yVelocity;
    private float yAcceleration;

    /// <summary>
    /// bool = an "isCollected"; Vector3 = this.transform.position at the moment of impact
    /// </summary>
    public static event Action<bool, Vector3> OnRaindropDestroyed;

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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherCollidedObject = collision.collider.gameObject;
        Debug.Log(otherCollidedObject);
        if (!otherCollidedObject.CompareTag("Raindrops"))   // ignore overlaps with other raindrops
        {
            // determine which collider we hit
            // (count only the 2 types of bucket parts, player, and ground -> either "collect" or "uncollect" the drop)
            // @ Unity: What is that incompetent documentation/description of which collider is "collider" vs "otherCollider"!?
            if (otherCollidedObject.CompareTag("NotCollected"))
            {
                OnRaindropDestroyed?.Invoke(false, transform.position);
            }
            else if (otherCollidedObject.CompareTag("Collected"))
            {
                OnRaindropDestroyed?.Invoke(true, transform.position);
            }
            // else it probably hit the walls, we don't care and shouldn't count these

            // everything else that should happen always: visual effects IF INVOKED FROM HERE AND NOT EVENT, destroy...
            Destroy(gameObject);
        }
    }

    public void InitSetVelocity(float velocity)
    {
        yVelocity = velocity;
    }

    public void InitSetAcceleration(float acceleration)
    {
        yAcceleration = acceleration;
    }
}
