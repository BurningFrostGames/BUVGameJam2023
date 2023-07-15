using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostProjectile : MonoBehaviour
{
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    
    public void AddForce(Vector3 direction)
    {
        rb.AddForce(direction, ForceMode2D.Impulse);
    }
}
