using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private int initialAmmo = 10;
    
    [MMReadOnly]
    public int CurrentAmmo;

    private void Start()
    {
        CurrentAmmo = initialAmmo;
    }

    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    
    public void AddForce(Vector3 direction)
    {
        rb.AddForce(direction, ForceMode2D.Impulse);
    }
}
