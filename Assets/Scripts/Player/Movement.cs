using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region Public

    public float moveSpeed = 5.0f;
    public Rigidbody2D Player;
    public Rigidbody2D firePoint;
    public GameObject crosshair;
    public Camera cam;
    public AudioClip walking;
    public Animator ani;
    public GameObject holder;

    #endregion Public

    public Vector2 movement;
    private Vector2 mousePos;
    // Update is called once per frame
    private void Start()
    {
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        /*ani.SetFloat("Horizontal", movement.x);
        ani.SetFloat("Vertical", movement.y);
        ani.SetFloat("Speed", movement.sqrMagnitude);*/
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        Player.MovePosition(Player.position + movement * moveSpeed * Time.fixedDeltaTime);
        firePoint.MovePosition(firePoint.position + movement * moveSpeed * Time.fixedDeltaTime);
        firePoint.position = holder.transform.position;
        Vector2 lookDir = mousePos - Player.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //Player.rotation = angle;
        firePoint.rotation = angle;
    }
}
