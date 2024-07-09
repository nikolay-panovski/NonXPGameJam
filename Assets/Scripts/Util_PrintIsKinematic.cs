using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util_PrintIsKinematic : MonoBehaviour
{
    private Rigidbody2D rb;
    void Start()
    {
        TryGetComponent<Rigidbody2D>(out rb);
        Debug.Log("Rigidbody " + rb + " is kinematic: " + rb.isKinematic);
    }

}
