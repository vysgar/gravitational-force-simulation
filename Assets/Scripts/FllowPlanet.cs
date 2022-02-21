using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FllowPlanet : MonoBehaviour
{
    public GameObject followPlanet;
    public Vector3 cameraOffSet;
    public Transform lookAt;
    private void Start()
    {
        transform.position = followPlanet.transform.position + cameraOffSet;
    }
    private void FixedUpdate()
    {
        transform.LookAt(lookAt);
        transform.position = Vector3.Slerp(transform.position, followPlanet.transform.position + cameraOffSet, 0.1f);
        
    }
}
