using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaController : MonoBehaviour {

    private Rigidbody2D rb2d;
    private Animator anim;
    private BoxCollider2D collide;
    private int moveDirection = 1;
    private int wait = 0;
    private bool dead = false;

    public float speed;

    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        collide = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
	
	void FixedUpdate () {
        rb2d.velocity = new Vector2(moveDirection * speed, rb2d.velocity.y);

        if (Physics2D.Raycast(transform.position + Vector3.right * 0.55f, transform.TransformDirection(Vector3.right), 0.05f))
        {
            moveDirection = -1;
        }
        if (Physics2D.Raycast(transform.position + Vector3.left * 0.55f, transform.TransformDirection(Vector3.left), 0.05f))
        {
            moveDirection = 1;
        }

        wait = Mathf.Max(0, wait - 1);
        if (dead == true && wait == 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Killed()
    {
        anim.SetBool("wasKilled", true);
        rb2d.bodyType = RigidbodyType2D.Static;
        collide.enabled = false;
        dead = true;
        wait = 30;
    }
}