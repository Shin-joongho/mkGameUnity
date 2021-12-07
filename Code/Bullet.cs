using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public SpriteRenderer render;
    public float speed = 10f;
    public float set_time = 10f;
    public bool flip = true;
    public bool first = true;
    // Update is called once per frame
    private void Start()
    {
        render = GetComponent<SpriteRenderer>();

    }
    void Update()
    {
        if (first)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                render.flipX = true;
                flip = false;

            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                flip = true;
            }
            first = false;
        }
        Destroy(gameObject, set_time);
        
        if (flip)
        {
            transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), transform.position.y, transform.position.z);
        }
        if (!flip)
        {
            transform.position = new Vector3(transform.position.x - (speed * Time.deltaTime), transform.position.y, transform.position.z);
        }
    }
    
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("bottom") || collision.CompareTag("wall"))
        {
            Destroy(this.gameObject);
        }
        else if (collision.CompareTag("monster"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
