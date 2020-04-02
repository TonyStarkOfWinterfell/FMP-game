using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOverConfirmed;

    public static GameManager Instance;

    public GameObject total;

    public GameObject startPage;
    public GameObject gameOverPage;
    public GameObject countdownPage;
    public Text scoreText;

    public GameObject character;
    public Sprite fish, jellyFish, turtle, penguin;
    private SpriteRenderer mySprite;
    public int choose2 = 0;


    private ScoreCount theScoreManager3;
    private CharacterSwap CharactersSwap;





    void Start()
    {
        theScoreManager3 = FindObjectOfType<ScoreCount>();      
    }



    enum PageState
    {
        None,
        Start,
        GameOver,
        Countdown
    }

    int score = 0;
    bool gameOver = false;
    
    public bool GameOver { get { return gameOver; } }




    void Awake()
    {
       Instance = this;
        mySprite = character.GetComponent<SpriteRenderer>();
        CharactersSwap = FindObjectOfType<CharacterSwap>();
    }



    void Update()
    {
        if (gameOver == false)
        {
            transform.position += new Vector3(10f * Time.deltaTime, 0, 0);
        }
    }





    void OnEnable()
    {
        CountdownText.OnCountdownFinished += OnCountdownFinished;
        PlayerTap.OnPlayerDied += OnPlayerDied;
        PlayerTap.OnPlayerScored += OnPlayerScored;
    }

    void OnDisable()
    {
        CountdownText.OnCountdownFinished -= OnCountdownFinished;
        PlayerTap.OnPlayerDied -= OnPlayerDied;
        PlayerTap.OnPlayerScored -= OnPlayerScored;
    }



    void OnCountdownFinished()
    {
        SetPageState(PageState.None);
        OnGameStarted();
        score = 0;
        gameOver = false;
    }


    void OnPlayerDied()
    {
        total.transform.position = new Vector3((total.transform.position.x + 500), total.transform.position.y, total.transform.position.z);
        

        gameOver = true;        
        int savedScore = PlayerPrefs.GetInt("Highscore");
        if (score > savedScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        SetPageState(PageState.GameOver);       
    }


    void OnPlayerScored()
    {
        score++;
        scoreText.text = score.ToString();
    }



    void SetPageState(PageState state)
    {
        switch (state)
        {
            case PageState.None:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(false);
                break;

            case PageState.Start:
                startPage.SetActive(true);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(false);
                break;

            case PageState.GameOver:
                startPage.SetActive(false);
                gameOverPage.SetActive(true);
                countdownPage.SetActive(false);
                break;

            case PageState.Countdown:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(true);
                break;
        }
            
    }

    public void ConfirmGameOver()
    {
        //activated when replay button is hit
        OnGameOverConfirmed();
        scoreText.text = "0";
        SetPageState(PageState.Start);

       
    }

    public void StartGame()
    {
        theScoreManager3.scoreCount = 0;
        choose2 = CharactersSwap.SelectInt;
        
        switch (choose2)
        {
            case 1:
                mySprite.sprite = fish;
                break;
            case 2:
                mySprite.sprite = jellyFish;
                break;
            case 3:
                mySprite.sprite = turtle;
                break;
            case 4:
                mySprite.sprite = penguin;
                break;
            default:
                mySprite.sprite = fish;
                break;

        }

        SetPageState(PageState.Countdown);        
    }

}
