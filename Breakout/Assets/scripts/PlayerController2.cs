using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{

    //Movement
    public float horizontalInput;
    public float verticalInput;
    public float speed = 5;
    public float startingY = -4.52f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-4.28f, startingY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }



        //Set Bounds
        if (transform.position.x < -7.61f)
        {
            transform.position = new Vector3(-7.61f, startingY, 0);
        }

        if (transform.position.x > -0.87f)
        {
            transform.position = new Vector3(-0.87f, startingY, 0);
        }
    }
}
