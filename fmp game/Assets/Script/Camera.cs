using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(5f * Time.deltaTime, 0, 0);
    }
}
