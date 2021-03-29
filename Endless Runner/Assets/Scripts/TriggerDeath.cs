using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TriggerDeath : MonoBehaviour
{
    public Text scoreText;
    public Text highscoreText;
    public Image background;
    private float transition = 2.0f;
    private bool isTriggered = false;
    private float importedScore;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTriggered)
        {
            return;
        }

        transition += Time.deltaTime;
        background.color = Color.Lerp(new Color(0, 0, 0, 0), Color.white,transition);
    }

    public void ToggleEndScore(float score)
    {
        gameObject.SetActive(true);
        scoreText.text = "Current Score: " + score.ToString();
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore");
        isTriggered = true;
        importedScore = score;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ResetScore()
    {
        PlayerPrefs.SetInt("Highscore", 0);
        ToggleEndScore(importedScore);
    }
}
