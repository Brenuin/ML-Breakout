using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeepScore : MonoBehaviour
{

    public Text score1Text;
    public Text score2Text;
    public Text lives1Text;
    public Text lives2Text;
    public Text level1Text;
    public Text level2Text;
    public BrickManager brickManager;

    public int score1 = 0;
    public int score2 = 0;
    public int lives1 = 3;
    public int lives2 = 3;

    // Start is called before the first frame update
    void Start()
    {
        score1Text.text = "Score: " + score1.ToString();
        score2Text.text = "Score: " + score2.ToString();
        lives1Text.text = "Lives: " + lives1.ToString();
        lives2Text.text = "Lives: " + lives2.ToString();
        level1Text.text = "Level: " + brickManager.player1Level.ToString();
        level2Text.text = "Level: " + brickManager.player2Level.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        score1Text.text = "Score: " + score1.ToString();
        score2Text.text = "Score: " + score2.ToString();
        lives1Text.text = "Lives: " + lives1.ToString();
        lives2Text.text = "Lives: " + lives2.ToString();
        level1Text.text = "Level: " + brickManager.player1Level.ToString();
        level2Text.text = "Level: " + brickManager.player2Level.ToString();

    }
}
