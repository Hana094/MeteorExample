using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    public float thrust;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb=GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Activate();
    }

    public void Activate()
    {
        rb.AddForce((GameManager.instance.player.transform.position- transform.position) * thrust);
        Invoke("ProjectileDies", 3f);
    }


    void SetDirection()
    {
        //
    }

    void ProjectileDies()
    {
        
        if (!spriteRenderer.isVisible)
        {
            
            Object.Destroy(gameObject);
        }
        else
        {
            Invoke("ProjectileDies", 3f);
        }
    }
}
