using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brick : MonoBehaviour
{
    public KeepScore keepScore;
    public SpriteRenderer sprite;
    public BrickManager brickManager;

    // Start is called before the first frame update
    void Start()
    {
        brickManager = GameObject.Find("BrickGeneration").GetComponent<BrickManager>();
        keepScore = GameObject.Find("Canvas").GetComponent<KeepScore>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        if (transform.position.x > 0)
        {
            sprite.color = Color.green;
        }
        else
        {
            sprite.color = Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.position.x < 0)
        {
            keepScore.score2 += 1;
        }
        else
        {
            keepScore.score1 += 1;
            
        }
        brickManager.RemoveBrickFromPlayerList(1, gameObject);
        Destroy(gameObject);
    }
}
