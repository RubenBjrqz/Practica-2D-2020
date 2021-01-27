using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2D : MonoBehaviour
{
    Animator anim;

    SpriteRenderer spr;

    Rigidbody2D rb2D;

    [SerializeField, Range(0.1f, 20f)]
    float moveSpeed = 2f;

    [SerializeField, Range(0.1f, 15f)]
    float jumpForce;

    [SerializeField]
    Color rayColor = Color.magenta;

    [SerializeField, Range(0.1f, 15f)]
    float rayDistance = 5f;

    [SerializeField]
    LayerMask groundLayer;

    void Awake() 
    {
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        transform.Translate(Vector2.right * Axis.x * moveSpeed * Time.deltaTime);
        if(jumpButton && IsGrouding)
        {
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void LateUpdate()
    {
        spr.flipX = Flip;
        anim.SetFloat("moveX", Mathf.Abs(Axis.x));
    }

    void FixedUpdate()
    {
        Debug.Log(rb2D.velocity.normalized.y);
        anim.SetFloat("velocity", rb2D.velocity.normalized.y);
    }

    Vector2 Axis
    {
        get => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    bool Flip
    {
        get => Axis.x > 0 ? false : Axis.x < 0f ? true : spr.flipX;
    }

    bool jumpButton => Input.GetButtonDown("Jump");

    bool IsGrouding => Physics2D.Raycast(transform.position, Vector2.down, rayDistance, groundLayer);

    void OnDrawGizmosSelected()
    {
        Gizmos.color = rayColor;
        Gizmos.DrawRay(transform.position, Vector2.down * rayDistance);
    }

}
