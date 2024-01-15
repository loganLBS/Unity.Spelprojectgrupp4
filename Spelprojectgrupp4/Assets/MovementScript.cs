using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Hur snabbt vår karaktär får röra sig
    public float jumpForce = 5f; // Vilken force hopp knappen kan göra
    public Transform groundCheck; // Kolla om spelaren har rört vid marken
    public float groundCheckRadius = 0.2f; // Inom vilken radie kan vi röra marken
    public LayerMask groundLayer; // Vilket lager har marken
    private bool isFacingRight = true; // Kolla vilket håll karaktären tittar i
    private bool isGrounded = false; // Om vi är på marken eller inte
    private Rigidbody2D rb; // Ref till vår rigidbody2D
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Hämta vår Rigidbody 2D
    }
    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); // Kolla om spelaren är på marken
        float moveDirection = Input.GetAxis("Horizontal");  // Kolla om vi rör oss horisontellt
        Move(moveDirection); // Flytta spelaren
        if (moveDirection > 0 && !isFacingRight) // Flippa spelaren så den tittar i den riktningen som den rör sig i
        {
            Flip();
        }
        else if (moveDirection < 0 && isFacingRight) // Flippa spelaren så den tittar i den riktningen som den rör sig i
        {
            Flip();
        }
        if (Input.GetButtonDown("Jump") && isGrounded) // Om vi tycker på space, så låt spelaren hoppa
        {
            Jump();
        }
    }
    private void Move(float direction)
    {
        Vector2 movement = new Vector2(direction * moveSpeed, rb.velocity.y); // räkna ut hur vi rör oss i relation till spelarens velocity
        rb.velocity = movement;
    }
    private void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse); // Lägg till vertical force för att kunna hoppa
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;  // Flippa player spriten horisontellt
        transform.Rotate(0f, 180f, 0f);
    }
}
