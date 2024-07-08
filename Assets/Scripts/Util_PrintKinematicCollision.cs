using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util_PrintKinematicCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("bump");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("bump (trigger)");
    }
}
