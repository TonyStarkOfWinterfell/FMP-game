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

    private ScoreCount theScoreManager;
    private MapGen spawning;


    




    void Start()
    {
        theScoreManager = FindObjectOfType<ScoreCount>();
        spawning = FindObjectOfType<MapGen>();
        rigidbody = GetComponent<Rigidbody2D>();
        downRotation = Quaternion.Euler(0, 0, -90);
        forwardRotation = Quaternion.Euler(0, 0, 35);
        game = GameManager.Instance;
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

        if (Input.GetMouseButtonDown(0))
        {
            tapAudio.Play();
            transform.rotation = forwardRotation;
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltSmooth * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "ScoreZone")
        {
            //register score
            OnPlayerScored();
            //play 
            scoreAudio.Play();
        }

        if (col.gameObject.tag == "DeadZone")
        {
            rigidbody.simulated = false;

            dieAudio.Play();
            
            OnPlayerDied();

            spawning.isSpawning = false;
            theScoreManager.scoreIncreasing = false;

            transform.localPosition = startPos;
            transform.rotation = Quaternion.identity;

            spawning.obs1 = GenerateObs(spawning.player.transform.position.x + 10);
            spawning.obs2 = GenerateObs(spawning.obs1.transform.position.x);
            spawning.obs3 = GenerateObs(spawning.obs2.transform.position.x);
            spawning.obs4 = GenerateObs(spawning.obs3.transform.position.x);
        }

        GameObject GenerateObs(float referenceX)
        {
            GameObject obs = GameObject.Instantiate(spawning.obsPrefab);
            SetTransform(obs, referenceX);
            return obs;
        }

        void SetTransform(GameObject obs, float referenceX)
        {
            obs.transform.position = new Vector3(referenceX + Random.Range(spawning.minObsSpacing, spawning.maxObsSpacing), Random.Range(spawning.minObsY, spawning.maxObsY), 0);

            //stretch on y
            obs.transform.localScale = new Vector3(obs.transform.localScale.x, Random.Range(spawning.minObsScaleY, spawning.maxObsY), obs.transform.localScale.z);
        }
    }
    


}
