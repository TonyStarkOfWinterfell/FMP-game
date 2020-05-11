using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDetecter : MonoBehaviour
{
    
    private MapGen spawning4;
         
    void Start()
    {
        spawning4 = FindObjectOfType<MapGen>();
    }
         
        
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "ScoreZone")
        {            
            spawning4.Score.transform.position = new Vector3(Random.Range(spawning4.player.transform.position.x + 20, spawning4.player.transform.position.x + 60), Random.Range(spawning4.minObsY, spawning4.maxObsY), spawning4.Score.transform.position.z);
        }
                     
    }

}
