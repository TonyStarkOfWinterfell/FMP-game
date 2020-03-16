using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;
    public float scoreCount;
    public float highScoreCount;

    public float pointsPerSecond;
    public bool scoreIncreasing;


   
    void Update()
    {
        if (scoreIncreasing)
        {
            scoreCount += pointsPerSecond * Time.deltaTime;
        }
        
        if (scoreCount > highScoreCount)
        {
            highScoreCount = scoreCount;
        }

        scoreText.text = Mathf.Round(scoreCount) + " meters";
        highScoreText.text = "Highscore: " + Mathf.Round(highScoreCount) + " meters";
    }
}
