using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;

    public float BulletSpeed;

    public float time2die;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(Vector2.right*BulletSpeed);

        Destroy(this.gameObject,time2die);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Meteor"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
