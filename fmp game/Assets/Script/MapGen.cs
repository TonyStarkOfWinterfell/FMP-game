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

    public GameObject obsPrefab;

    public float minObsY;
    public float maxObsY;

    public float minObsSpacing;
    public float maxObsSpacing;

    public float minObsScaleY;
    public float maxObsScaleY;


    void Start()
    {
        obs1 = GameObject.Instantiate(obsPrefab);
        obs1.transform.position = new Vector3(player.transform.position.x + 10f + Random.Range(minObsSpacing, maxObsSpacing), Random.Range(minObsY, maxObsY), 0);
    }


    // Update is called once per frame
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
    }
}
