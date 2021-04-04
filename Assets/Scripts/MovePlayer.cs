using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    public GameObject playerBody;
    SpriteRenderer sr;
    Animator anim;

    Vector2 movement;
    float vInput;
    float hInput;

    private void Start()
    {
        sr = playerBody.GetComponent<SpriteRenderer>();
        anim = playerBody.GetComponent<Animator>();
    }

    void Update()
    {
        vInput = Input.GetAxis("Vertical");
        hInput = Input.GetAxis("Horizontal");

        if (hInput < 0)
        {
            sr.flipX = true;
        }
        else if(hInput > 0)
        {
            sr.flipX = false;
        }

        movement = (Vector2.up * vInput + Vector2.right * hInput) * speed;

        anim.SetFloat("Magnitude", movement.magnitude);
        anim.SetFloat("Horizontal", hInput);
        anim.SetFloat("Vertical", vInput);

        if (movement != Vector2.zero)
        {
            anim.SetFloat("Idle Horizontal", hInput);
            anim.SetFloat("Idle Vertical", vInput);
        }

    }

    private void FixedUpdate()
    {
        rb.AddForce(movement);
    }
}
