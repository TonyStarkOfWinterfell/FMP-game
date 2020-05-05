using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class inputWindow : MonoBehaviour
{    
    public InputField inputText;
    private string saveText;
    private int saveScore;

    public GameObject input;

    public HighscoreTable highT;
    public ScoreCount score7; 

    /*
    public void Awake()
    {
        
        score7 = FindObjectOfType<ScoreCount>();

        Hide();
    }


    public void Update()
    {
        //saveScore = (int)score7.scoreCount;
    }


    public void Show()
    {
        input.transform.position = new Vector3(input.transform.position.x, input.transform.position.x -500, 0);
    }

    public void Hide()
    {
        input.transform.position = new Vector3(input.transform.position.x, input.transform.position.x + 500, 0);
    }

    public void Confirm()
    {
        saveText = inputText.text;
        saveScore = (int)score7.scoreCount;
        highT.AddHighscoreEntry(32, "BCA");

        Hide();
    }*/

    
}
 