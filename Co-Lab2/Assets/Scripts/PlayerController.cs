using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    public float speed = 5.0f;
    public float jumpForce = 8.0f;
    public float airControlForce = 10.0f;
    public float airControlMax = 1.5f;

    Vector2 boxExtents;

   

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // get the extent of the collision box
        boxExtents = GetComponent<BoxCollider2D>().bounds.extents;

    }
    // Update is called once per frame
    void Update()
    {
        float xSpeed = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("xspeed", xSpeed);

        float ySpeed = rb.velocity.y;
        animator.SetFloat("yspeed", ySpeed);
    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        if (h > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (h < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false; // Always face right when idle
        }

        // Check if we are on the ground
        Vector2 bottom = new Vector2(transform.position.x, transform.position.y - boxExtents.y);
        Vector2 hitBoxSize = new Vector2(boxExtents.x * 2.0f, 0.05f);

        RaycastHit2D result = Physics2D.BoxCast(bottom, hitBoxSize, 0.0f, Vector2.down, 0.0f, 1 << LayerMask.NameToLayer("Ground"));
        bool grounded = result.collider != null && result.normal.y > 0.9f;

        if (grounded)
        {
            if (Input.GetAxis("Jump") > 0.0f)
            {
                rb.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
            }
            else
            {
                rb.velocity = new Vector2(speed * h, rb.velocity.y);
            }
        }
    }
}
