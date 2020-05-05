using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerTap : MonoBehaviour

{
    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnPlayerScored;

    public float tapForce = 10;
    public float tiltSmooth = 5;
    public Vector3 startPos;

    Rigidbody2D rigidbody;
    Quaternion downRotation;
    Quaternion forwardRotation;

    public AudioSource tapAudio;
    public AudioSource scoreAudio;
    public AudioSource dieAudio;

    GameManager game;

    public Text scoredPoints;
    public int ScoreP;

    private ScoreCount theScoreManager;
    private MapGen spawning;
    private inputWindow inWind;


    

    void Start()
    {
        theScoreManager = FindObjectOfType<ScoreCount>();
        spawning = FindObjectOfType<MapGen>();
        inWind = FindObjectOfType<inputWindow>();
        rigidbody = GetComponent<Rigidbody2D>();        
        downRotation = Quaternion.Euler(0, 0, -90);
        forwardRotation = Quaternion.Euler(0, 0, 35);
        game = GameManager.Instance;

        inWind.input.transform.position = new Vector3(inWind.input.transform.position.x, inWind.input.transform.position.y + 50, 0);
    }


    
    void OnEnable()
    {
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    void OnDisable()
    {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }



    void OnGameStarted()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.simulated = true;
    }

    void OnGameOverConfirmed()
    {
        
    } 
       


    void Update()
    {
        if (game.GameOver) return;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            tapAudio.Play();
            transform.rotation = forwardRotation;
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltSmooth * Time.deltaTime);

        scoredPoints.text = "" + Mathf.Round(ScoreP);
    }




    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "ScoreZone")
        {
            spawning.Score.transform.position = new Vector3(Random.Range(spawning.player.transform.position.x + 10, spawning.player.transform.position.x + 50), Random.Range(spawning.minObsY, spawning.maxObsY), spawning.Score.transform.position.z);
            OnPlayerScored();            
            scoreAudio.Play();

            ScoreP += Random.Range(-1, 5);
            //scoredPoints.text = "" + Mathf.Round(Random.Range(-1, 5));
        }

        if (col.gameObject.tag == "DeadZone")
        {
            rigidbody.simulated = false;

            dieAudio.Play();
            
            OnPlayerDied();

            spawning.isSpawning = false;
            theScoreManager.scoreIncreasing = false;

            inWind.input.transform.position = new Vector3(inWind.input.transform.position.x, inWind.input.transform.position.y - 50, 0);

            transform.localPosition = startPos;
            transform.rotation = Quaternion.identity;

            spawning.Score.transform.position = new Vector3(Random.Range(spawning.player.transform.position.x + 10, spawning.player.transform.position.x + 50), Random.Range(spawning.minObsY, spawning.maxObsY), spawning.Score.transform.position.z);

            spawning.obs1 = GenerateObs(spawning.player.transform.position.x + 14);
            spawning.obs2 = GenerateObs(spawning.obs1.transform.position.x);
            spawning.obs3 = GenerateObs(spawning.obs2.transform.position.x);
            spawning.obs4 = GenerateObs(spawning.obs3.transform.position.x);
            spawning.obs5 = GenerateObs(spawning.obs4.transform.position.x);
            spawning.obs6 = GenerateObs(spawning.obs5.transform.position.x);
            spawning.obs7 = GenerateObs(spawning.obs6.transform.position.x);
        }

        GameObject GenerateObs(float referenceX)
        {
            spawning.randomInt = Random.Range(0, spawning.spawnee.Length);

            GameObject obs = GameObject.Instantiate(spawning.spawnee[spawning.randomInt]);
            SetTransform(obs, referenceX);
            return obs;
        }

        void SetTransform(GameObject obs, float referenceX)
        {
            obs.transform.position = new Vector3(referenceX + Random.Range(spawning.minObsSpacing, spawning.maxObsSpacing), Random.Range(spawning.minObsY, spawning.maxObsY), 0);

            obs.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(spawning.minObsZRotate, spawning.maxObsZRotate));
        }
    }
    


}
