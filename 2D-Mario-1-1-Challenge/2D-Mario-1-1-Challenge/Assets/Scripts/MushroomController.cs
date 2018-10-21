using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour {

    private Rigidbody2D rb2d;
    private BoxCollider2D collide;
    private int moveDirection = 1;

    public float speed;

    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        collide = GetComponent<BoxCollider2D>();
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
    }
}
