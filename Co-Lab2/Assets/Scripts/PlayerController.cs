using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    public float speed = 5.0f;
   

    
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    // Update is called once per frame
    void Update()
    {
        float xSpeed = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("xspeed", xSpeed);

        
    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        if (h != 0.0f)
            rb.velocity = new Vector2(h * speed, 0.0f);
        spriteRenderer.flipX = rb.velocity.x < - 0.1f;

    }
}
