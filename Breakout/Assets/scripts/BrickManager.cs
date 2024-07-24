using System.Collections;
using System.Collections.Generic;
using Unity.Barracuda;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    public GameObject brickPrefab;
    public int rows = 8;
    public int columns = 4;
    public float brickWidth = 2f;
    public float brickHeight = 0.75f;
    public float xAdjust = 1.14f;
    public float yAdjust = 1.4f;
    public float percentMissing = 0.9f;
    public int numberOfLevels = 5;
    // public float greenBricks = 10;
    // public float redBricks = 10;
    public float roof = 4.89416f + 1.4f;


    private List<List<Vector3>> levelBrickPositions;
    public int player1Level;
    public int player2Level;
    public List<GameObject> player1Bricks;
    public List<GameObject> player2Bricks;
    


    // Start is called before the first frame update
    void Start()
    {
        levelBrickPositions = new List<List<Vector3>>();
        player1Bricks = new List<GameObject>();
        player2Bricks = new List<GameObject>();
        player1Level = 0;
        player2Level = 0;

        GenerateLevelBrickPositions();
        GenerateRandomBricksForPlayer(player1Bricks, player1Level);
        GenerateRandomBricksForPlayer(player2Bricks, player2Level);
    }

    private void GenerateLevelBrickPositions()
    {
        for (int level = 0; level < numberOfLevels; level++)
        {
            List<Vector3> brickPositions = new List<Vector3>();
            for (int i = 0; i<rows; i++)
            {
                for (int j = 0; j<columns; j++)
                {
                    if (Random.value > percentMissing)
                    {
                        Vector3 position = new Vector3(j * brickWidth + xAdjust, roof - (i * brickHeight + yAdjust), 0);
                        brickPositions.Add(position);
                        // if (initialBrickPositions.Count >= maxBricks){
                        //     return;
                        // }
                    }
                }
            }
            levelBrickPositions.Add(brickPositions);
        }
    }

    void GenerateRandomBricksForPlayer(List<GameObject> playerBricks, int level)
    {
        foreach (Vector3 position in levelBrickPositions[level])
        {
            GameObject brick = Instantiate(brickPrefab, position, Quaternion.identity, transform);
            playerBricks.Add(brick);
        }
    }

    public void ResetBricks(int player)
    {
        List<GameObject> playerBricks = player == 1 ? player1Bricks : player2Bricks;
        int currentLevel = player == 1 ? player1Level : player2Level;
        foreach (var brick in playerBricks)
        {
            if (brick != null)
            {
                Destroy(brick);
            }
        }
        playerBricks.Clear();
        GenerateRandomBricksForPlayer(playerBricks, currentLevel);
    }
    public void RemoveBrickFromPlayerList(int player, GameObject brick)
    {
        List<GameObject> playerBricks = player == 1 ? player1Bricks : player2Bricks;
        if (playerBricks.Contains(brick))
        {
            playerBricks.Remove(brick);
            // Destroy(brick);

            // Check if the player has destroyed all bricks
            if (playerBricks.Count == 0)
            {
                if (player == 1)
                {
                    player1Level += 1;
                }
                else
                {
                    player2Level += 1;
                }
                Debug.Log($"Player {player} has completed level {(player == 1 ? player1Level : player2Level)}");
                ResetBricks(player);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}


