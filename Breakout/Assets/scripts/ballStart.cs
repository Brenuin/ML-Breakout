using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ballStart : MonoBehaviour
{

    Rigidbody2D rb;
    float ang;
    public bool dead = false;
    float dtime;
    public float speed = 5f;

    public float minVelocityY = 4f;
    public float maxVelocityY = 6f;
    public float VelocityY;

    public KeepScore keepScore;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ang = Random.Range(-3.0f, 3.0f);
        rb.velocity = new Vector3(ang, -speed, 0);

        keepScore = GameObject.Find("Canvas").GetComponent<KeepScore>();
    }

    // Update is called once per frame
    void Update()
    {
        VelocityY = rb.velocity.y;

        // if (keepScore.score1 >= 10 || keepScore.score2 >= 10)
        // {
            
        //     // Application.Quit();
        //     // UnityEditor.EditorApplication.isPlaying = false;
        // }

        // on death
        if (transform.position.y < -5.5f)
        {
            // reset
            rb = GetComponent<Rigidbody2D>();
            if (transform.position.x > 0)
            {
                keepScore.lives1 -= 1;
                if (keepScore.lives1 < 0)
                {
                    Application.Quit();
                    UnityEditor.EditorApplication.isPlaying = false;
                }
                transform.position = new Vector3(4.28f, 0, 0);
                
            }
            else
            {
                keepScore.lives2 -= 1;
                if (keepScore.lives2 < 0)
                {
                    Application.Quit();
                    UnityEditor.EditorApplication.isPlaying = false;
                }
                transform.position = new Vector3(-4.28f, 0, 0);
            }
            // pause
            rb.velocity = new Vector3(0, 0, 0);
            dead = true;
            dtime = Time.time + 2.0f;

        }

        //play
        if (Time.time > dtime && dead == true)
        {
            rb = GetComponent<Rigidbody2D>();
            ang = Random.Range(-3.0f, 3.0f);
            rb.velocity = new Vector3(ang, -speed, 0); ;
            dead = false;
        }

        if (dead == false && Mathf.Abs(rb.velocity.y) < minVelocityY)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, -minVelocityY, 0);
            }
            else
            {
                rb.velocity = new Vector3(rb.velocity.x, minVelocityY, 0);
            }
            

        }



    }
}