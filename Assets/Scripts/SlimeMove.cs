using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMove : MonoBehaviour
{
    Transform player;
    Rigidbody2D rb;
    Animator anim;
    bool canAttack = true;
    bool moving = false;
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player") && canAttack)
        {
            StartCoroutine(Damage(2, other.collider));
        }
    }

    IEnumerator Damage(int dam, Collider2D player)
    {
        canAttack = false;
        player.GetComponent<PlayerHealth>().Damage(2);
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }
        // Update is called once per frame
        void Update()
    {
        if (!moving)
        {
            if (Vector2.Distance(transform.position, player.position) < 7)
            {
                StartCoroutine(Move(player.position));
            }
        }
    }

    public void Knockback(int force, Vector2 source)
    {
        rb.AddForce((source - new Vector2(transform.position.x, transform.position.y)) * force * -1, ForceMode2D.Impulse);
    }

    IEnumerator Move(Vector2 pos)
    {
        moving = true;
        anim.SetBool("Movement", moving);
        rb.AddForce((pos - new Vector2(transform.position.x, transform.position.y))*speed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(.6f);
        moving = false;
        anim.SetBool("Movement", moving);
    }
}
