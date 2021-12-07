using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomer : MonoBehaviour
{
    public Animator anim;
    public CircleCollider2D circle;
    public LayerMask what_layer;
    public GameObject explosion;
    // Start is called before the first frame update
    void Awake()
    {
        explosion.SetActive(false);
        anim = GetComponent<Animator>();
        anim.speed = 0;
        StartCoroutine(Anim_start());
        
    }
    private IEnumerator Anim_start()
    {
        yield return new WaitForSeconds(2f);
        anim.speed = 1;
        explosion.SetActive(true);
        Boom();
        Destroy(this.gameObject, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Boom()
    {
        int radiusInt = Mathf.RoundToInt(circle.radius);
        for(int i= -radiusInt; i <= radiusInt; i++)
        {
            for(int j = -radiusInt; j <= radiusInt; j++)
            {
                Vector3 check = new Vector3(transform.position.x + i, transform.position.y + j, 0);
                float distance = Vector2.Distance(transform.position, check) - 0.001f;

                if(distance <= radiusInt)
                {
                    Collider2D overC = Physics2D.OverlapCircle(check, 0.1f,what_layer);
                    if (overC != null)
                    {
                        overC.transform.GetComponent<Bricks>().MakeDot(check);
                    }
                }
            }
        }
    }
}
