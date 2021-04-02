using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public float speed;
    public Animator anim;

    Vector2 movement;
    float vInput;
    float hInput;

    void Update()
    {
        vInput = Input.GetAxis("Vertical");
        hInput = Input.GetAxis("Horizontal");
        anim.SetFloat("Horizontal", hInput);
        anim.SetFloat("Vertical", vInput);

        if (hInput < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }

        movement = (Vector2.up * vInput + Vector2.right * hInput) * speed;
        anim.SetFloat("Magnitude", movement.magnitude);
    }

    private void FixedUpdate()
    {
        rb.AddForce(movement);
    }
}
