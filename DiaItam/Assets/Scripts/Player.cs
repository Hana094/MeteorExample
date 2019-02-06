using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5;
    Vector2 direction;
    Vector2 auxDirection;
    Rigidbody2D rb;

    public GameObject bulletPrefab;

    //shoot variables
    float timeForNextShoot;
    public float timeBetweenShoots;
    public Transform SpaceShipMuzzle;


     public SpriteRenderer sprite;

    float m_spriteHalfWidth;
    float m_spriteHalfHeight;

    

    private void Awake()
    {
        m_spriteHalfWidth = sprite.sprite.bounds.size.y * 0.5f * sprite.transform.localScale.y;
        m_spriteHalfHeight = sprite.sprite.bounds.size.x * 0.5f * sprite.transform.localScale.x;
        rb = GetComponent<Rigidbody2D>();
    }

    void OnDrawGizmos()
    {
        Vector3 floorHeightPos = new Vector3
                (
                    transform.position.x,
                    transform.position.y,
                    transform.position.z
                );

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(floorHeightPos + Vector3.left * m_spriteHalfWidth, floorHeightPos + Vector3.right * m_spriteHalfWidth);
    }

    // Update is called once per frame
    void Update()
    {
        
        Move(Vector2.up * Input.GetAxisRaw("Vertical") + Vector2.right * Input.GetAxisRaw("Horizontal"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

    }

    private void FixedUpdate()
    {
        if (direction!= Vector2.zero)
        {
            auxDirection = transform.position + (Vector3)direction * Time.deltaTime * speed;
            rb.MovePosition(new Vector2(Mathf.Clamp(auxDirection.x, -GameManager.instance.ScreenHalfSizeWorldUnits.x + m_spriteHalfWidth, GameManager.instance.ScreenHalfSizeWorldUnits.x - m_spriteHalfWidth),
                                        Mathf.Clamp(auxDirection.y, -GameManager.instance.ScreenHalfSizeWorldUnits.y + m_spriteHalfHeight, GameManager.instance.ScreenHalfSizeWorldUnits.y - m_spriteHalfHeight)));
            //transform.position += (Vector3)direction * Time.deltaTime * speed;
        }
    }

    void Move(Vector2 dir)
    {
        if (direction!=dir)
        {
            direction = Vector2.zero + Vector2.up*dir.y;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Meteor")
        {
            GameManager.instance.KillPlayer();
        }
            

    }

    void Shoot()
    {
        if (Time.time>timeForNextShoot)
        {
            Instantiate(bulletPrefab,SpaceShipMuzzle.position,Quaternion.identity,null);
            timeForNextShoot = Time.time + timeBetweenShoots;
        }
    }

    public void ResetPlayer()
    {
        timeForNextShoot = Time.time;
        gameObject.SetActive(true);
        transform.position = Vector3.right * (-GameManager.instance.ScreenHalfSizeWorldUnits.x + 1.5f*m_spriteHalfWidth);
    }

    public void Die()
    {
        gameObject.SetActive(false);
        //GameManager.instance.DisplayStartMenu();
    }



}
