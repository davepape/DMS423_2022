// Script that moves objects based on gravity.
// The gravity force is computed from the positions and masses of all the other objects of this same type (attached to the "solarsystem" object).
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class systembody : MonoBehaviour
{
    private solarsystem sol;
    [SerializeField] private Vector3 velocity;
//    [SerializeField] private float mass = 1;
    private List<systembody> otherbodies;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(velocity, ForceMode.VelocityChange);
        sol = GetComponentInParent<solarsystem>();
        otherbodies = new List<systembody>();
        foreach (Transform other in sol.transform)
            {
                if ((other != transform) && (other.GetComponent<systembody>() != null))
                {
                    otherbodies.Add(other.GetComponent<systembody>());
                }
            }
    }

    
    void FixedUpdate()
    {
        Vector3 position = transform.position;

        foreach (systembody other in otherbodies)
        {
            Vector3 v = other.transform.position - position;
            float force = sol.G * rb.mass * other.rb.mass / v.sqrMagnitude;
            Vector3 forceVector = force * v.normalized;
            rb.AddForce(forceVector);
        }

/*
        Vector3 position = transform.position;

        foreach (systembody other in otherbodies)
        {
            Vector3 acceleration = Vector3.zero;
            Vector3 v = other.transform.position - position;
            float force = sol.G * mass * other.mass / v.sqrMagnitude;
            Vector3 forceVector = force * v.normalized;
            acceleration = forceVector / mass;
            velocity += acceleration * Time.fixedDeltaTime;
        }
       
        position += velocity * Time.fixedDeltaTime;
        transform.position = position;
*/
    }
}
