using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    float vInput;
    float hInput;

    void Update()
    {
        vInput = Input.GetAxis("Vertical");
        hInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        rb.AddForce((Vector2.up * vInput + Vector2.right * hInput)*speed);
    }
}
