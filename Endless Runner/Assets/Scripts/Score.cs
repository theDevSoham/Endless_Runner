using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Transform player;
    private int score = 0;  //contains score

    private float traverse = 0.0f;  //min length to traverse for increment of score


    private int speedModifier = 5; //increase speed by this value
    
    private float scoreToIncrease = 5.0f;  //min value of score to level up

    private int difficultyLevel = 0;   //level modifier to increase speed 

    readonly private float maxDifficulty = 10.0f;    //max level after which speed becomes constant

    private bool isDead = false;

    public TriggerDeath death;

    public Text scoreText;
    public Text highScore;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;     //need player transform as score depends on it
        DisplayHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        if (score == scoreToIncrease)
        {
            LevelUp();
        }
        //player must cover one tile for increment of score to +1

        if(player.position.z >= (traverse + 45.0f))
        {
            score += 1;
            traverse += 45.0f;
        }

        //print score on scorebox
        scoreText.text = "<b> Score: " + score.ToString() + "</b>";
    }

    private void LevelUp()
    {
        //condition for maximum speed after which it'll be constant

        if (difficultyLevel >= maxDifficulty)
        {
            return;
        }

        scoreToIncrease *= 2;
        difficultyLevel++;

        GetComponent<Player_move>().SetSpeed((difficultyLevel * speedModifier)/2);
        if(speedModifier == 0)
        {
            return;
        }
        else
        {
            speedModifier--;
        } 
    }

    public void StopScore()
    {
        isDead = true;
        if(PlayerPrefs.GetInt("Highscore") < score)
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
        death.ToggleEndScore(score);
    }

    private void DisplayHighScore()
    {
        highScore.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();
    }
}
