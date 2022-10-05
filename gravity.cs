// Script that moves a single object based on gravity.
// The gravity force is computed from the position of a separate "sun" object.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravity : MonoBehaviour
{
    private solarsystem sol;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Transform sunTransform;

    void Start()
    {
        sol = GetComponentInParent<solarsystem>();
        print(sol.G);
    }

    void FixedUpdate()
    {
        Vector3 sunPosition = sunTransform.position;
        Vector3 position = transform.position;
        Vector3 acceleration;
        Vector3 v = sunPosition - position;
        float mymass = 1;
        float sunmass = 1;
        float F = sol.G * mymass * sunmass / (v.sqrMagnitude);
        acceleration = F/mymass * v.normalized;
        velocity += acceleration * Time.fixedDeltaTime;
        position += velocity * Time.fixedDeltaTime;
        transform.position = position;
    }
}
