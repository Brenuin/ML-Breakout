using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;


public class PlayerController : Agent
{
    public Transform ballTransform;
    public Transform leftWall;
    public Transform rightWall;
    public BrickManager brickManager;

    public float leftBoundary;
    public float rightBoundary;
    private Rigidbody2D ballRigidbody;
    private KeepScore keepScore;

    public float speed = 5;
    public float startingY = -4.52f;
    public float deathLineY = -5.5f;
    private int lastScore1 = 0;
    private int lastLevel1 = 0;

    void Start()
    {
        ballRigidbody = ballTransform.GetComponent<Rigidbody2D>();
        keepScore = GameObject.Find("Canvas").GetComponent<KeepScore>();
        lastScore1 = keepScore.score1;
    }

    public override void OnEpisodeBegin()
    {
        // Reset paddle position
        transform.position = new Vector3(4.28f, startingY, 0);

        // Reset ball position and velocity
        ballTransform.position = new Vector3(4.28f, startingY + 1.0f, 0);
        ballRigidbody.velocity = Vector2.zero;

        // Add initial velocity to the ball
        ballRigidbody.AddForce(new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), 1).normalized * 10.0f, ForceMode2D.Impulse);

        // Reset score
        keepScore.score1 = 0;
        keepScore.score2 = 0;

        // Reset levels
        brickManager.player1Level = 0;
        brickManager.player2Level = 0;

        // Reset lives
        keepScore.lives1 = 3;
        keepScore.lives2 = 3;

        // reset bricks in brick manager
        brickManager.ResetBricks(1);
        brickManager.ResetBricks(2);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Position of the paddle
        sensor.AddObservation(transform.position.x);

        // Position and velocity of the ball
        sensor.AddObservation(ballTransform.position);
        sensor.AddObservation(ballRigidbody.velocity);

        // Distances to walls
        sensor.AddObservation(transform.position.x - leftWall.position.x);
        sensor.AddObservation(rightWall.position.x - transform.position.x);
        
        // bricks
        foreach (GameObject brick in brickManager.player1Bricks)
        {
            sensor.AddObservation(brick.transform.position);
        }


    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int action = actions.DiscreteActions[0];

        float move = 0;

        // Debug.Log(actions.DiscreteActions[0]);

        // Debug.Log(action);

        if (action == 1) 
        {
            // transform.Translate(Vector3.left * Time.deltaTime * speed);
            move = -1;
        }
        if (action == 2) {
            // transform.Translate(Vector3.right * Time.deltaTime * speed);
            move = 1;
        }

        Vector3 newPosition = transform.position + Vector3.right * move * Time.deltaTime * speed;
        newPosition.x = Mathf.Clamp(newPosition.x, leftBoundary, rightBoundary);

        transform.position = newPosition;

        // add small negative reward for each step to penalize wasted action(?)
        AddReward(-0.001f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = 0;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            discreteActionsOut[0] = 1;
            // Debug.Log("left");
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            discreteActionsOut[0] = 2;
            // Debug.Log("right");
        }
    }

    void FixedUpdate()
    {
        // if player level has changed, reset ball and paddle
        if (brickManager.player1Level != lastLevel1)
        {
            lastLevel1 = brickManager.player1Level;
            ballTransform.position = new Vector3(4.28f, startingY + 1.0f, 0);
            ballRigidbody.velocity = Vector2.zero;
            transform.position = new Vector3(4.28f, startingY, 0);
            ballRigidbody.AddForce(new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), 1).normalized * 10.0f, ForceMode2D.Impulse);
        }

        if (brickManager.player1Level > 3 || brickManager.player2Level > 3)
        {
            Debug.Log("Player has completed all levels");
            AddReward(5.0f);
            EndEpisode();
        }
        if (ballTransform.position.y <= deathLineY)
        {
            Debug.Log("Ball fell off");
            AddReward(-3.0f);
            // brickManager.ResetBricks(1);
            EndEpisode();
        }
        CheckBrickCollisions();
    }

    void CheckBrickCollisions()
    {
        // check if brickManager or its bricks list is null
        if (brickManager == null || brickManager.player1Bricks == null)
        {
            Debug.LogError("brickManager or bricks list is null");
            return;
        }

        // Debug.Log("Bricks count: " + brickManager.player1Bricks.Count);

        // check if the bricks list in brickManager is empty
        if (brickManager.player1Bricks.Count == 0)
        {
            AddReward(1.0f);
            Debug.Log("All bricks destroyed");
            // EndEpisode();
        }
        else
        {
            // make sure ballTransform is not null before proceeding
            if (ballTransform == null)
            {
                Debug.LogError("ballTransform is null");
                return;
            }

            // check if score went up and reward if it does
            if (keepScore.score1 > lastScore1)
            {
                float reward = keepScore.score1 - lastScore1;
                AddReward(reward);
                lastScore1 = keepScore.score1;
                // Debug.Log("Score went up");
            }
            
            
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("collided");
        if (collision.gameObject.CompareTag("Ball"))
        {
            AddReward(1f); // Reward for hitting the ball
        }
    }

}



