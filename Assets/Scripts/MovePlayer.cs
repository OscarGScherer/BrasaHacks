using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    public GameObject playerBody;
    public SpriteRenderer backTool;
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
            backTool.flipX = (Mathf.Abs(hInput) > Mathf.Abs(vInput)) ? true : false;
        }
        else if(hInput > 0)
        {
            sr.flipX = false;
            backTool.flipX = (Mathf.Abs(hInput) < Mathf.Abs(vInput)) ? true : false;
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

        if (vInput > hInput)
        {
            backTool.sortingOrder = 3;
        }
        else if (vInput < hInput)
        {
            backTool.sortingOrder = 1;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(movement);
    }
}
