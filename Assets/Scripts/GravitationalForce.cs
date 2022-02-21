using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalForce : MonoBehaviour
{
    
    private float gravitationalConstant = 0.67f;
    [Range(1, 100000)]
    public float BodyMass;

    Rigidbody rd;
    List<GameObject> everyOtherCelestialBodies;
    float[] celectialBodyDistance;
    Vector3[] force;
    Vector3[] unitVector;
    GameObject[] celestialBodies;
    public Vector3 intialVelocity;


    private void Start()
    {
        rd = GetComponent<Rigidbody>();
        everyOtherCelestialBodies = new List<GameObject>();
        celestialBodies = GameObject.FindGameObjectsWithTag("celestialBodies");
        for (int i = 0; i < celestialBodies.Length; i++)
        {
            if (celestialBodies[i] != gameObject)
            {
                everyOtherCelestialBodies.Add(celestialBodies[i]);
            }

        }
        celectialBodyDistance = new float[everyOtherCelestialBodies.Count];
        force = new Vector3[everyOtherCelestialBodies.Count];
        unitVector = new Vector3[everyOtherCelestialBodies.Count];
        rd.mass = BodyMass;
        rd.velocity = intialVelocity;
    }


    void CalculateDistance()
    {
        for (int i = 0; i < everyOtherCelestialBodies.Count; i++)
        {
            celectialBodyDistance[i] = Vector3.Distance(gameObject.transform.position, everyOtherCelestialBodies[i].transform.position);
        }
    }


    void CalculateUnitVector()
    {

        for (int i = 0; i < everyOtherCelestialBodies.Count; i++)
        {

            unitVector[i] = (everyOtherCelestialBodies[i].transform.position - gameObject.transform.position) / (Vector3.Magnitude(everyOtherCelestialBodies[i].transform.position - gameObject.transform.position));
        }
    }


    void CalculateForce()
    {
        rd.mass = BodyMass;
        for (int i = 0; i < everyOtherCelestialBodies.Count; i++)
        {
            float bodyDistance = celectialBodyDistance[i];
            // f = (Gm1m2/r*r) * unit vector
            //force[i] = -1 * unitVector[i] * (gravitationalConstant * rd.mass * everyOtherCelestialBodies[i].GetComponent<Rigidbody>().mass) / (celectialBodyDistance[i] * celectialBodyDistance[i]);
            force[i] = -1 * unitVector[i] * (gravitationalConstant * rd.mass * everyOtherCelestialBodies[i].GetComponent<Rigidbody>().mass) / Mathf.Pow(bodyDistance, 2);
           
        }
    }


    void ApplyForce()
    {
        for (int i = 0; i < everyOtherCelestialBodies.Count; i++)
        {

            everyOtherCelestialBodies[i].GetComponent<Rigidbody>().AddForce(force[i], ForceMode.Force);
        }
    }


    private void FixedUpdate()
    {
        CalculateDistance();
        CalculateUnitVector();
        CalculateForce();
        ApplyForce();

    }
}
