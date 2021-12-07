using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public bool find = false;
    public float time = 0;
    public float set_time = 1f;
    public bool arrive = false;
    public bool first = true;
    public Vector3 pos0;
    public Vector3 pos1;
    public Vector3 pos2;
    public float speed = 2f;
    public bool kill = false;
    // Update is called once per frame
    private void Start()
    {
        pos0 = new Vector3(transform.position.x, transform.position.y, transform.position.z); 
        pos1 = new Vector3(transform.position.x+2, transform.position.y, transform.position.z);
        pos2 = new Vector3(transform.position.x-2, transform.position.y, transform.position.z);
    }
    void Update()
    {
        if (!find && !kill)
        {
            if (!first)
            {
                transform.position = Vector3.MoveTowards(transform.position, pos0, 2 * Time.deltaTime);
            }
            if(transform.position  == pos0)
            {
                first = true;
            }
            if (arrive && first)
            {
                transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), pos1.y, pos1.z);
                if (transform.position.x > pos1.x)
                {
                    arrive = false;
                }
            }
            else if (!arrive && first)
            {
                transform.position = new Vector3(transform.position.x - (speed * Time.deltaTime), pos1.y, pos1.z);
                if (transform.position.x < pos2.x)
                {
                    arrive = true;
                }
            }
        }
        else if (find & !kill)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 3*Time.deltaTime);
        }
        
            
            
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            kill = true;
        }
       
    }
    
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("trigger");
        if (collision.CompareTag("Player"))
        {
            find = true;
            Debug.Log("trigger2");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            find = false;
            first = false;
        }
    }

}
