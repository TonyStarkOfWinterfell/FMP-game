using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{

    public GameObject prevRoof;
    public GameObject prevFloor;
    public GameObject floor;
    public GameObject roof;

    public GameObject player;

    public GameObject obs1;
    public GameObject obs2;
    public GameObject obs3;
    public GameObject obs4;
    public GameObject obs5;
    public GameObject obs6;
    public GameObject obs7;

    public GameObject obsPrefab;
    public GameObject[] spawnee;
   
    public float minObsY;
    public float maxObsY;

    public float minObsSpacing;
    public float maxObsSpacing;

    public float minObsScaleY;
    public float maxObsScaleY;

    public bool isSpawning;

    public int randomInt;


    
    void Start()
    {
        obs1 = GenerateObs(player.transform.position.x + 10);
        obs2 = GenerateObs(obs1.transform.position.x);
        obs3 = GenerateObs(obs2.transform.position.x);
        obs4 = GenerateObs(obs3.transform.position.x);
        obs5 = GenerateObs(obs4.transform.position.x);
        obs6 = GenerateObs(obs5.transform.position.x);
        obs7 = GenerateObs(obs6.transform.position.x);
    }

    GameObject GenerateObs(float referenceX)
    {
        randomInt = Random.Range(0, spawnee.Length);
        
        GameObject obs = GameObject.Instantiate(spawnee[randomInt]);
        SetTransform(obs, referenceX);
        return obs;
    }



    void SetTransform(GameObject obs, float referenceX)
    {
        obs.transform.position = new Vector3(referenceX + Random.Range(minObsSpacing, maxObsSpacing), Random.Range(minObsY, maxObsY), 0);

        //stretch on y
        //obs.transform.localRotation = new Vector3(obs.transform.localRotation.x, obs.transform.localRotation.y, obs.transform.localRotation.z);
    }





   
    void Update()
    {
        if (player.transform.position.x > floor.transform.position.x)
        {
            var tempRoof = prevRoof;
            var tempFloor = prevFloor;
            prevRoof = roof;
            prevFloor = floor;

            tempRoof.transform.position += new Vector3(33, 0, 0);
            tempFloor.transform.position += new Vector3(33, 0, 0);

            roof = tempRoof;
            floor = tempFloor;
        }


        if (isSpawning == true)
        {
            if (player.transform.position.x > obs2.transform.position.x)
            {
                var tempObs = obs1;
                obs1 = obs2;
                obs2 = obs3;
                obs3 = obs4;
                obs4 = obs5;
                obs5 = obs6;
                obs6 = obs7;

                SetTransform(tempObs, obs6.transform.position.x);
                obs7 = tempObs;
            }
        }



       
    }
}
