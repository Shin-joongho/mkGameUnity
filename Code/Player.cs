using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D PlayerMove;
    private SpriteRenderer Render;
    private Animator What_state;
    public GameObject Bullet;
    public GameObject Back_sound;
    public float bull_time = 0.2f;
    public float shooting = 0;
    public int is_state = 0;
    public float speed = 6;
    public float jump_speed = 400;
    public float maxspeed = 6;
    private string state = "char_ani";
    public bool isground = true;
    public bool isclimb = false;
    public bool isdead = false;
    public int dead_first = 0;
    public float set_bull = 5f;
    public AudioSource audio;
    public AudioClip dead_sound;
    public int set_boom = 0;
    public GameObject Boom_set;
    public float boom_timer = 2f;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMove = GetComponent<Rigidbody2D>();
        Render = GetComponent<SpriteRenderer>();
        What_state = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isdead)
        {
            shooting += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space) && isground)//바닥에 닿으면 0이 되도록 변경해야함
            {
                PlayerMove.AddForce(new Vector2(0, jump_speed));
                Debug.Log("space");
                isground = false;
                is_state = (int)char_state.jump;
            }
            if (PlayerMove.velocity.x > maxspeed)
            {
                PlayerMove.velocity = new Vector2(maxspeed, PlayerMove.velocity.y);
            }
            if (PlayerMove.velocity.x < -maxspeed)
            {
                PlayerMove.velocity = new Vector2(-maxspeed, PlayerMove.velocity.y);
            }
            if (Input.GetKey(KeyCode.Z) && shooting >= bull_time)
            {
                Shooting();
            }
            if (Input.GetKeyDown(KeyCode.X) && set_boom == 0)
            {
                GameObject boomer = Instantiate(Boom_set);
                boomer.transform.position = transform.position;

            }
        }
    }
    private void FixedUpdate()
    {
        if(!isdead)
        {
            Move();
            if (!isground)
            {
                is_state = (int)char_state.jump;
            }
            Ani();
            if (isclimb)
            {
                What_state.SetBool("isClimb", true);
                float ver = Input.GetAxis("Vertical");
                PlayerMove.velocity = new Vector2(PlayerMove.velocity.x, ver * speed);
                PlayerMove.gravityScale = 0;
            }
            else
            {
                What_state.SetBool("isClimb", false);
                PlayerMove.gravityScale = 2;
            }
        }
        else if(isdead && dead_first == 0)
        {
            What_state.SetBool("die", true);
            Dead();
            audio.clip = dead_sound;
            audio.Play();
        }
    }
    enum char_state
    {
        stay = 0,
        run = 1,
        jump = 2,
        climb = 3,
    };

    void Move()
    {
        int RL = 0;
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            RL = 1;
            Render.flipX = false;
            if(isground)
            {
                is_state = (int)char_state.run;
            }
        }
        else if(Input.GetAxisRaw("Horizontal") < 0)
        {
            RL = -1;
            Render.flipX = true;
            if (isground)
            {
                is_state = (int)char_state.run;
            }
        }
        else
        {
            is_state = (int)char_state.stay;
        }
        PlayerMove.velocity = new Vector2(speed*RL, PlayerMove.velocity.y);
    }

    void Ani()
    {
        if (is_state == (int)char_state.jump)
        {
            What_state.SetInteger(state, (int)char_state.jump);
        }
        else if (is_state == (int)char_state.run)
        {
            What_state.SetInteger(state, (int)char_state.run);
        }
        else
        {
            What_state.SetInteger(state, (int)char_state.stay);
        }
    }

    void Shooting()
    {
        audio.Play();
        GameObject bull = Instantiate(Bullet);
        bull.transform.position = transform.position;
        Destroy(bull, set_bull);
        shooting = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "bottom")
        {
            isground = true;
        }
        if (collision.gameObject.tag == "monster")
        {
            isdead = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ladder"))
        {
            isclimb = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isclimb = false;
        }
    }
    void Dead()
    {
        Back_sound.SetActive(false);
        PlayerMove.bodyType = RigidbodyType2D.Static;
        GameManger.instance.Dead();
        dead_first = 1;
    }
}
